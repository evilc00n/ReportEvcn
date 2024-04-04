using ReportEvcn.Domain.Interfaces;


namespace ReportEvcn.Domain.Entity
{
    public class Report : IEntityId<Guid>, IAuditable
    {
        public Guid Id { get; set ; }

        public string Name { get; set; }

        public string Description { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }

        public Report() { }

        public Report(Guid id, string name, string description, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }


    }
}
