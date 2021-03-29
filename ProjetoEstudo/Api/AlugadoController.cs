using Microsoft.AspNetCore.Mvc;
using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;
using ProjetoEstudo.Model.Dtos;
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
				if ((alugado.ClienteId != default) && (!this.CheckJogoEstaDisponivel(alugado))) 
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
																	(bean.JogoId == alugado.JogoId) &&
																	(bean.Status != StatusAlugado.Alugado)
																	);

			return query.Any();
		}

		[HttpPut]
		public IActionResult DevolverJogo([FromBody] DevolverJogoRequestDto devolverJogoRequestDto)
		{
			if (ModelState.IsValid)
			{
				Alugado alugado = _alugadoDao.FindById(devolverJogoRequestDto.Id);

				if (alugado != null)
				{
					alugado.Status = StatusAlugado.Entregue;

					_alugadoDao.Update(alugado);

					DevolverJogoResponseDto responseDto = this.GetDevolverJogoResponseDto(devolverJogoRequestDto.DataDevolucao, alugado.DataEntrega);

					return Ok(responseDto);
				}
			}

			return BadRequest();
		}

		private DevolverJogoResponseDto GetDevolverJogoResponseDto(DateTime dataEntregaReal, DateTime dataEntregaEsperada)
		{

			TimeSpan date = dataEntregaReal - dataEntregaEsperada;

			int days = date.Days <= 0 ? 0 : date.Days;

			DevolverJogoResponseDto devolverJogoResponseDto = new DevolverJogoResponseDto()
			{
				DiasDeAtraso = days,
			};

			return devolverJogoResponseDto;
		}
	}
}
