using System.ComponentModel.DataAnnotations;

namespace AppCursosCast.Models
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }


        [StringLength(40), Required]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; } = "";


        [StringLength(40), Required]
        [Display(Name = "Ação")]
        public string? Acao { get; set; }


        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        public Curso? Curso { get; set; }


        [Display(Name = "Data de inclusão do Curso")]
        public DateTime DataInclusao { get; set; }


        [Display(Name = "Ultima atualização do Curso")]
        public DateTime? DataAtualizacao { get; set; }
    }
}
