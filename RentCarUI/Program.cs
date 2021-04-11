using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace CarApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            CarManager carManager = new CarManager(new EfCarDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            foreach (var rental in rentalManager.GetAll().Data)
            {
                Console.WriteLine(rental.CarId);
            }

            rentalManager.Add(new Rental
            {
                CarId = 2,
                CustomerId = 1,
                RentDate = DateTime.Now,
                ReturnDate = null
            });




        }

        private static void CarDetails(CarManager carManager)
        {


            var result = carManager.GetAllCarDetails();
            if (result.Success)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine("Marka: " + car.BrandName + "\n" + "Model: " + car.CarName + "\n" + "Renk: " + car.ColorName + "\n" + "Günlük Fiyat: " + car.DailyPrice);
                }
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

    }
}
