using System.ComponentModel.DataAnnotations;

namespace DELSUR.WebApi.Request
{
    public class DeleteClienteRequest
    {
        [Required]
        public int Codigo { get; set; }
    }
}
