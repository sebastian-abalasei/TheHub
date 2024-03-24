using TheHub.Domain.Common.Entities;

namespace TheHub.Domain.Quiz.Entities;

public class Quiz : BaseAuditableEntity
{
    public string Title { get; }

    public Quiz( string title )
    {
        this.Title = title;
    }
}
