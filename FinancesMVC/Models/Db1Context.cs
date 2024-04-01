using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Tracing;

namespace FinancesMVC.Models;

public partial class Db1Context : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public Db1Context(DbContextOptions<Db1Context> options) : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<SharedBudget> SharedBudgets { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Important!

        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__achievem__3213E83F0637D22E");

            entity.ToTable("achievements");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Status)
                .HasDefaultValue(false)
                .HasColumnName("status");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F930CEC60");

            entity.ToTable("categories");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ExpenditureLimit).HasColumnName("expenditureLimit");
            entity.Property(e => e.IsParentControl)
                .HasDefaultValue(false)
                .HasColumnName("isParentControl");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Categories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__categorie__userI__4D94879B")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__messages__3213E83F01993062");

            entity.ToTable("messages");

            entity.HasIndex(e => e.Type, "UQ__messages__E3F852480E6B4C46").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        modelBuilder.Entity<SharedBudget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__shared_b__3213E83F19DEC5E4");

            entity.ToTable("sharedBudget");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AddedUsersId).HasColumnName("addedUserId");
            entity.Property(e => e.CommonCategoryId).HasColumnName("commonCategoryId");

            entity.HasOne(d => d.OwnerUser)
          .WithMany(p => p.SharedBudgets)
          .HasForeignKey(d => d.OwnerId)
          .OnDelete(DeleteBehavior.ClientSetNull)
          .HasConstraintName("FK_sharedBudget_OwnerUser");

            entity.HasOne(d => d.CommonCategory).WithMany(p => p.SharedBudgets)
                .HasForeignKey(d => d.CommonCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sharedBudget_categories");

        });

        modelBuilder.Entity<Stat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__stats__3213E83F27BC9641");

            entity.ToTable("stats");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CalculatedExpances).HasColumnName("calculatedExpances");
            entity.Property(e => e.ChosenCategoryId).HasColumnName("chosenCategoryId");
            entity.Property(e => e.EndTime).HasColumnName("endTime");
            entity.Property(e => e.StartTime).HasColumnName("startTime");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.ChosenCategory).WithMany(p => p.Stats)
                .HasForeignKey(d => d.ChosenCategoryId)
                .HasConstraintName("FK__stats__chosenCat__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.Stats)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__stats__userId__4CA06362")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__transact__3213E83FC90F2F93");

            entity.ToTable("transactions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.BudgetOverflown)
                .HasDefaultValue(false)
                .HasColumnName("budgetOverflown");
            entity.Property(e => e.CompletedAchievementId).HasColumnName("completedAchievementId");
            entity.Property(e => e.ExpenditureCategoryId).HasColumnName("expenditureCategoryId");
            entity.Property(e => e.ExpenditureNote)
                .HasMaxLength(255)
                .HasColumnName("expenditureNote");
            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.MoneySpent).HasColumnName("moneySpent");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.CompletedAchievement).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CompletedAchievementId)
                .HasConstraintName("FK_transactions_achievements");

            entity.HasOne(d => d.ExpenditureCategory).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ExpenditureCategoryId)
                .HasConstraintName("FK__transacti__expen__4BAC3F29")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Message).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.MessageId)
                .HasConstraintName("FK_transactions_messages");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__transacti__userI__48CFD27E")
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F0DCBB468");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.IsMature).HasColumnName("isMature");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserProf__3213E83F7E505319");

            entity.ToTable("userProfiles");

            entity.HasIndex(e => e.Id, "UQ_UserProfiles_id").IsUnique();

            entity.HasIndex(e => e.UserId, "UQ_UserProfiles_userId").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AboutMe)
                .HasColumnType("text")
                .HasColumnName("aboutMe");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithOne(p => p.UserProfile)
                .HasForeignKey<UserProfile>(d => d.UserId)
                .HasConstraintName("FK_UserProfiles_Users")
                .OnDelete(DeleteBehavior.SetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
