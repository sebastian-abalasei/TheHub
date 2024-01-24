namespace TheHub.Domain.Entities;

public record Question(int QuestionnaireId, string Text, string [] Options, int [] Scores);
