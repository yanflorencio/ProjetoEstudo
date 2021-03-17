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

		private readonly IDao<Jogo> _jogoDao;

		public JogoController(IDao<Jogo> jogoDao)
		{
			_jogoDao = jogoDao;
		}

		[HttpGet]
		public IActionResult GetAllJogos()
		{
			List<Jogo> listaJogos = _jogoDao.GetAll().ToList();
			return Ok(listaJogos);
		}

		[HttpPost]
		public IActionResult CadastrarJogo([FromBody] Jogo jogo)
		{
			if (ModelState.IsValid)
			{
				_jogoDao.Save(jogo);

				
				var uri = Url.Action("GetJogo", new { id = jogo.Id });
				return Created(uri, jogo); //201
			}

			return BadRequest();
		}

		[HttpGet("{id}")]
		public IActionResult GetJogo(long id)
		{
			Jogo jogo = _jogoDao.FindById(id);

			if(jogo == null)
			{
				return NotFound();
			}
			return Ok(jogo);
		}
	}
}
