using System.ComponentModel.DataAnnotations;

namespace AppCursosCast.Validations
{
    public class ValidaDataFinal : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dataInicial = (DateTime)validationContext.ObjectInstance.GetType().GetProperty("DataInicio").GetValue(validationContext.ObjectInstance, null);
            var dataFinal = (DateTime)value;
            if (dataFinal < dataInicial)
            {
                return new ValidationResult("Data final deve ser maior que data inicial");
            }
            return ValidationResult.Success;
        }
    }
}
