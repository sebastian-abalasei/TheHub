using TheHub.Application.Common.Interfaces;
using TheHub.Domain.Quiz;
using TheHub.Domain.Quiz.Entities;

namespace TheHub.Application.Quizzes.Queries.GetQuizzes;

public record GetQuizQuery(int Id) : IRequest<QuizAggregate>;

public class GetQuizQueryHandler(IApplicationDbContext context) : IRequestHandler<GetQuizQuery, QuizAggregate>
{
    public async Task<QuizAggregate> Handle(GetQuizQuery request, CancellationToken cancellationToken)
    {
        var quiz= await context.Quizzes
            .AsNoTracking().FirstAsync(q => q.Id == request.Id);
        QuizAggregate aggregate = new QuizAggregate(quiz.Title);
        var q1 = new Question(quiz.Id, "Lorem ipsum dolor sit amet, consectet 1");
        q1.AddAnswer(new Answer(1,"Option 1"));
        q1.AddAnswer(new Answer(2, "Option 2"));
        q1.AddAnswer(new Answer(3, "Option 3",true));
        q1.AddAnswer(new Answer(4, "Option 4"));
        aggregate.AddQuestion(q1);
        var q2 = new Question(quiz.Id, "Lorem ipsum dolor sit amet, consectet 2");
        q2.AddAnswer(new Answer(1, "Option 1"));
        q2.AddAnswer(new Answer(2, "Option 2"));
        q2.AddAnswer(new Answer(3, "Option 3"));
        q2.AddAnswer(new Answer(4, "Option 4",true));
        aggregate.AddQuestion(q2);
        return aggregate;
    }
}
