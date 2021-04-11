using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICustomerService _customerService;
        ICarService _carService;
        IFindexService _findexService;

        public RentalManager(IRentalDal rendalDal, ICustomerService customerService, ICarService carService, IFindexService findexService)
        {
            _rentalDal = rendalDal;
            _customerService = customerService;
            _carService = carService;
            _findexService = findexService;
        }

        public IResult Add(Rental rental)
        {
            var results = BusinessRules.Run(CarRentedCheck(rental), FindexScoreCheck(rental.CustomerId, rental.CarId));

            if (results != null)
            {
                return results;
            }

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.CarRentSuccess);


        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<Rental> Get(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id));
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<List<RentDetailDto>> GetRentDetails()
        {
            return new SuccessDataResult<List<RentDetailDto>>(_rentalDal.GetRentDetails());
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        private IResult FindexScoreCheck(int userId, int carId)
        {
            var customerFindexPoint = _findexService.GetFindexByUserId(userId).Data.Point;


            if (customerFindexPoint == 0)
            {
                return new ErrorResult(Messages.CustomerFindexPointZero);
            }

            var carFindexPoint = _carService.GetById(carId).Data.FindexPoint;

            if (customerFindexPoint < carFindexPoint)
            {
                return new ErrorResult(Messages.CustomerScoreIsInsufficient);
            }

            return new SuccessResult();
        }
        private IResult CarRentedCheck(Rental rental)
        {
            var rentelladCars = _rentalDal.GetAll(r => r.CarId == rental.CarId && (r.ReturnDate == null || r.ReturnDate > DateTime.Now)).Any();
            if (rentelladCars)
            {
                return new ErrorResult(Messages.CarIsRentalled);
            }
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetRentalByCarId(int carId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId));
        }
    }
}
