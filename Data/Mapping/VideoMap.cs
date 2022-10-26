using Aluraflix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aluraflix.Data;
public class VideoMap : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        //Tabela
        builder.ToTable("Video");

        //Primary Key
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
          .ValueGeneratedOnAdd()
          .UseIdentityColumn(); //identity(1,1)

        builder.Property(x => x.Title)
          .IsRequired()
          .HasColumnName("Title")
          .HasColumnType("NVARCHAR")
          .HasMaxLength(30);

        builder.Property(x => x.Description)
          .HasColumnName("Description")
          .HasColumnType("TEXT")
          .HasMaxLength(100);
        
        builder.Property(x => x.Url)
          .IsRequired()
          .HasColumnName("Url")
          .HasColumnType("NVARCHAR")
          .HasMaxLength(30);

        //Index
        builder.HasIndex(x => x.Title, "IX_Video_Title")
          .IsUnique();
    }
}
