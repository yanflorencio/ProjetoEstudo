using EstudoProjeto.Utils;
using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;
using ProjetoEstudo.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoEstudo.Service
{
	public class JogoService : IJogoService
	{
		private readonly IJogoDao _jogoRepositorio;

		public JogoService(IJogoDao jogoRepositorio)
		{
			_jogoRepositorio = jogoRepositorio;
		}

		public IList<Jogo> GetJogos()
		{
			List<Jogo> listaJogos = _jogoRepositorio.GetAll().ToList();

			return listaJogos;
		}

		public void CadastrarJogo(Jogo jogo)
		{
			_jogoRepositorio.Save(jogo);
		}

		public void AlterarJogo(Jogo jogo)
		{
			_jogoRepositorio.Update(jogo);
		}

		public IList<Jogo> GetJogoByPlataforma(Enum.Plataforma plataforma)
		{

			return _jogoRepositorio.GetJogoByPlataforma(plataforma);
		}

		public Jogo GetById(long id)
		{
			return _jogoRepositorio.FindById(id);
		}

		public void DeletarJogo(long id)
		{
			Jogo jogo = _jogoRepositorio.FindById(id);

			if (jogo != null)
			{
				_jogoRepositorio.Delete(jogo);
			}
		}

		public IList<Jogo> GetJogosDisponiveis()
		{
			IList<Jogo> listaJogos = _jogoRepositorio.GetJogosDisponiveis();

			return listaJogos;
		}
	}//class
}
