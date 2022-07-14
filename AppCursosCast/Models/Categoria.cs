using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCursosCast.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }


        [Required(ErrorMessage = " O campo categoria é obrigatório")]
        [StringLength(40)]
        [Display(Name = "Nome da categoria")]
        public string? Nome { get; set; }


        [StringLength(500)]
        public string? ImagemUrl { get; set; }

        [Display(Name = "Imagem")]
        [NotMapped]
        public IFormFile? Imagem { get; set; }
    }
}
