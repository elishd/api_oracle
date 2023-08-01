using AutoMapper;
using Dapper;
using DELSUR.WebApi.Model;
using DELSUR.WebApi.Request;
using DELSUR.WebApi.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Transactions;

namespace DELSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IConfiguration _configuracion;
        private IMapper _mapper;

        public ClienteController(IConfiguration configuracion, IMapper mapper)
        {
            this._configuracion = configuracion;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCliente()
        {
            try
            {
                using var con = new OracleConnection(_configuracion["ConnectionStrings:Oracle"]);

                string query = "SELECT * FROM CLIENTE";
                var dato = _mapper.Map<List<ClienteRessponse>>(await con.QueryAsync<Cliente>(query));

                return Ok(new GenericResponse(0, "OK", dato));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse(1, e.Message, null));
            }

        }


        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetByIdAllCliente(int codigo)
        {
            try
            {
                using var con = new OracleConnection(_configuracion["ConnectionStrings:Oracle"]);
                var query = @"SELECT * FROM CLIENTE WHERE CODIGO= :Codigo";


                var dato = _mapper.Map<ClienteRessponse>(await con.QueryFirstOrDefaultAsync<Cliente>(query, new { Codigo = codigo }));

                return Ok(new GenericResponse(0, "OK", dato));

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse(1, e.Message, null));
            }


        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCliente(int codigo)
        {
            try
            {
                using var conection = new OracleConnection(_configuracion["ConnectionStrings:Oracle"]);
                var query = @"DELETE FROM CLIENTE WHERE CODIGO=:Codigo";

                var dato = await conection.ExecuteAsync(query, new { Codigo = codigo });

                return Ok(new GenericResponse(0, "OK", dato));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse(1, e.Message, null));
            }


        }

        //[HttpPost]
        //public async Task<IActionResult> CreateCliente(CreateClienteRequest data)
        //{

        //    try
        //    {
        //        using (TransactionScope trans = new())
        //        {
        //            using var cn = new OracleConnection(_configuracion["ConnectionStrings:Oracle"]);
        //            var querySecuencia = @"SELECT  Cliente_seq.NEXTVAL  FROM dual ";
        //            var query = @"INSERT INTO CLIENTE (CODIGO,NOMBRE,DIRECCION,SALARIO,EDAD) VALUES (:Codigo,:Nombre,:Direccion, :Salario, :Edad)  ";

        //            var codigo = await cn.QueryFirstOrDefaultAsync<int>(querySecuencia);

        //            var dato = await cn.ExecuteAsync(query, new { Codigo = codigo, data.Nombre, data.Direccion, data.Salario, data.Edad });

        //            trans.Complete();

        //            return Ok(new GenericResponse(0, "OK", codigo));
        //        };
        //    }
        //    catch (Exception e)
        //    {        
        //        return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse(0, e.Message, null));
        //    }
        //}


        [HttpPost]
        public async Task<IActionResult>  SpCreateCliente(CreateClienteRequest data)
        {
            try
            {            
                    using var cn = new OracleConnection(_configuracion["ConnectionStrings:Oracle"]);

                    var query = @"InsertarCliente ";

                var parameters = new DynamicParameters();
                parameters.Add("Nombre", data.Nombre);
                parameters.Add("Direccion", data.Direccion);
                parameters.Add("Salario", data.Salario);
                parameters.Add("Edad", data.Edad);
                parameters.Add("Codigo", dbType: DbType.Decimal, direction: ParameterDirection.Output);

                var dato = await cn.ExecuteAsync(query, parameters,commandType: CommandType.StoredProcedure);
                decimal codigoGenerado = parameters.Get<decimal>("Codigo");

                return Ok(new GenericResponse(0, "OK", codigoGenerado));
             
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse(0, e.Message, null));
            }
        }

    }
}
