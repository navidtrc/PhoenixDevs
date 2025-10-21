using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phoenix.Domain;
using Phoenix.Domain.Aggregates.Subscription;
using Phoenix.Domain.Aggregates.Subscription.Enums;

namespace Phoenix.Infrastructure.Configurations;

public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
{

    public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(Consts.SUBSCRIPTION_PLAN_TITLE.MaxLength);

        builder.Property(x => x.Description)
            .HasMaxLength(Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MaxLength)
            .IsUnicode();
        
        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(p => p.Amount)
                .IsRequired()
                .HasPrecision(19, 4) 
                .HasColumnName("Price");
        });

        builder.Property(x => x.Duration)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.ToTable("SubscriptionPlans");
    }
}