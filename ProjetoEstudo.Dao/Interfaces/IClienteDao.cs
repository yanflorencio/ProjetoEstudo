using ProjetoEstudo.Dao.Interface;
using ProjetoEstudo.Model;

namespace ProjetoEstudo.Dao.Interfaces
{
	public interface IClienteDao : IDao<Cliente>
	{
		public Cliente GetJogosAlugadosByCpf(string cpf);
	}
}
