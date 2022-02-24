using Microsoft.EntityFrameworkCore;
using ProjetoEstudo.Dao.Interface;
using System;
using System.Linq;
using System.Transactions;

namespace ProjetoEstudo.Dao
{
	public class RepositorioBase<TEntity> : IDao<TEntity> where TEntity : class
	{
		protected readonly BancoContext _context;

		protected readonly DbSet<TEntity> _dbSet;

		public RepositorioBase(BancoContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		public void Delete(params TEntity[] entity)
		{
			_dbSet.RemoveRange(entity);
			_context.SaveChanges();
		}

		public TEntity FindById(long id) => _dbSet.Find(id);


		public IQueryable<TEntity> GetAll() => _dbSet.AsQueryable();

		public void Save(params TEntity[] entity)
		{
			_dbSet.AddRange(entity);
			_context.SaveChanges();
		}

		public void Update(params TEntity[] entity)
		{
			_dbSet.UpdateRange(entity);
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
