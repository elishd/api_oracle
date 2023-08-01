using System.ComponentModel.DataAnnotations;

namespace DELSUR.WebApi.Request
{
    public class UpdateClienteRequest
    {
        [Required]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El {0}  es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El {0}  es obligatorio")]
        public double Salario { get; set; }
        
    }
}
