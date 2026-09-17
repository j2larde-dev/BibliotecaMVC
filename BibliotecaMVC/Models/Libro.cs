using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Models
{
    public class Libro
    {
        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Autor { get; set; } = string.Empty;

        [StringLength(50)]
        public string Categoria { get; set; } = string.Empty;

        [Precision(10, 2)]
        public decimal Precio { get; set; }

        public bool Disponible { get; set; }
    }
}
