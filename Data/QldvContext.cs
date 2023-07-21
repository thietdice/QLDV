using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLDV.Data;

public partial class QldvContext : DbContext
{
    public QldvContext()
    {
    }

    public QldvContext(DbContextOptions<QldvContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventUser> EventUsers { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCatalogue> UserCatalogues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=qldv;Uid=root;Pwd=nhom7");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("articles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Pdf)
                .HasMaxLength(255)
                .HasColumnName("pdf");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UseridCreated).HasColumnName("userid_created");
            entity.Property(e => e.UseridUpdated).HasColumnName("userid_updated");
            entity.Property(e => e.Viewed).HasColumnName("viewed");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("classes");

            entity.HasIndex(e => e.FacultyId, "classes_ibfk_1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UseridCreated).HasColumnName("userid_created");
            entity.Property(e => e.UseridUpdated).HasColumnName("userid_updated");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Classes)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("classes_ibfk_1");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("events");

            entity.HasIndex(e => e.SemesterId, "semester_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DayEnd)
                .HasColumnType("date")
                .HasColumnName("day_end");
            entity.Property(e => e.DayStart)
                .HasColumnType("date")
                .HasColumnName("day_start");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasColumnType("text")
                .HasColumnName("image");
            entity.Property(e => e.Publish).HasColumnName("publish");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.SemesterId).HasColumnName("semester_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UseridCreated).HasColumnName("userid_created");
            entity.Property(e => e.UseridUpdated).HasColumnName("userid_updated");

            entity.HasOne(d => d.Semester).WithMany(p => p.Events)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("events_ibfk_1");
        });

        modelBuilder.Entity<EventUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("event_user");

            entity.HasIndex(e => e.UserId, "event_user_ibfk_1");

            entity.HasIndex(e => e.EventId, "event_user_ibfk_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Image)
                .HasColumnType("text")
                .HasColumnName("image");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");
            entity.Property(e => e.NoteReviewer)
                .HasMaxLength(255)
                .HasColumnName("note_reviewer");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Event).WithMany(p => p.EventUsers)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_user_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.EventUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_user_ibfk_1");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("faculties");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ShortTitle)
                .HasMaxLength(255)
                .HasColumnName("short_title");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UseridCreated).HasColumnName("userid_created");
            entity.Property(e => e.UseridUpdated).HasColumnName("userid_updated");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("semesters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DayEnd)
                .HasColumnType("date")
                .HasColumnName("day_end");
            entity.Property(e => e.DayStart)
                .HasColumnType("date")
                .HasColumnName("day_start");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UseridCreated).HasColumnName("userid_created");
            entity.Property(e => e.UseridUpdated).HasColumnName("userid_updated");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.ClassId, "users_ibfk_1");

            entity.HasIndex(e => e.UserCatalogueId, "users_ibfk_2_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DayInUnion)
                .HasColumnType("date")
                .HasColumnName("day_in_union");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Ethnic)
                .HasMaxLength(100)
                .HasColumnName("ethnic");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.Gender)
                .HasMaxLength(100)
                .HasColumnName("gender");
            entity.Property(e => e.IdCard)
                .HasMaxLength(100)
                .HasColumnName("id_card");
            entity.Property(e => e.IdStudent)
                .HasMaxLength(100)
                .HasColumnName("id_student");
            entity.Property(e => e.Image)
                .HasColumnType("text")
                .HasColumnName("image");
            entity.Property(e => e.LevelComputer)
                .HasMaxLength(100)
                .HasColumnName("level_computer");
            entity.Property(e => e.LevelEducation)
                .HasMaxLength(100)
                .HasColumnName("level_education");
            entity.Property(e => e.LevelLanguage)
                .HasMaxLength(100)
                .HasColumnName("level_language");
            entity.Property(e => e.LevelPolitics)
                .HasMaxLength(100)
                .HasColumnName("level_politics");
            entity.Property(e => e.LevelSpecialize)
                .HasMaxLength(100)
                .HasColumnName("level_specialize");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Profession)
                .HasMaxLength(100)
                .HasColumnName("profession");
            entity.Property(e => e.Religion)
                .HasMaxLength(5)
                .HasColumnName("religion");
            entity.Property(e => e.ResidenceAddress)
                .HasMaxLength(255)
                .HasColumnName("residence_address");
            entity.Property(e => e.ResidenceCity)
                .HasMaxLength(20)
                .HasColumnName("residence_city");
            entity.Property(e => e.ResidenceDistrict)
                .HasMaxLength(20)
                .HasColumnName("residence_district");
            entity.Property(e => e.ResidenceWard)
                .HasMaxLength(20)
                .HasColumnName("residence_ward");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserCatalogueId).HasColumnName("user_catalogue_id");
            entity.Property(e => e.UseridCreated).HasColumnName("userid_created");
            entity.Property(e => e.UseridUpdated).HasColumnName("userid_updated");

            entity.HasOne(d => d.Class).WithMany(p => p.Users)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_ibfk_1");

            entity.HasOne(d => d.UserCatalogue).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserCatalogueId)
                .HasConstraintName("users_ibfk_2");
        });

        modelBuilder.Entity<UserCatalogue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_catalogues");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
