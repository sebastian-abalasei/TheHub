namespace TheHub.Domain.Quiz.Entities;

public class Question(int QuizId, string Text)
{
    public int QuizId { get; init; } = QuizId;
    public string Text { get; init; } = Text;
    private readonly List<Answer> _answers = [];
    public bool isEditable { get; set; }
    public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();    
    public void AddAnswer(Answer answer)
    {
        _answers.Add(answer);
    }
    public void RemoveAnswer(Answer answer)
    {
        _answers.Remove(answer);
    }
    public void ClearAnswers()
    {
        _answers.Clear();
    }
    
}

