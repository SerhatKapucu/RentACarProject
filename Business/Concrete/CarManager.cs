using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        // [SecuredOperation("car.add,admin")]
        //[ValidationAspect(typeof(CarValidator))]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);

            return new SuccessResult(Messages.CarAdded);

        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        //[SecuredOperation("admin")]
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetList(), Messages.CarsListed);
        }


        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }
        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetAllCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetails());
        }


        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetList(c => c.BrandId == id), Messages.CarsFilterListed);
        }


        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetList(c => c.ColorId == id), Messages.CarsFilterListed);
        }
        [TransactionScopeAspect]
        public IResult TransactionalOperation(Car car)
        {
            _carDal.Update(car);
            _carDal.Add(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }


        public IDataResult<List<CarDetailDto>> GetCarDetailsByColor(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetails(c => c.ColorId == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarsByBrand(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetails(b => b.BrandId == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarsByColor(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetails(c => c.ColorId == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarsByBrandAndColor(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetails(b => b.BrandId == brandId && b.ColorId == colorId));
        }
    }
}
