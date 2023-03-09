using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SIP.SurveyMaker.PL;

public partial class SurveyMakerEntities : DbContext
{
    public SurveyMakerEntities()
    {
    }

    public SurveyMakerEntities(DbContextOptions<SurveyMakerEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<tblActivation> tblActivations { get; set; }

    public virtual DbSet<tblAnswer> tblAnswers { get; set; }

    public virtual DbSet<tblQuestion> tblQuestions { get; set; }

    public virtual DbSet<tblQuestionAnswer> tblQuestionAnswers { get; set; }

    public virtual DbSet<tblResponse> tblResponses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SIP.SurveyMaker.DB;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblActivation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblActiv__3214EC074C379CEA");

            entity.ToTable("tblActivation");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActivationCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.tblActivations)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblActiva__Quest__2B3F6F97");
        });

        modelBuilder.Entity<tblAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblAnswe__3214EC07C30EAEBF");

            entity.ToTable("tblAnswer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Text)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblQuest__3214EC078438E2E6");

            entity.ToTable("tblQuestion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Text)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblQuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblQuest__3214EC07DD4E0B97");

            entity.ToTable("tblQuestionAnswer");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Answer).WithMany(p => p.tblQuestionAnswers)
                .HasForeignKey(d => d.AnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkAnswerId");

            entity.HasOne(d => d.Question).WithMany(p => p.tblQuestionAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkQuestionId");
        });

        modelBuilder.Entity<tblResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblRespo__3214EC0765E48732");

            entity.ToTable("tblResponse");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ResponseDate).HasColumnType("datetime");

            entity.HasOne(d => d.Answer).WithMany(p => p.tblResponses)
                .HasForeignKey(d => d.AnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblRespon__Answe__2F10007B");

            entity.HasOne(d => d.Question).WithMany(p => p.tblResponses)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblRespon__Quest__2E1BDC42");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
