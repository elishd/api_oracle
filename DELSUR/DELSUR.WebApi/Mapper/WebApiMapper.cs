using AutoMapper;
using DELSUR.WebApi.Model;
using DELSUR.WebApi.Request;
using DELSUR.WebApi.Response;

namespace DELSUR.WebApi.Mapper
{
    public class WebApiMapper: Profile
    {
        public WebApiMapper()
        {
           // REQUEST
            CreateMap<CreateClienteRequest, Cliente>();
            CreateMap<DeleteClienteRequest, Cliente>();
            CreateMap<GetByIdClienteRequest, Cliente>();
            CreateMap<UpdateClienteRequest, Cliente>();

            // RESPONSE
            CreateMap<Cliente, ClienteRessponse>();
        }
    }
}
