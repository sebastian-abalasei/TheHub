namespace TheHub.Domain.Entities;

public class Question(int QuizId, string Text, string [] Options, int [] Scores)
{
    public int QuizId { get; init; } = QuizId;
    public string Text { get; init; } = Text;
    public string [] Options { get; init; } = Options;
    public int [] Scores { get; init; } = Scores;

    public void Deconstruct(out int QuizId, out string Text, out string [] Options, out int [] Scores)
    {
        QuizId = this.QuizId;
        Text = this.Text;
        Options = this.Options;
        Scores = this.Scores;
    }
}
