using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarProjectContext context = new CarProjectContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join cl in context.Colors
                             on c.ColorId equals cl.Id
                             select new CarDetailDto
                             {
                                 CarId = c.Id,                                                
                                 BrandName = b.BrandName,
                                 ColorName = cl.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 CarName = c.CarName,
                                 ModelYear = c.ModelYear
                             };
                return result.ToList();

            }
        }

        public List<CarDetailDto> GetAllCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (CarProjectContext context = new CarProjectContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join cl in context.Colors on c.ColorId equals cl.Id                  
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 BrandId = b.Id,
                                 ColorId = cl.Id,
                                 BrandName = b.BrandName,
                                 ColorName = cl.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 CarName = c.CarName,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 ImagePath = (from a in context.CarImages where a.CarId == c.Id select a.ImagePath).FirstOrDefault()
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();


            }
        }

        public virtual List<Car> GetList(Expression<Func<Car, bool>> filter = null)
        {
            using (var context = new CarProjectContext())
            {
                return filter == null
                    ? context.Set<Car>().Include(x => x.Brand).Include(x => x.Color).Include(x => x.CarImages).ToList()
                    : context.Set<Car>().Include(x => x.Brand).Include(x => x.Color).Include(x => x.CarImages).Where(filter).ToList();
            }
        }

        public Car Get(Expression<Func<Car, bool>> filter = null)
        {
            using (var context = new CarProjectContext())
            {
                return context.Set<Car>().Include(x => x.Brand).Include(x => x.Color).Include(x => x.CarImages).FirstOrDefault(filter);
            }
        }
    }
}
