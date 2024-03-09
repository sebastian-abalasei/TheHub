using TheHub.Domain.Models;
using TheHub.Domain.Quiz.Entities;

namespace TheHub.Domain.Quiz;

public class QuizAggregate : AggregateRoot
{
    public string Title { get; } = string.Empty;
    private readonly List<Question> _questions = new();
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();
    public QuizAggregate(string title)
    {
        Title = title;
    }
    public void AddQuestion(Question question)
    {
        _questions.Add(question);
    }
    public void RemoveQuestion(Question question)
    {
        _questions.Remove(question);
    }
    public void UpdateQuestion(Question question)
    {
        _questions.Remove(question);
        _questions.Add(question);
    }
}
