using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SIP.SurveyMaker.PL;

public partial class SurveyEntities : DbContext
{
    public SurveyEntities()
    {
    }

    public SurveyEntities(DbContextOptions<SurveyEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<tblAnswer> tblAnswers { get; set; }

    public virtual DbSet<tblQuestion> tblQuestions { get; set; }

    public virtual DbSet<tblQuestionAnswer> tblQuestionAnswers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SIP.SurveyMaker.DB;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblAnswe__3214EC07591D7527");

            entity.ToTable("tblAnswer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Answer)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblQuest__3214EC07709D4961");

            entity.ToTable("tblQuestion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Question)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblQuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblQuest__3214EC078B6E6B19");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
