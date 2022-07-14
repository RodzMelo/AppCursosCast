using System.ComponentModel.DataAnnotations;


namespace AppCursosCast.Validations
{

    public class ValidaDataInicio : ValidationAttribute
    {
        public ValidaDataInicio() : base("Data de início não pode ser menor que a data atual")
        {
            
        }
        
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime dataInicio = (DateTime)value;
            if (dataInicio.Date < DateTime.Now.Date)
            {
                return new ValidationResult("Data de início não pode ser menor que a data atual");
            }

            return ValidationResult.Success;
        }


    }
}
