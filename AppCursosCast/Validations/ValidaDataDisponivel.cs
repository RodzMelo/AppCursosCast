using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AppCursosCast.Validations
{
    public class ValidaDataDisponivel : ValidationAttribute
    {
        public ValidaDataDisponivel() : base("Existe(m) curso(s) planejados(s) dentro do período informado.")
        {

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            CursosContext context = (CursosContext)validationContext.GetService(typeof(CursosContext));
            
            if (context == null)
            {
                return new ValidationResult("Não foi possível obter o contexto do banco de dados.");
            }
            else
            {
                DateTime dataInicio = (DateTime)value;
                var cursos = context.Curso.Where(c => dataInicio >= c.DataInicio && dataInicio <= c.DataFim && c.CursoId != Convert.ToInt32(validationContext.ObjectInstance.GetType().GetProperty("CursoId").GetValue(validationContext.ObjectInstance, null)));

                if (cursos.Count() > 0)
                {
                    return new ValidationResult("Existe(m) curso(s) planejados(s) dentro do período informado.");
                }
                else
                {
                    return null;
                }
            }

        }
    }
}
