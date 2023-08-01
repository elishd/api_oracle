using System.ComponentModel.DataAnnotations;

namespace DELSUR.WebApi.Request
{
    public class GetByIdClienteRequest
    {
        [Required]
        public int Codigo { get; set; }
    }
}
