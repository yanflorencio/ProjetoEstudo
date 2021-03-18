using System.Linq;

namespace ProjetoEstudo.Dao
{
	public class RepositorioBase<TEntity> : IDao<TEntity> where TEntity : class
	{
		private readonly BancoContext _context;

		public RepositorioBase(BancoContext context)
		{
			_context = context;
		}

		public void Delete(params TEntity[] entity)
		{
			_context.Set<TEntity>().RemoveRange(entity);
			_context.SaveChanges();
		}

		public TEntity FindById(long id) => _context.Set<TEntity>().Find(id);


		public IQueryable<TEntity> GetAll() => _context.Set<TEntity>().AsQueryable();

		public void Save(params TEntity[] entity)
		{
			_context.Set<TEntity>().AddRange(entity);
			_context.SaveChanges();
		}

		public void Update(params TEntity[] entity)
		{
			_context.Set<TEntity>().UpdateRange(entity);
			_context.SaveChanges();
		}
	}
}
