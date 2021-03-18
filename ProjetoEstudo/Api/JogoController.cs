﻿using EstudoProjeto.Utils;
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

		[HttpPut]
		public IActionResult AlterarJogo([FromBody] Jogo jogo)
		{
			if (ModelState.IsValid)
			{
				_jogoDao.Update(jogo);

				return Ok(jogo);
			}

			return BadRequest();
		}

		[HttpGet("ByPlataforma/{plataforma:int}")]
		public IActionResult GetJogoByPlataforma(Enum.Plataforma plataforma)
		{
			List<Jogo> listaJogos = _jogoDao.GetAll()
											.Where(jogo => jogo.Plataforma == plataforma)
											.ToList();
			return Ok(listaJogos);
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

		[HttpDelete("{id}")]
		public IActionResult DeleteJogo(long id)
		{
			Jogo jogo = _jogoDao.FindById(id);

			if (jogo != null)
			{
				_jogoDao.Delete(jogo);
				return Ok(true); //Poderia retornar NoContent()
			}

			return BadRequest();
		}
	}
}
