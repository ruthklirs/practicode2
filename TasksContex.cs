using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi;

public partial class TasksContex : DbContext
{
    public TasksContex()
    {
    }

    public TasksContex(DbContextOptions<TasksContex> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=bn0bln5q8nacs0m5yeam-mysql.services.clever-cloud.com;user=uqet0quzmctqv8rt;password=XHQ6S4iuup13k8umadaa;database=bn0bln5q8nacs0m5yeam", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("items");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}








