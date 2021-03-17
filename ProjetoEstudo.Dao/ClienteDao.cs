using ProjetoEstudo.Model;

namespace ProjetoEstudo.Dao
{
	public class ClienteDao : RepositorioBase<Cliente>
	{
		public ClienteDao(BancoContext context) : base(context)
		{

		}
	}
}
