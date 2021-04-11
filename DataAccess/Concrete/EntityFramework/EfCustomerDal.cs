using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
   public class EfCustomerDal : EfEntityRepositoryBase<Customer,CarProjectContext>, ICustomerDal
    {
        public CustomerDto GetByEmail(Expression<Func<CustomerDto, bool>> filter)
        {
            using (CarProjectContext context = new CarProjectContext())
            {
                var result = from c in context.Customers
                             join u in context.Users on c.UserId equals u.Id

                             select new CustomerDto
                             {
                                 Id = c.Id,
                                 UserId = c.UserId,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 CompanyName = c.CompanyName,
                                 FindexPoint = (from f in context.Findexs where f.UserId == c.UserId select f.Point).FirstOrDefault()
                             };

                return result.SingleOrDefault(filter);
            }
        }

        public List<CustomerDto> GetCustomerDetails()
        {
            using (var context = new CarProjectContext())
            {
                var result = from c in context.Customers
                             join u in context.Users on c.UserId equals u.Id
                             select new CustomerDto
                             {
                                 Id = c.Id,
                                 UserId = c.UserId,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 CompanyName = c.CompanyName,
                                 FindexPoint = (from f in context.Findexs where f.UserId == c.UserId select f.Point).FirstOrDefault()
                             };

                return result.ToList();
            }
        }
    }
}

