using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Demo_LINQ.Models;

public partial class MailDemoDbContext : DbContext
{
    public MailDemoDbContext()
    {
    }

    public MailDemoDbContext(DbContextOptions<MailDemoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmailAttachment> EmailAttachments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MANAS-PC\\SQLEXPRESS;Database=MailDemoDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailAttachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailAtt__3214EC0756CFD755");

            entity.Property(e => e.SenderMailId).HasMaxLength(100);
            entity.Property(e => e.Subject).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
