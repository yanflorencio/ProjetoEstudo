using Microsoft.EntityFrameworkCore;
using ProjetoEstudo.Model;

namespace ProjetoEstudo.Dao
{
	public class BancoContext : DbContext
	{
		public DbSet<Alugado> Alugado { get; set; }
		public DbSet<Cliente> Cliente { get; set; }
		public DbSet<Jogo> Jogo { get; set; }

		public BancoContext(DbContextOptions<BancoContext> options) : base(options)
		{
			this.Database.Migrate();
		}
	}
}
