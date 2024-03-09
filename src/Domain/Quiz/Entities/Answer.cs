namespace TheHub.Domain.Quiz.Entities;
public class Answer
{
    public int Id { get; init; } = 0;
    public string Text { get; init; } = string.Empty;
    
    public bool IsCorrect { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;

    public Answer(int id, string text, bool isCorrect = false)
    {
        Id = id;
        Text = text ?? string.Empty;
        IsCorrect = isCorrect;
    }
    
}
