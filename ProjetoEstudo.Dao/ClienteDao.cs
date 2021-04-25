using Microsoft.EntityFrameworkCore;
using ProjetoEstudo.Dao.Interfaces;
using ProjetoEstudo.Model;
using System.Linq;
using static EstudoProjeto.Utils.Enum;

namespace ProjetoEstudo.Dao
{
	public class ClienteDao : RepositorioBase<Cliente>, IClienteDao
	{
		public ClienteDao(BancoContext context) : base(context)
		{

		}

		public Cliente GetClienteIncludeJogosAlugadosByCpf(string cpf)
		{
			IQueryable<Cliente> query = _context.Cliente
										.Include(c => c.JogosAlugados
														.Where(bean => bean.Status == StatusAlugado.Alugado)
												)
										.ThenInclude(j => j.Jogo);

			query = query.Where(c => c.Cpf.Equals(cpf));

			Cliente cliente = query.FirstOrDefault();

			return cliente;
		}
	}
}
