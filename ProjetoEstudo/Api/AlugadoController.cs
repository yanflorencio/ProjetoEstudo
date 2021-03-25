using Microsoft.AspNetCore.Mvc;
using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;
using System;

namespace ProjetoEstudo.Api
{
	[ApiController]
	[Route("api/[controller]")]
	public class AlugadoController : ControllerBase
	{
		private readonly IAlugadoDao _alugadoDao;

		public AlugadoController(IAlugadoDao alugadoDao)
		{
			_alugadoDao = alugadoDao;
		}

		[HttpPost]
		public IActionResult AlugarJogo([FromBody] Alugado alugado)
		{
			if (ModelState.IsValid)
			{
				if ((alugado.IdCliente != default) && (alugado.IdJogo != default)) 
				{
					DateTime dataEntrega = alugado.DataAluguel.AddDays(5);

					alugado.DataEntrega = dataEntrega;
					alugado.Status = EstudoProjeto.Utils.Enum.StatusAlugado.Alugado;

					_alugadoDao.Save(alugado);

					return Ok(dataEntrega);
				}
			}

			return BadRequest();
		}
	}
}
