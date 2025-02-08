using BackEnd_NET6.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_NET6.Controllers
{
    [Authorize]
    [ApiController]
    public class PlanosController : Controller
    {
        private readonly VendaContext _context;

        public PlanosController(VendaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/planos/listar")]
        public async Task<IActionResult> GetVendedores()
        {
            var planos = await _context.Planos.ToListAsync();
            return Ok(planos);            
        }
    }
}
