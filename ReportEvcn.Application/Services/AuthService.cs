﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportEvcn.Application.Resources;
using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Dto.User;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Enum;
using ReportEvcn.Domain.Interfaces.Databases;
using ReportEvcn.Domain.Interfaces.Repositories;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;
using Serilog;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ReportEvcn.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<UserToken> _userTokenRepository;
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IBaseRepository<User> userRepository, ILogger logger,
            IMapper mapper, ITokenService tokenService, IBaseRepository<UserToken> userTokenRepository,
            IBaseRepository<Role> roleRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _tokenService = tokenService;
            _userTokenRepository = userTokenRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }


        /// <inheritdoc />
        public async Task<BaseResult<TokenDTO>> Login(LoginUserDTO dto)
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Login == dto.Login);
            if (user == null)
            {
                return new BaseResult<TokenDTO>
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            if (!IsVerifyPassword(user.Password, dto.Password))
            {
                return new BaseResult<TokenDTO>
                {
                    ErrorMessage = ErrorMessage.PasswordIsWrong,
                    ErrorCode = (int)ErrorCodes.PasswordIsWrong
                };
            }

            var userToken = await _userTokenRepository.GetAll()
                .FirstOrDefaultAsync(x => x.UserId == user.Id);

            var userRoles = user.Roles;

            var claims = userRoles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();
            claims.Add(new Claim(ClaimTypes.Name, user.Login));


            var refreshToken = _tokenService.GenerateRefreshToken();
            var accessToken = _tokenService.GenerateAccessToken(claims);

            if (userToken == null)
            {
                userToken = new UserToken()
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7)
                };
                await _userTokenRepository.CreateAsync(userToken);
                await _userTokenRepository.SaveChangesAsync();
            }
            else
            {
                userToken.RefreshToken = refreshToken;
                userToken.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7);
                var updatedUserToken = _userTokenRepository.Update(userToken);
                await _userTokenRepository.SaveChangesAsync();
            }

            return new BaseResult<TokenDTO>()
            {
                Data = new TokenDTO()
                {
                    AccessToken = accessToken,
                    RefreshToken = userToken.RefreshToken
                }
            };
        }

        /// <inheritdoc />
        public async Task<BaseResult<UserDTO>> Register(RegisterUserDTO dto)
        {
            if (dto.Password != dto.PasswordConfirm)
            {
                return new BaseResult<UserDTO>()
                {
                    ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                    ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm
                };
            }
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == dto.Login);
            if (user != null)
            {
                return new BaseResult<UserDTO>()
                {
                    ErrorMessage = ErrorMessage.UserAlreadyExists,
                    ErrorCode = (int)ErrorCodes.UserAlreadyExists
                };
            }
            var hashUserPassword = HashPassword(dto.Password);

            using(var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    user = new User()
                    {
                        Login = dto.Login,
                        Password = hashUserPassword
                    };

                    await _unitOfWork.Users.CreateAsync(user);

                    await _unitOfWork.SaveChangesAsync();

                    var role = await _roleRepository.GetAll()
                        .FirstOrDefaultAsync(x => x.Name == nameof(Roles.User));

                    if (role == null)
                    {
                        return new BaseResult<UserDTO>()
                        {
                            ErrorMessage = ErrorMessage.RoleNotFound,
                            ErrorCode = (int)ErrorCodes.RoleNotFound
                        };
                    }

                    var userRole = new UserRole()
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    };

                    await _unitOfWork.UserRoles.CreateAsync(userRole);
                    await _unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }

            return new BaseResult<UserDTO>()
            {
                Data = _mapper.Map<UserDTO>(user)
            };

        }

        private string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool IsVerifyPassword(string userPasswordHash, string userPassword)
        {
            var hash = HashPassword(userPassword);
            return hash == userPasswordHash;
        }
    }
}
