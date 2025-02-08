using Microsoft.AspNetCore.Mvc;
using BackEnd_NET6.Data;
using BackEnd_NET6.Models;
using BackEnd_NET6.Models.DTOs;
using BackEnd_NET6.Services.Interfaces;

namespace BackEnd_NET6.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly VendaContext _context;

        private readonly I_JWT_Service _jwtService;                

        public AuthController(VendaContext context, I_JWT_Service jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }
        
        [HttpPost]
        [Route("api/login")]
        
        public IActionResult Login([FromBody] LoginDTO login)
        {    
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Dados inválidos");
            }
            
            // Verificar se as credenciais do usuário estão corretas            
            var usuario = _context.Usuarios
                                   .FirstOrDefault(u => u.Username == login.Username);

            if (usuario == null || usuario.Password != login.Password)
            {
                return Unauthorized("Credenciais inválidas");
            }

            // Gerar token JWT
            var token = _jwtService.GenerateJwtToken(login.Username);            

            return Ok( new
            {
                Message = "Autenticado com sucesso",
                Token = token
            });
        }            
    }
}
