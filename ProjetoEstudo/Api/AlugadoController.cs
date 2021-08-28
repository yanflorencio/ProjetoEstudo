using Microsoft.AspNetCore.Mvc;
using ProjetoEstudo.Model;
using ProjetoEstudo.Model.Dtos;
using ProjetoEstudo.Service.Interfaces;
using System;

namespace ProjetoEstudo.Api
{
	[ApiController]
	[Route("api/[controller]")]
	public class AlugadoController : ControllerBase
	{
		private readonly IDevolverJogo _devolverJogo;

		private readonly IAlugarJogo _alugadoService;

		public AlugadoController(IDevolverJogo devolverJogo, IAlugarJogo alugadoService)
		{
			_devolverJogo = devolverJogo;
			_alugadoService = alugadoService;
		}

		[HttpPost]
		public IActionResult AlugarJogo([FromBody] Alugado alugado)
		{
			if (ModelState.IsValid)
			{
				DateTime? dataEntrega = _alugadoService.AlugarJogo(alugado);

				if (dataEntrega.HasValue)
				{
					return Ok(dataEntrega.Value);
				}
			}

			return BadRequest();
		}

		[HttpPut]
		public IActionResult DevolverJogo([FromBody] DevolverJogoRequestDto devolverJogoRequestDto)
		{
			if (ModelState.IsValid)
			{

				DevolverJogoResponseDto responseDto = _devolverJogo.DevolverJogo(devolverJogoRequestDto);

				if (responseDto != null)
				{
					return Ok(responseDto);
				}			
			}

			return BadRequest();
		}
	}
}
