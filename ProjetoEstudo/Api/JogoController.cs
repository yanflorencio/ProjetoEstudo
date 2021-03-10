using Microsoft.AspNetCore.Mvc;
using ProjetoEstudo.Dao;
using ProjetoEstudo.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoEstudo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class JogoController : ControllerBase
	{
		private readonly BancoContext _context;

		public JogoController(BancoContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAllJogos()
		{
			List<Jogo> listaJogos = _context.Jogo.ToList();
			return Ok(listaJogos);
		}
	}
}
