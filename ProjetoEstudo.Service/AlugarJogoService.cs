using Projeto.Estudo.SenderServiceBus.Interfaces;
using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;
using ProjetoEstudo.Service.Interfaces;
using ProjetoEstudo.Utils;
using System;
using System.Linq;
using static EstudoProjeto.Utils.Enum;

namespace ProjetoEstudo.Service
{
	public class AlugarJogoService : IAlugarJogo
	{
		private readonly IAlugadoDao _alugadoDao;
		private readonly ISender _sender;
		private string TopicName { get; set; }

		public AlugarJogoService(IAlugadoDao alugadoDao, ISender sender)
		{
			_alugadoDao = alugadoDao;
			_sender = sender;

			this.TopicName = UtilitiesConfig.GetAppSetting("AlugarJogoTopic");
		}

		public DateTime? AlugarJogo(Alugado alugado)
		{
			if ((alugado.ClienteId != default) && (!this.CheckJogoEstaDisponivel(alugado)))
			{
				DateTime dataEntrega = alugado.DataAluguel.AddDays(5);

				alugado.DataEntrega = dataEntrega;
				alugado.Status = StatusAlugado.Alugado;

				Action action = () =>
				{
					_alugadoDao.Save(alugado);

					_sender.SendMessage(alugado, this.TopicName);
					
				};

				ExecuteTransactionWithoutResult(action);

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

		public void ExecuteTransactionWithoutResult(Action action)
		{
			Func<object> func = () =>
			{
				action();
				return null;
			};//func
			_alugadoDao.ExecuteTransaction(func);
		}//func
	}//class
}//namespace
