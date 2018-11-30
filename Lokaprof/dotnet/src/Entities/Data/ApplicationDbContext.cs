namespace WebApplication.Entities.Data {
  using System;
  using System.Collections.Generic;
  using Microsoft.EntityFrameworkCore;
  using WebApplication.Entities.Models;

  public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
      : base(options) {}

    public DbSet<Course> Courses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
    }
  }
}
