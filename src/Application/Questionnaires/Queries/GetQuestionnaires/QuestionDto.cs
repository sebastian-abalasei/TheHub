#region

using TheHub.Domain.Entities;

#endregion

namespace TheHub.Application.Questionnaires.Queries.GetQuestionnaires;

public class QuestionDto
{
    public int QuestionnaireId { get; init; }

    public string? Text { get; init; }


    public static explicit operator QuestionDto(Question item)
    {
        return new QuestionDto()
        {
            QuestionnaireId = item.QuestionnaireId,
            Text = item.Text,
        };
    }
}
