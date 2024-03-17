using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportEvcn.Application.Resources;
using ReportEvcn.Domain.Dto.Role;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Enum;
using ReportEvcn.Domain.Interfaces.Repositories;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;

namespace ReportEvcn.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IBaseRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;

        public RoleService(IBaseRepository<User> userRepository,
            IBaseRepository<Role> roleRepository, IMapper mapper, 
            IBaseRepository<UserRole> userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
        }


        /// <inheritdoc />
        public async Task<BaseResult<RoleDTO>> CreateRoleAsync(CreateRoleDTO dto)
        {
            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);

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
            return new BaseResult<RoleDTO>()
            {
                Data = _mapper.Map<RoleDTO>(role)
            };

        }

        /// <inheritdoc />
        public async Task<BaseResult<RoleDTO>> DeleteRoleAsync(long id)
        {
            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (role == null)
            {
                return new BaseResult<RoleDTO>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode = (int)ErrorCodes.RoleNotFound
                };
            }

            await _roleRepository.RemoveAsync(role);
            return new BaseResult<RoleDTO>()
            {
                Data = _mapper.Map<RoleDTO>(role)
            };
        }

        /// <inheritdoc />
        public async Task<BaseResult<RoleDTO>> UpdateRoleAsync(UpdateRoleDTO dto)
        {
            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (role == null)
            {
                return new BaseResult<RoleDTO>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode = (int)ErrorCodes.RoleNotFound
                };
            }
            role.Name = dto.Name;

            await _roleRepository.UpdateAsync(role);
            return new BaseResult<RoleDTO>()
            {
                Data = _mapper.Map<RoleDTO>(role)
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

            var roles = user.Roles.Select(x => x.Name).ToArray();
            if (!roles.Any(x => x == dto.RoleName))
            {
                var role = await _roleRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == dto.RoleName);

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
                return new BaseResult<UserRoleDTO>()
                {
                    Data = new UserRoleDTO()
                    {
                        Login = user.Login,
                        RoleName = role.Name
                    }
                };
            }

            return new BaseResult<UserRoleDTO>()
            {
                ErrorMessage = ErrorMessage.UserAlreadyHaveThisRole,
                ErrorCode = (int)ErrorCodes.UserAlreadyHaveThisRole
            };


        }
    }
}
