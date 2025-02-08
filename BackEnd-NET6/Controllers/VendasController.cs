using BackEnd_NET6.Data;
using BackEnd_NET6.Models.DTOs;
using BackEnd_NET6.Services;
using BackEnd_NET6.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_NET6.Controllers
{
    [ApiController]
    [Authorize]
    public class VendasController : Controller
    {
        private readonly I_Venda_Service _vendaService;

        private readonly I_Validar_CPF_Service _validar_CPF_Service;

        public VendasController(I_Venda_Service vendaService, I_Validar_CPF_Service validar_CPF_Service)
        {
            _vendaService = vendaService;
            _validar_CPF_Service = validar_CPF_Service;
        }        

        [HttpPost]

        [Route("api/vendas/adicionar")]

        public IActionResult AdicionarVenda([FromBody] VendaDTO vendaDTO)
        {            
            
            if (string.IsNullOrEmpty(vendaDTO.NomeCliente) ||
            string.IsNullOrEmpty(vendaDTO.Telefone) ||                    
            string.IsNullOrEmpty(vendaDTO.Email) ||
            string.IsNullOrEmpty(vendaDTO.CPF) ||
            string.IsNullOrEmpty(vendaDTO.RG) ||
            string.IsNullOrEmpty(vendaDTO.CEP) ||
            string.IsNullOrEmpty(vendaDTO.Endereco) ||
            string.IsNullOrEmpty(vendaDTO.Numero) ||
            string.IsNullOrEmpty(vendaDTO.Pdv) ||             
            string.IsNullOrEmpty(vendaDTO.Complemento))
            {
                return BadRequest("Todos os campos são obrigatórios");
            }            
                        
            else
            {

                if (!_validar_CPF_Service.Validar_CPF(vendaDTO.CPF))
                {
                    return BadRequest(new{
                            mensagem = "CPF inválido"
                        });
                }
                
                try
                {
                    _vendaService.AdicionarVenda(vendaDTO);
                    return Ok( new{
                        mensagem = "Venda adicionada com sucesso"
                    });
                }
                catch (Exception e)
                {
                    return BadRequest("Erro ao adicionar venda: " + e.Message);
                }
            }
        }

        [HttpGet]

        [Route("api/servicos/listar")]

        public IActionResult ListarVendas()
        {
            return Ok(_vendaService.ListarVendas());
        }

        [HttpGet]

        [Route("api/search/nome/{nome}")]

        public IActionResult PesquisarVendasPorNome(string nome)
        {
            return Ok(_vendaService.PesquisarVendasPorNome(nome));
        }

        [HttpGet]

        [Route("api/search/cpf/{cpf}")]

        public IActionResult PesquisarVendaPorCPF(string cpf)
        {
            return Ok(_vendaService.PesquisarVendaPorCPF(cpf));
        }

        [HttpGet]
        [Route("api/validate/cpf/{cpf}")]

        public IActionResult ValidarCPF(string cpf)
        {
            try
            {
                //Valida o CPF usando o 4devs
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://www.4devs.com.br/");

                var payload = new Dictionary<string, string>
                {
                    { "acao", "validar_cpf" },
                    { "txt_cpf", cpf }
                };

                var content = new FormUrlEncodedContent(payload);

                var response = client.PostAsync("ferramentas_online.php", content).Result;

                var responseString = response.Content.ReadAsStringAsync().Result;                

                if (responseString.Contains("Verdadeiro"))
                {
                    return Ok(new { mensagem = "CPF válido" });
                }
                else
                {
                    return BadRequest(new { mensagem = "CPF inválido" });
                }
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao validar CPF: " + e.Message);
            }
        }

        [HttpGet]

        [Route("api/search/telefone/{telefone}")]

        public IActionResult PesquisarVendaPorTelefone(string telefone)
        {
            return Ok(_vendaService.PesquisarVendaPorTelefone(telefone));
        }
        
        [HttpGet]

        [Route("api/search/id/{id}")]

        public IActionResult PesquisarVendaPorId(int id)
        {
            return Ok(_vendaService.PesquisarVendaPorId(id));
        }

        [HttpPut]

        [Route("api/vendas/atualizar/{id}")]

        public IActionResult AtualizarVenda(int id, [FromBody] VendaDTO vendaDTO)
        {
            if (string.IsNullOrEmpty(vendaDTO.NomeCliente) ||
            string.IsNullOrEmpty(vendaDTO.Telefone) ||                        
            string.IsNullOrEmpty(vendaDTO.Email) ||
            string.IsNullOrEmpty(vendaDTO.CPF) ||
            string.IsNullOrEmpty(vendaDTO.RG) ||
            string.IsNullOrEmpty(vendaDTO.CEP) ||
            string.IsNullOrEmpty(vendaDTO.Endereco) ||
            string.IsNullOrEmpty(vendaDTO.Numero) ||
            string.IsNullOrEmpty(vendaDTO.Complemento))
            {
                return BadRequest("Todos os campos são obrigatórios");
            }
            else
            {
                if (!_validar_CPF_Service.Validar_CPF(vendaDTO.CPF))
                {
                    return BadRequest(new{
                            mensagem = "CPF inválido"
                        });
                }
                try
                {
                    _vendaService.AtualizarVenda(id, vendaDTO);
                    return Ok(new{
                        mensagem = "Venda atualizada com sucesso"
                    });
                }
                catch (Exception e)
                {
                    return BadRequest("Erro ao atualizar venda: " + e.Message);
                }
            }
        }                    
    }
}
