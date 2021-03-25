using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;
using System.Collections.Generic;
using System.Linq;
using static EstudoProjeto.Utils.Enum;

namespace ProjetoEstudo.Dao
{
	public class JogoDao : RepositorioBase<Jogo>, IJogoDao
	{
		public JogoDao(BancoContext context) : base(context)
		{

		}

		public IList<Jogo> GetJogosDisponiveis()
		{
			IQueryable<Jogo> query = from jogo in _context.Jogo
									 join alugado in _context.Alugado
									 on jogo.Id equals alugado.IdJogo into joinJogoAlugado
									 from subquery in joinJogoAlugado.DefaultIfEmpty()
									 where subquery.Status != StatusAlugado.Alugado
									 select new Jogo 
									 { 
										 Id = jogo.Id,
										 Nome = jogo.Nome,
										 Plataforma = jogo.Plataforma
									 };

			return query.ToList();
		}
	}
}
