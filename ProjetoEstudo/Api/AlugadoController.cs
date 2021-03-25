using Microsoft.AspNetCore.Mvc;
using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;
using System;
using System.Linq;
using static EstudoProjeto.Utils.Enum;

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
				if ((alugado.IdCliente != default) && (this.CheckJogoEstaDisponivel(alugado))) 
				{
					DateTime dataEntrega = alugado.DataAluguel.AddDays(5);

					alugado.DataEntrega = dataEntrega;
					alugado.Status = StatusAlugado.Alugado;

					_alugadoDao.Save(alugado);

					return Ok(dataEntrega);
				}
			}

			return BadRequest();
		}

		private bool CheckJogoEstaDisponivel(Alugado alugado)
		{
			IQueryable<Alugado> query = _alugadoDao.GetAll().Where(bean =>
																	(bean.IdJogo == alugado.IdJogo) &&
																	(bean.Status != StatusAlugado.Alugado)
																	);

			return query.Any();
		}
	}
}
