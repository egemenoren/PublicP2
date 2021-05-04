using AyazNew.Entity;
using AyazNew.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyazNew.Service
{
    public class BaseService<T> where T : BaseEntity
    {
        internal GenericRepository<T> repository;

        public BaseService()
        {
            repository = new GenericRepository<T>();
        }
        public virtual List<T> GetAll()
        {
            var result = repository.SelectAll();
            return result.ToList();

        }

        public virtual ServiceResult<T> Get(int id)
        {
            var result = repository.FindById(id);
            if (result == null || result.Id == 0)
                return new ServiceResult<T>(ServiceResultCode.RecordNotFound);

            return new ServiceResult<T>(result);
        }

        public virtual ServiceResult New(T entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.Status = DataStatus.Active;
            repository.Insert(entity);
            return SaveChanges();
        }

        public virtual ServiceResult Edit(T entity)
        {
            var actualTransfer = repository.Select(x => x.Id == entity.Id);
            if (actualTransfer == null)
            {
                return new ServiceResult(ServiceResultCode.RecordNotFound);
            }

            repository.Update(entity);

            return SaveChanges();
        }

        public virtual ServiceResult Delete(int id)
        {
            var result = repository.FindById(id);
            if (result == null || result.Id == 0)
                return new ServiceResult<T>(ServiceResultCode.RecordNotFound);

            repository.Delete(id);
            return new ServiceResult();
        }

        internal ServiceResult SaveChanges()
        {
            try
            {
                repository.SaveChanges();
                return new ServiceResult();
            }
            catch (Exception ex)
            {
                return new ServiceResult(ServiceResultCode.Generic, ex.Message);
            }
        }


    }
}
