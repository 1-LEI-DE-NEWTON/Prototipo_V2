using BackEnd_NET6.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_NET6.Controllers
{
    [Authorize]
    [ApiController]
    public class VendedoresController : Controller
    {
        private readonly VendaContext _context;

        public VendedoresController(VendaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/vendedores/listar")]
        public async Task<IActionResult> GetVendedores()
        {
            var vendedores = await _context.Vendedores.ToListAsync();
            return Ok(vendedores);            
        }
    }
}
