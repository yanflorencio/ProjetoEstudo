using System.Linq;

namespace ProjetoEstudo.Dao.Interface
{
	public interface IDao<TEntity> where TEntity : class
	{
		public IQueryable<TEntity> GetAll();
		public TEntity FindById(long id);
		public void Save(params TEntity[] entity);
		public void Delete(params TEntity[] entity);
		public void Update(params TEntity[] entity);
	}
}
