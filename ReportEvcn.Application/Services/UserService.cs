
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportEvcn.Application.Resources;
using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Dto.User;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Enum;
using ReportEvcn.Domain.Interfaces.Repositories;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;
using Serilog;
using System.Security.Claims;

namespace ReportEvcn.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserService(IBaseRepository<User> userRepository,
            ILogger logger,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<BaseResult<UserDTO>> GetUserDataAsync(Guid userId)
        {
            var user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                _logger.Error(ErrorMessage.UserNotFound);
                return new BaseResult<UserDTO>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            return new BaseResult<UserDTO>()
            {
                Data = _mapper.Map<UserDTO>(user),
            };
        }





    }
}
