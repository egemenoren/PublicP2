using AyazNew.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyazNew.Service
{
    public class ProductService : BaseService<Products>
    {
        public override ServiceResult New(Products entity)
        {
            var bike = repository.Select(x => x.ProductName == entity.ProductName);
            if (bike.Any())
            {
                return new ServiceResult(ServiceResultCode.Generic, "This Product is already exists.");
            }
            else
            {
                new ServiceResult(ServiceResultCode.Success, "Success");
                return base.New(entity);
            }

        }
        public ServiceResult<Products> Get(string productName)
        {
            // var bike = repository.Select(x => x.brand == brand && x => x.BikeModel== bikeModel);
            var prod = repository.Select(x => x.ProductName == productName);
            if (!prod.Any())
            {
                return new ServiceResult<Products>(ServiceResultCode.Generic, "Not exists.");
            }
            else
            {
                return new ServiceResult<Products>(prod.First());
            }
        }
        public ServiceResult<Products> GetById(int id)
        {
            var prod = repository.Select(x => x.Id == id);
            if (prod.Any())
                return new ServiceResult<Products>(prod.First());
            else
                return new ServiceResult<Products>(ServiceResultCode.Generic, "Not Exists.");
        }
        public List<Products> GetBikeType(int filter)
        {
            var result = repository.Select(x => x.CategoryID == filter);
            return result.ToList();

        }
        public ServiceResult<Products> Activate(int bikeid, string AdminName)
        {
            var getbike = repository.FindById(bikeid);
            getbike.Status = DataStatus.Active;
            repository.Update(getbike);
            SaveChanges();
            return new ServiceResult<Products>(ServiceResultCode.Success, "Succeed");
        }


    }
}
