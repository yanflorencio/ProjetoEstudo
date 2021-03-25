using ProjetoEstudo.Dao.Interface;
using ProjetoEstudo.Model;
using System.Collections.Generic;

namespace ProjetoEstudo.Dao.Interfaces
{
	public interface IJogoDao : IDao<Jogo>
	{
		public IList<Jogo> GetJogosDisponiveis();
	}
}
