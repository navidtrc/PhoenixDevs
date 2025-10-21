using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phoenix.Domain;
using Phoenix.Domain.Aggregates.Subscription;
using Phoenix.Domain.Aggregates.User;

namespace Phoenix.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(Consts.USER_USERNAME.MaxLength);
        
        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("Email");
        });
        
        builder.HasOne(x => x.CurrentPlan)
            .WithMany()
            .HasForeignKey(x => x.CurrentPlanId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Users_CurrentPlan");

        builder.HasOne(x => x.ReservedPlan)
            .WithMany()
            .HasForeignKey(x => x.ReservedPlanId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Users_ReservedPlan");

        builder.ToTable("Users");
    }
}