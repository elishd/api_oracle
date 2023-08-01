using System.ComponentModel.DataAnnotations;

namespace DELSUR.WebApi.Request
{
    public class CreateClienteRequest
    {

        [Required (ErrorMessage = "El {0}  es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El {0} es obligatorio")]
        [MaxLength (300, ErrorMessage ="La longitud maxima del {0} es {1}")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El {0}  es obligatorio")]
        public double Salario { get; set; }
        [Required(ErrorMessage = "El {0}  es obligatorio")]
        public int Edad { get; set; }
    }
}
