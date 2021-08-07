using EstudoProjeto.Utils;
using Microsoft.AspNetCore.Mvc;
using ProjetoEstudo.Model;
using ProjetoEstudo.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoEstudo.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class JogoController : ControllerBase
	{

		private readonly IJogoService _jogoService;

		public JogoController(IJogoService jogoService)
		{
			_jogoService = jogoService;
		}

		[HttpGet]
		public IActionResult GetAllJogos()
		{
			IList<Jogo> listaJogos = _jogoService.GetJogos();
			return Ok(listaJogos);
		}

		[HttpPost]
		public IActionResult CadastrarJogo([FromBody] Jogo jogo)
		{
			if (ModelState.IsValid)
			{
				_jogoService.CadastrarJogo(jogo);

				
				var uri = Url.Action("GetJogo", new { id = jogo.Id });
				return Created(uri, jogo); //201
			}

			return BadRequest();
		}

		[HttpPut]
		public IActionResult AlterarJogo([FromBody] Jogo jogo)
		{
			if (ModelState.IsValid)
			{
				_jogoService.AlterarJogo(jogo);

				return Ok(jogo);
			}

			return BadRequest();
		}

		[HttpGet("ByPlataforma/{plataforma:int}")]
		public IActionResult GetJogoByPlataforma(Enum.Plataforma plataforma)
		{
			IList<Jogo> listaJogos = _jogoService.GetJogoByPlataforma(plataforma);

			if (listaJogos.Any())
			{
				return Ok(listaJogos);
			}

			return NotFound();
		}

		[HttpGet("{id}")]
		public IActionResult GetJogo(long id)
		{
			Jogo jogo = _jogoService.GetById(id);

			if(jogo == null)
			{
				return NotFound("Nao Encontrado");
			}
			return Ok(jogo);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteJogo(long id)
		{
			_jogoService.DeletarJogo(id);

			return Ok();
		}

		[HttpGet("GetJogosDisponiveis")]
		public IActionResult GetJogosDisponiveis()
		{
			IList<Jogo> listaJogos = _jogoService.GetJogosDisponiveis();
			return Ok(listaJogos);
		}
	}
}
