using AyazNew.Data;
using AyazNew.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AyazNew.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private AyazEntities _context;
        private DbSet<T> _dbSet;
        public GenericRepository()
        {
            _context = new AyazEntities();
            _dbSet = _context.Set<T>();
        }
        public virtual T FindById(object EntityId)
        {
            return _dbSet.Find(EntityId);
        }
        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter != null)
            {
                return _dbSet.Where(Filter);
            }
            return _dbSet;
        }
        public virtual List<T> SelectAll()
        {
            return _dbSet.ToList();
        }
        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }
        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void Delete(object EntityId)
        {

            T entityToDelete = _dbSet.Find(EntityId);
            entityToDelete.Status = DataStatus.Deleted;
            SaveChanges();
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
