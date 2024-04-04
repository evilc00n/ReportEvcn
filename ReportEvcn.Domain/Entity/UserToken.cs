using ReportEvcn.Domain.Interfaces;

namespace ReportEvcn.Domain.Entity
{
    public class UserToken : IEntityId<Guid>
    {
        public Guid Id { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpireTime { get; set; }


        public User User { get; set; }

        public Guid UserId { get; set; }


    }
}
