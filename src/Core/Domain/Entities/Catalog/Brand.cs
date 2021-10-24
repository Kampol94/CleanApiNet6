using CleanApi.Domain.Contracts;
using CleanApi.Domain.Extensions;

namespace CleanApi.Domain.Entities.Catalog
{
    public class Brand : AuditableEntity, IMustHaveTenant
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string TenantKey { get; set; }

        public Brand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Brand Update(string name, string description)
        {
            if (name != null && !Name.NullToString().Equals(name)) Name = name;
            if (description != null && !Description.NullToString().Equals(description)) Description = description;
            return this;
        }
    }
}