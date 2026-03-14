namespace SunatIntegration.Domain.Common
{
    public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
