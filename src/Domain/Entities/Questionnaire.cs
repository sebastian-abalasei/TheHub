namespace TheHub.Domain.Entities;

public class Questionnaire:BaseAuditableEntity
{
    public string Title { get; init; } = string.Empty;
}
