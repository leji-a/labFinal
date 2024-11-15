﻿using System.ComponentModel.DataAnnotations;

namespace lab4Final.Models
{
	public class Editorial
	{
		public int Id { get; set; }
		public string? Nombre { get; set; }
        [Display(Name = "País")]
        public int PaisId { get; set; }  
		
		// Navegación
		public Pais? Pais { get; set; }
		public ICollection<Libro>? Libros { get; set; }
	}
}
