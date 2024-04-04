using ReportEvcn.Domain.Interfaces;


namespace ReportEvcn.Domain.Entity
{
    public class User : IEntityId<Guid>, IAuditable
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<Report> Reports { get; set; }

        public List<Role> Roles { get; set; }

        public UserToken UserToken { get; set; }

        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }

        public User() { }
        public User(Guid id, string login)
        {
            Id = id;
            Login = login;
        }
        
    }
}
