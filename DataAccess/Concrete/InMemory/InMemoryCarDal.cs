﻿using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car> { new Car() { Id = 1, BrandId = 1, ColorId = 1, DailyPrice = 120, ModelYear = 2019, Description = "Ekonomik Araba" },
            new Car() { Id = 2, BrandId = 2, ColorId = 2, DailyPrice = 150, ModelYear = 2019, Description = "Ekonomik Araba" },
            new Car() { Id = 3, BrandId = 3, ColorId = 2, DailyPrice = 160, ModelYear = 2019, Description = "Ekonomik Araba" },
            new Car() { Id = 4, BrandId = 4, ColorId = 3, DailyPrice = 180, ModelYear = 2020, Description = "Orta Segment Araba" },
            new Car() { Id = 5, BrandId = 5, ColorId = 4, DailyPrice = 200, ModelYear = 2020, Description = "Orta Segment Araba" }};
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carsToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carsToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetById(int Id)
        {
            return _cars.Where(c => c.Id == Id).ToList();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carsToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carsToUpdate.BrandId = car.BrandId;
            carsToUpdate.ColorId = car.ColorId;
            carsToUpdate.DailyPrice = car.DailyPrice;
            carsToUpdate.ModelYear = car.ModelYear;
            carsToUpdate.Description = car.Description;
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }
    }
}
