using System.ComponentModel.DataAnnotations;
using AppCursosCast.Validations;

namespace AppCursosCast.Models
{
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }

        [Required(ErrorMessage = " O campo descrição é obrigatório")]
        [StringLength(400)]
        [Display(Name = "Descrição do curso")]
        [ValidaCursoExistente]
        public string? Descricao { get; set; }


        [Required(ErrorMessage = " O campo data inicial é obrigatório")]
        [Display(Name = "Data de início")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ValidaDataInicio]
        [ValidaDataDisponivel]
        public DateTime DataInicio { get; set; }


        [Required(ErrorMessage = " O campo data final é obrigatório")]
        [Display(Name = "Data de fim")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ValidaDataFinal]
        public DateTime DataFim { get; set; }


        [Display(Name = "Nº de Alunos")]
        public int? QuantidadeEstudantes { get; set; }


        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }


        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
