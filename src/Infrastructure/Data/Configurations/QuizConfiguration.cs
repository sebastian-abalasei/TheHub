#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheHub.Domain.Entities;
using TheHub.Domain.Quiz.Entities;

#endregion

namespace TheHub.Infrastructure.Data.Configurations;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.Property(q => q.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
    }
}
