using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;
using ProjetoEstudo.Model.Dtos;
using ProjetoEstudo.Service.Interfaces;
using System;
using static EstudoProjeto.Utils.Enum;

namespace ProjetoEstudo.Service
{
	public class DevolverJogoService : IDevolverJogo
	{
		private readonly IAlugadoDao _alugadoDao;

		public DevolverJogoService(IAlugadoDao alugadoDao)
		{
			_alugadoDao = alugadoDao;
		}

		public DevolverJogoResponseDto DevolverJogo(DevolverJogoRequestDto devolverJogoRequestDto)
		{
			Alugado alugado = _alugadoDao.FindById(devolverJogoRequestDto.Id);

			if (alugado != null)
			{
				alugado.Status = StatusAlugado.Entregue;

				_alugadoDao.Update(alugado);

				DevolverJogoResponseDto responseDto = this.GetDevolverJogoResponseDto(devolverJogoRequestDto.DataDevolucao, alugado.DataEntrega);

				return responseDto;
			}
			return null;
		}//func

		private DevolverJogoResponseDto GetDevolverJogoResponseDto(DateTime dataEntregaReal, DateTime dataEntregaEsperada)
		{

			TimeSpan date = dataEntregaReal - dataEntregaEsperada;

			int days = date.Days <= 0 ? 0 : date.Days;

			DevolverJogoResponseDto devolverJogoResponseDto = new DevolverJogoResponseDto()
			{
				DiasDeAtraso = days,
			};

			return devolverJogoResponseDto;
		}//func
	}//class
}//namespace
