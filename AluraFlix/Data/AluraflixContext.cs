using Aluraflix.Data.Mapping;
using Aluraflix.Models;
using Microsoft.EntityFrameworkCore;

namespace Aluraflix.Data;

public class AluraflixContext : DbContext
{
  public AluraflixContext(DbContextOptions<AluraflixContext> options)
    : base(options)
  {

  }

  public DbSet<Video> Videos { get; set; } = null!;

  public DbSet<Category> Categories { get; set; } = null!;
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new VideoMap());
    modelBuilder.ApplyConfiguration(new CategoryMap());
  }
}