using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
        public IResult Add(CarImage carImage, IFormFile file)
        {
            IResult result = BusinessRules.Run(CheckIfImageLimit(carImage.CarId));

            if (result != null)
            {
                return result;
            }
            if (file != null)
            {
                carImage.ImagePath = FileHelper.Add(file);
            }

            carImage.Date = DateTime.Now;

            _carImageDal.Add(carImage);

            return new SuccessResult(Messages.ImageAdded);
        }

        public IResult Delete(CarImage carImage)
        {
            IResult result = BusinessRules.Run(CarImageDelete(carImage));
            if (result != null)
            {
                return result;
            }

            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetImagesByCarId(int id)
        {
            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(id));
        }

        public IResult Update(CarImage carImage, IFormFile file)
        {
            carImage.ImagePath = FileHelper.Update(_carImageDal.Get(p => p.Id == carImage.Id).ImagePath, file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        private List<CarImage> CheckIfCarImageNull(int id)
        {
            string path = "logo.jpg";


            var result = _carImageDal.GetAll(c => c.CarId == id).Count();

            if (result < 1)
            {
                return new List<CarImage> { new CarImage { CarId = id, ImagePath = path, Date = DateTime.Now } };
            }
            return _carImageDal.GetAll(p => p.CarId == id);
        }

        private IResult CheckIfImageLimit(int carId)
        {
            var result = _carImageDal.GetAll(i => i.CarId == carId).Count;
            if (result > 50)
            {
                return new ErrorResult(Messages.ImageLimitError);
            }

            return new SuccessResult();

        }
        private IResult CarImageDelete(CarImage carImage)
        {
            try
            {
                File.Delete(carImage.ImagePath);
            }
            catch (Exception exception)
            {

                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }

        public IResult Update(CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        public IDataResult<CarImage> GetImageByCarId(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.GetAll(i => i.CarId == id).FirstOrDefault());
        }

        public string GetImagePathByCarId(int id)
        {
            var result = _carImageDal.GetAll(i => i.CarId == id).FirstOrDefault();
            if (result != null)
            {
                return result.ImagePath;
            }
            else
            {
                return "logo.jpg";
            }
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

    }
}

