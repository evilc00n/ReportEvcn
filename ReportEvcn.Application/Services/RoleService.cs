using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportEvcn.Application.Resources;
using ReportEvcn.Domain.Dto.Report;
using ReportEvcn.Domain.Dto.Role;
using ReportEvcn.Domain.Dto.UserRole;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Enum;
using ReportEvcn.Domain.Interfaces.Databases;
using ReportEvcn.Domain.Interfaces.Repositories;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;
using Serilog;

namespace ReportEvcn.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IBaseRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IBaseRepository<User> userRepository,
            IBaseRepository<Role> roleRepository, IMapper mapper,
            IBaseRepository<UserRole> userRoleRepository, 
            IUnitOfWork unitOfWork, ILogger logger)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<CollectionResult<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAll()
                .Select(x => _mapper.Map<RoleDTO>(x))
                .ToArrayAsync();

            if (roles == null || roles.Length == 0)
            {
                _logger.Error(ErrorMessage.RoleNotFound);
                return new CollectionResult<RoleDTO>
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode = (int)ErrorCodes.RoleNotFound
                };
            }


            return new CollectionResult<RoleDTO>
            {
                Data = roles,
                Count= roles.Length
            };
        }


        /// <inheritdoc />
        public async Task<BaseResult<RoleDTO>> CreateRoleAsync(CreateRoleDTO dto)
        {
            var role = await _roleRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Name == dto.Name);

            if (role != null) 
            {
                return new BaseResult<RoleDTO>()
                {
                    ErrorMessage = ErrorMessage.RoleAlreadyExists,
                    ErrorCode = (int)ErrorCodes.RoleAlreadyExists
                };
            }

            role = new Role()
            {
                Name = dto.Name,
            };

            await _roleRepository.CreateAsync(role);
            await _roleRepository.SaveChangesAsync();
            return new BaseResult<RoleDTO>()
            {
                Data = _mapper.Map<RoleDTO>(role)
            };

        }

        /// <inheritdoc />
        public async Task<BaseResult<RoleDTO>> DeleteRoleAsync(Guid id)
        {
            var role = await _roleRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (role == null)
            {
                return new BaseResult<RoleDTO>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode = (int)ErrorCodes.RoleNotFound
                };
            }

            _roleRepository.Remove(role);
            await _roleRepository.SaveChangesAsync();
            return new BaseResult<RoleDTO>()
            {
                Data = _mapper.Map<RoleDTO>(role)
            };
        }

        /// <inheritdoc />
        public async Task<BaseResult<RoleDTO>> UpdateRoleAsync(UpdateRoleDTO dto)
        {
            var role = await _roleRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (role == null)
            {
                return new BaseResult<RoleDTO>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode = (int)ErrorCodes.RoleNotFound
                };
            }
            role.Name = dto.Name;

            var updatedRole = _roleRepository.Update(role);
            await _roleRepository.SaveChangesAsync();
            return new BaseResult<RoleDTO>()
            {
                Data = _mapper.Map<RoleDTO>(updatedRole)
            };
        }

        /// <inheritdoc />
        public async Task<BaseResult<UserRoleDTO>> AddRoleForUserAsync(UserRoleDTO dto)
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Login == dto.Login);

            if (user == null)
            {
                return new BaseResult<UserRoleDTO>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            var roles = user.Roles.Select(x => x.Id).ToArray();
            if (!roles.Any(x => x == dto.RoleId))
            {
                var role = await _roleRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == dto.RoleId);

                if (role == null)
                {
                    return new BaseResult<UserRoleDTO>()
                    {
                        ErrorMessage = ErrorMessage.RoleNotFound,
                        ErrorCode = (int)ErrorCodes.RoleNotFound
                    };
                }

                UserRole userRole = new UserRole()
                {
                    RoleId = role.Id,
                    UserId = user.Id
                };
                
                await _userRoleRepository.CreateAsync(userRole);
                await _userRoleRepository.SaveChangesAsync();
                return new BaseResult<UserRoleDTO>()
                {
                    Data = new UserRoleDTO(Login: user.Login, RoleId:role.Id)
                };
            }

            return new BaseResult<UserRoleDTO>()
            {
                ErrorMessage = ErrorMessage.UserAlreadyHaveThisRole,
                ErrorCode = (int)ErrorCodes.UserAlreadyHaveThisRole
            };


        }

        ///<inheritdoc/>
        public async Task<BaseResult<UserRoleDTO>> DeleteRoleForUserAsync(DeleteUserRoleDTO dto)
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Login == dto.Login);

            if (user == null)
            {
                return new BaseResult<UserRoleDTO>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }
            var role = user.Roles.FirstOrDefault(x => x.Id == dto.RoleId);
            if (role == null)
            {
                return new BaseResult<UserRoleDTO>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode = (int)ErrorCodes.RoleNotFound
                };
            }

            var userRole = await _userRoleRepository.GetAll()
                .Where(x => x.RoleId == role.Id)
                .FirstOrDefaultAsync(x => x.UserId == user.Id);

            _userRoleRepository.Remove(userRole);
            await _userRoleRepository.SaveChangesAsync();

            return new BaseResult<UserRoleDTO>()
            {
                Data = new UserRoleDTO(Login:user.Login, RoleId:role.Id)
            };
        }

        public async Task<BaseResult<UserRoleDTO>> UpdateRoleForUserAsync(UpdateUserRoleDTO dto)
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Login == dto.Login);

            if (user == null)
            {
                return new BaseResult<UserRoleDTO>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }
            var role = user.Roles
                .FirstOrDefault(x => x.Id == dto.OldRoleId);
            if (role == null)
            {
                return new BaseResult<UserRoleDTO>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode = (int)ErrorCodes.RoleNotFound
                };
            }

            var newRoleForUser = await _roleRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.NewRoleId);
            if (newRoleForUser == null)
            {
                return new BaseResult<UserRoleDTO>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode = (int)ErrorCodes.RoleNotFound
                };
            }

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var userRole = await _unitOfWork.UserRoles.GetAll()
                        .Where(x => x.RoleId == role.Id)
                        .FirstAsync(x => x.UserId == user.Id);

                    _unitOfWork.UserRoles.Remove(userRole);
                    await _unitOfWork.SaveChangesAsync();

                    var newUserRole = new UserRole()
                    {
                        RoleId = newRoleForUser.Id,
                        UserId = user.Id
                    };
                    await _unitOfWork.UserRoles.CreateAsync(newUserRole);
                    await _unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }

            return new BaseResult<UserRoleDTO>()
            {
                Data = new UserRoleDTO(Login: user.Login, RoleId : newRoleForUser.Id)
            };
        }


    }
}
