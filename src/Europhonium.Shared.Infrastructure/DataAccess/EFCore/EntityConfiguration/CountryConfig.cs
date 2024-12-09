using Europhonium.Shared.Domain.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Europhonium.Shared.Infrastructure.DataAccess.EFCore.EntityConfiguration;

public sealed class CountryConfig : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("country");

        builder.Property(country => country.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasConversion(src => src.Value, value => CountryId.FromValue(value));

        builder.Property(country => country.CountryCode)
            .IsRequired()
            .HasColumnType("char(2)")
            .HasConversion(src => src.Value, value => CountryCode.FromValue(value).Value);

        builder.Property(country => country.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasKey(country => country.Id);

        builder.HasIndex(country => country.CountryCode).IsUnique();

        builder.OwnsMany(country => country.ParticipatingContestIds, subBuilder =>
        {
            subBuilder.ToTable("country_participating_contest");

            subBuilder.Property<int>("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            subBuilder.WithOwner()
                .HasForeignKey("CountryId")
                .HasPrincipalKey(country => country.Id);

            subBuilder.Property(id => id.Value)
                .HasColumnName("participating_contest_id")
                .IsRequired();

            subBuilder.HasKey("Id");

            subBuilder.HasIndex("CountryId", "Value").IsUnique();
        }).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(country => country.CompetingBroadcastIds, subBuilder =>
        {
            subBuilder.ToTable("country_competing_broadcast");

            subBuilder.Property<int>("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            subBuilder.WithOwner()
                .HasForeignKey("CountryId")
                .HasPrincipalKey(country => country.Id);

            subBuilder.Property(id => id.Value)
                .HasColumnName("competing_broadcast_id")
                .IsRequired();

            subBuilder.HasKey("Id");

            subBuilder.HasIndex("CountryId", "Value").IsUnique();
        }).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(country => country.VotingBroadcastIds, subBuilder =>
        {
            subBuilder.ToTable("country_voting_broadcast");

            subBuilder.Property<int>("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            subBuilder.WithOwner()
                .HasForeignKey("CountryId")
                .HasPrincipalKey(country => country.Id);

            subBuilder.Property(id => id.Value)
                .HasColumnName("voting_broadcast_id")
                .IsRequired();

            subBuilder.HasKey("Id");

            subBuilder.HasIndex("CountryId", "Value").IsUnique();
        }).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
