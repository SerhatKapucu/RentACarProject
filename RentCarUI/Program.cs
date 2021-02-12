﻿using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace CarApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            //brandManager.Add(new Brand() { BrandName = "Citroen C4" });

            BrandList();

            ColorManager colorManager = new ColorManager(new EfColorDal());
            //colorManager.Add(new Color() { ColorName = "Beyaz" });

            ColorList();

            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetCarDetails();
            if (result.Success)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine("Marka: " + car.BrandName + "\n" + "Model: " + car.ModelYear + "\n" + "Renk: " + car.ColorName + "\n" + "Günlük Fiyat: " + car.DailyPrice);
                }
                Console.WriteLine(result.Message);

            }
            else
            {
                Console.WriteLine(result.Message);
            }



        }
        private static void BrandList()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            var result = brandManager.GetAll();
            if (result.Success)
            {
                foreach (var brand in result.Data)
                {
                    Console.WriteLine(brand.BrandName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

        }
        private static void ColorList()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            var result = colorManager.GetAll();
            if (result.Success)
            {
                foreach (var brand in result.Data)
                {
                    Console.WriteLine(brand.ColorName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}
