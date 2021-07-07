﻿using EstudoProjeto.Utils;
using Microsoft.AspNetCore.Mvc;
using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoEstudo.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class JogoController : ControllerBase
	{

		private readonly IJogoDao _jogoRepositorio;

		public JogoController(IJogoDao jogoRepositorio)
		{
			_jogoRepositorio = jogoRepositorio;
		}

		[HttpGet]
		public IActionResult GetAllJogos()
		{
			List<Jogo> listaJogos = _jogoRepositorio.GetAll().ToList();
			return Ok(listaJogos);
		}

		[HttpPost]
		public IActionResult CadastrarJogo([FromBody] Jogo jogo)
		{
			if (ModelState.IsValid)
			{
				_jogoRepositorio.Save(jogo);

				
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
				_jogoRepositorio.Update(jogo);

				return Ok(jogo);
			}

			return BadRequest();
		}

		[HttpGet("ByPlataforma/{plataforma:int}")]
		public IActionResult GetJogoByPlataforma(Enum.Plataforma plataforma)
		{
			List<Jogo> listaJogos = _jogoRepositorio.GetAll()
											.Where(jogo => jogo.Plataforma == plataforma)
											.ToList();

			if (listaJogos.Any())
			{
				return Ok(listaJogos);
			}

			return NotFound();
		}

		[HttpGet("{id}")]
		public IActionResult GetJogo(long id)
		{
			Jogo jogo = _jogoRepositorio.FindById(id);

			if(jogo == null)
			{
				return NotFound();
			}
			return Ok(jogo);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteJogo(long id)
		{
			Jogo jogo = _jogoRepositorio.FindById(id);

			if (jogo != null)
			{
				_jogoRepositorio.Delete(jogo);
				return Ok(true); //Poderia retornar NoContent()
			}

			return BadRequest();
		}

		[HttpGet("GetJogosDisponiveis")]
		public IActionResult GetJogosDisponiveis()
		{
			IList<Jogo> listaJogos = _jogoRepositorio.GetJogosDisponiveis();
			return Ok(listaJogos);
		}
	}
}
