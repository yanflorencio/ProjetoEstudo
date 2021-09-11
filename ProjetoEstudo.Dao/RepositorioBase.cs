using ProjetoEstudo.Dao.Interface;
using System;
using System.Linq;
using System.Transactions;

namespace ProjetoEstudo.Dao
{
	public class RepositorioBase<TEntity> : IDao<TEntity> where TEntity : class
	{
		protected readonly BancoContext _context;

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

		public R ExecuteTransaction<R>(Func<R> func)
		{
			using (TransactionScope transaction = new TransactionScope())
			{
				R result = func();

				transaction.Complete();

				return result;
			}//using
		}//func
	}
}
