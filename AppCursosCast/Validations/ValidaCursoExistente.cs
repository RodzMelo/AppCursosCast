using AppCursosCast.Models;
using System.ComponentModel.DataAnnotations;

namespace AppCursosCast.Validations
{
    public class ValidaCursoExistente : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            CursosContext context = (CursosContext)validationContext.GetService(typeof(CursosContext));

            if (context == null)
            {
                return new ValidationResult("Não foi possível obter o contexto do banco de dados.");
            }

            string descricao = (string)value;

            var cursos = context.Curso.Where(c => descricao == c.Descricao); Convert.ToString(validationContext.ObjectInstance.GetType()
                .GetProperty("CursoId").GetValue(validationContext.ObjectInstance, null));

            if (cursos.Count() > 0)
            {
                return new ValidationResult("Este curso já foi cadastrado!");
            }
            else
            {
                return null;
            }
        }
    }
}