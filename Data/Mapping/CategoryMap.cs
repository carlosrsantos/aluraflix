using Aluraflix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aluraflix.Data.Mapping;
public class CategoryMap : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    builder.ToTable("Category");

    builder.HasKey(c => c.Id);

    builder.Property(c => c.Id)
        .ValueGeneratedOnAdd()
        .UseIdentityColumn(); 

    builder.Property(c => c.Title)
        .IsRequired()
        .HasColumnName("Title")
        .HasColumnType("NVARCHAR")
        .HasMaxLength(30);

    builder.Property(c => c.Color)
        .IsRequired()
        .HasColumnName("Color")
        .HasColumnType("NVARCHAR")
        .HasMaxLength(30);

    builder.HasIndex(c => c.Color, "IX_Category_Color")
        .IsUnique();

  }
}