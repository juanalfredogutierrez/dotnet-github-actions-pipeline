using SunatIntegration.Domain.Common;

namespace SunatIntegration.Domain.Entities
{
    public abstract class BaseEntity : IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
