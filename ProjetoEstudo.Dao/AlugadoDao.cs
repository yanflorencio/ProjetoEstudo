using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;

namespace ProjetoEstudo.Dao
{
	public class AlugadoDao : RepositorioBase<Alugado>, IAlugadoDao
	{

		public AlugadoDao(BancoContext context) : base(context)
		{
		}
	}
}
