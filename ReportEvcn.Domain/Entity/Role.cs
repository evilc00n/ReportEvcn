using ReportEvcn.Domain.Interfaces;

namespace ReportEvcn.Domain.Entity
{
    public class Role : IEntityId<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set;}


        public Role() { }
        public Role(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
