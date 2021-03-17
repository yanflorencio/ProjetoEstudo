using ProjetoEstudo.Model;

namespace ProjetoEstudo.Dao
{
	public class JogoDao : RepositorioBase<Jogo>
	{
		public JogoDao(BancoContext context) : base(context)
		{

		}
	}
}
