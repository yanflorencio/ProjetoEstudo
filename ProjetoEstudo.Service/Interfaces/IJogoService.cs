using EstudoProjeto.Utils;
using ProjetoEstudo.Model;
using System.Collections.Generic;

namespace ProjetoEstudo.Service.Interfaces
{
	public interface IJogoService
	{
		public IList<Jogo> GetJogos();

		public void CadastrarJogo(Jogo jogo);

		public void AlterarJogo(Jogo jogo);

		public IList<Jogo> GetJogoByPlataforma(Enum.Plataforma plataforma);

		public Jogo GetById(long id);

		public void DeletarJogo(long id);

		public IList<Jogo> GetJogosDisponiveis();
	}
}
