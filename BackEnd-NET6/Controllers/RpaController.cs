using BackEnd_NET6.Models;
using BackEnd_NET6.Models.DTOs;
using BackEnd_NET6.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BackEnd_NET6.Controllers
{

    [ApiController]
    [Authorize]    
    public class RpaController : Controller
    {
        private readonly I_Venda_Service _vendaService;
        private readonly I_VendaStatus_Service _vendaStatusService;

        public RpaController(I_Venda_Service vendaService, I_VendaStatus_Service vendaStatusService)
        {
            _vendaService = vendaService;
            _vendaStatusService = vendaStatusService;
        }

        [HttpGet]
        [Route("api/rpa/obter-fila-vendas")]

        public IActionResult ObterFilaVendas()
        {
            var vendas = _vendaService.ListarVendasNaFila();

            if (vendas == null)
            {
                return NotFound(new
                {
                    mensagem = "Nenhuma venda na fila"
                });
            }

            return Ok(vendas);
        }        

        [HttpPut]
        [Route("api/rpa/atualizar-status-venda/{id}")]

        public IActionResult AtualizarStatusVenda(int id, [FromBody] VendaStatusDTO vendaStatusDTO)
        {
            try
            {   
                _vendaStatusService.AtualizarVendaStatus(id, vendaStatusDTO);         
                return Ok(new
                {
                    mensagem = "Status da venda atualizado com sucesso"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    mensagem = "Erro ao atualizar status da venda: " + e.Message
                });
            }
        }                
    }
}
