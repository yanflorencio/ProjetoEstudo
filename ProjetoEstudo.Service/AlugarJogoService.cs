using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;
using ProjetoEstudo.Service.Interfaces;
using System;
using System.Linq;
using static EstudoProjeto.Utils.Enum;

namespace ProjetoEstudo.Service
{
	public class AlugarJogoService : IAlugarJogo
	{
		private readonly IAlugadoDao _alugadoDao;

		public AlugarJogoService(IAlugadoDao alugadoDao)
		{
			_alugadoDao = alugadoDao;
		}

		public DateTime? AlugarJogo(Alugado alugado)
		{
			if ((alugado.ClienteId != default) && (!this.CheckJogoEstaDisponivel(alugado)))
			{
				DateTime dataEntrega = alugado.DataAluguel.AddDays(5);

				alugado.DataEntrega = dataEntrega;
				alugado.Status = StatusAlugado.Alugado;

				_alugadoDao.Save(alugado);

				return dataEntrega;
			}

			return null;
		}

		public bool CheckJogoEstaDisponivel(Alugado alugado)
		{
			IQueryable<Alugado> query = _alugadoDao.GetAll().Where(bean =>
																	(bean.JogoId == alugado.JogoId) &&
																	(bean.Status != StatusAlugado.Alugado)
																	);

			return query.Any();
		}
	}//class
}//namespace
