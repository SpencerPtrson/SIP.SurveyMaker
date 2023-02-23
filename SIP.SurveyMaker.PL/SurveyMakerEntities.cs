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
            entity.HasKey(e => e.Id).HasName("PK__tblAnswe__3214EC079DF80874");

            entity.ToTable("tblAnswer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Text)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblQuest__3214EC07BDCF30BA");

            entity.ToTable("tblQuestion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Text)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblQuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblQuest__3214EC07FE641618");

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
