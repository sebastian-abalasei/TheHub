namespace TheHub.Domain.Common.Entities;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset Created { get; set; }

    public ulong CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public ulong LastModifiedBy { get; set; }
}
