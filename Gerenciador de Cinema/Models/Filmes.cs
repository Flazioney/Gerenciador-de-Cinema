using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Gerenciador_de_Cinema.Models
{

    public class Filmes
    {
        [Key]
        public int id_filme { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Duração Filme")]
        //[DataType(DataType.Time)]
        public TimeSpan duracao { get; set; }

        
        public byte[] Dados { get; set; }

        // public string ContentType { get; set; }

        public string ContentType { get; set; }
        public string Extension { get; set; }
        public int Length { get; set; }

    }
    public class ImagemMap : IEntityTypeConfiguration<Filmes>
    {
        public void Configure(EntityTypeBuilder<Filmes> builder)
        {
            builder.ToTable("Filmes");

            builder.HasKey(x => x.id_filme);

            builder.Property(x => x.id_filme);

            builder.Property(x => x.Titulo)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Extension)
                .HasMaxLength(4)
                .IsRequired();

            builder.Property(x => x.Length)
                .IsRequired();

            builder.Property(x => x.Dados);

            builder.Property(x => x.ContentType)
                .IsRequired()
                .HasMaxLength(20);
        }

        public static ImagemMap Create() => new ImagemMap();
    }
    public static class Utils
    {
        public static string GetExtension(this IFormFile file)
        {
            return Path.GetExtension(file.FileName);
        }
        public static byte[] ToByteArray(this IFormFile file)
        {
            using (BinaryReader reader = new BinaryReader(file.OpenReadStream()))
            {
                return reader.ReadBytes((int)file.Length);
            }
        }
    }
}

