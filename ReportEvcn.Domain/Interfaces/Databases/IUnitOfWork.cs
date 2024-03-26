using Microsoft.EntityFrameworkCore.Storage;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Interfaces.Repositories;

namespace ReportEvcn.Domain.Interfaces.Databases
{
    public interface IUnitOfWork : IStateSaveChanges
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        IBaseRepository<User> Users { get; set; }
        IBaseRepository<Role> Roles { get; set; }
        IBaseRepository<UserRole> UserRoles { get; set; }

    }
}
