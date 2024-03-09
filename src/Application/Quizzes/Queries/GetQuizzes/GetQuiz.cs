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
        aggregate.AddQuestion(new Question(quiz.Id, "Lorem ipsum dolor sit amet, consectet 1"));
        aggregate.AddQuestion(new Question(quiz.Id, "Lorem ipsum dolor sit amet, consectet 2"));
        aggregate.AddQuestion(new Question(quiz.Id, "Lorem ipsum dolor sit amet, consectet 3"));
        aggregate.AddQuestion(new Question(quiz.Id, "Lorem ipsum dolor sit amet, consectet 4"));
        return aggregate;
    }
}
