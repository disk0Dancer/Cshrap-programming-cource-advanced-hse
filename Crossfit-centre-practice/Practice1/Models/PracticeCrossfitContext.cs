using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Practice1;

public partial class PracticeCrossfitContext : DbContext
{
    public PracticeCrossfitContext()
    {
    }

    public PracticeCrossfitContext(DbContextOptions<PracticeCrossfitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Tariff> Tariffs { get; set; }

    public virtual DbSet<Timetable> Timetables { get; set; }

    public virtual DbSet<Training> Training { get; set; }

    public virtual DbSet<TrainingFormat> TrainingFormats { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;database=practice_crossfit;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("gender");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("subscriptions");

            entity.HasIndex(e => e.TariffId, "tariff_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            entity.Property(e => e.TariffId)
                .HasColumnType("int(11)")
                .HasColumnName("tariff_id");
            entity.Property(e => e.TrainingsRest)
                .HasColumnType("int(11)")
                .HasColumnName("trainings_rest");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Tariff).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.TariffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subscriptions_ibfk_1");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("subscriptions_ibfk_2");
        });

        modelBuilder.Entity<Tariff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tariff");

            entity.HasIndex(e => e.TrainingFormatId, "training_format_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("int(11)")
                .HasColumnName("price");
            entity.Property(e => e.TrainingFormatId)
                .HasColumnType("int(11)")
                .HasColumnName("training_format_id");

            entity.HasOne(d => d.TrainingFormat)
                .WithMany(p => p.Tariffs)
                .HasForeignKey(d => d.TrainingFormatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tariff_ibfk_1");
        });

        modelBuilder.Entity<Timetable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("timetable");

            entity.HasIndex(e => e.TrainingId, "timetable_ibfk_1");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.End)
                .HasColumnType("datetime")
                .HasColumnName("end");
            entity.Property(e => e.Start)
                .HasColumnType("datetime")
                .HasColumnName("start");
            entity.Property(e => e.TrainingId)
                .HasColumnType("int(11)")
                .HasColumnName("training_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Training).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.TrainingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timetable_ibfk_1");

            entity.HasOne(d => d.User).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timetable_ibfk_2");

            entity.HasMany(d => d.Subscriptions).WithMany(p => p.Timetables)
                .UsingEntity<Dictionary<string, object>>(
                    "TimetableHasSubscription",
                    r => r.HasOne<Subscription>().WithMany()
                        .HasForeignKey("SubscriptionId")
                        .HasConstraintName("timetable_has_subscription_ibfk_1"),
                    l => l.HasOne<Timetable>().WithMany()
                        .HasForeignKey("TimetableId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("timetable_has_subscription_ibfk_2"),
                    j =>
                    {
                        j.HasKey("TimetableId", "SubscriptionId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("timetable_has_subscription");
                        j.HasIndex(new[] { "SubscriptionId" }, "subscription_id");
                        j.IndexerProperty<int>("TimetableId")
                            .HasColumnType("int(11)")
                            .HasColumnName("timetable_id");
                        j.IndexerProperty<int>("SubscriptionId")
                            .HasColumnType("int(11)")
                            .HasColumnName("subscription_id");
                    });
        });

        modelBuilder.Entity<Training>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("training");

            entity.HasIndex(e => e.TrainingFormatId, "training_format_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Author)
                .HasColumnType("text")
                .HasColumnName("author");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Duration)
                .HasDefaultValueSql("'60'")
                .HasColumnType("int(11)")
                .HasColumnName("duration");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.TrainingFormatId)
                .HasColumnType("int(11)")
                .HasColumnName("training_format_id");

            entity.HasOne(d => d.TrainingFormat).WithMany(p => p.Training)
                .HasForeignKey(d => d.TrainingFormatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("training_ibfk_1");
        });

        modelBuilder.Entity<TrainingFormat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("training_format");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.MaxClients)
                .HasColumnType("int(11)")
                .HasColumnName("max_clients");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.GenderId, "gender_id");

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.BirthDate)
                .HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasColumnType("text")
                .HasColumnName("email");
            entity.Property(e => e.Fio)
                .HasColumnType("text")
                .HasColumnName("fio");
            entity.Property(e => e.GenderId)
                .HasColumnType("int(11)")
                .HasColumnName("gender_id");
            entity.Property(e => e.HealthFeatures).HasColumnName("health_features");
            entity.Property(e => e.PasswordHash)
                .HasColumnType("text")
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasColumnType("text")
                .HasColumnName("password_salt");
            entity.Property(e => e.Phone)
                .HasColumnType("text")
                .HasColumnName("phone");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.SportAchivements)
                .HasColumnType("text")
                .HasColumnName("sport_achivements");
            entity.Property(e => e.TrainingExpereince)
                .HasColumnType("int(11)")
                .HasColumnName("training_expereince");

            entity.HasOne(d => d.Gender)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_ibfk_2");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
