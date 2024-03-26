
namespace ReportEvcn.Domain.Dto.UserRole
{
    public class UpdateUserRoleDTO
    {
        public string Login { get; set; }
        public long OldRoleId { get; set; }
        public long NewRoleId { get; set; }
    }
}
