using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;

namespace ProjetoEstudo.Dao
{
	public class ClienteDao : RepositorioBase<Cliente>, IClienteDao
	{
		public ClienteDao(BancoContext context) : base(context)
		{

		}
	}
}
