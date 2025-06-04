using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.SecuritySRQ;

namespace ricoh.ServiceRequests.Server
{
  partial class SecuritySRQFunctions
  {
    

    

    /// <summary>
    /// Записывает в заявку информацию о закрытии, модель и номер автомобиля, меняет статус на Closed.
    /// Срабатывает независимо от наличия у пользователя прав на заявку
    /// </summary>
    /// <param name="comment">Комментарий к закрыти</param>/// 
    /// <param name="CarModel"></param>
    /// <param name="CarNumber"></param>
    
    [Remote, Public]
    public void CloseRequestWithCarInfo(string comment, string carModel, string carNumber)
    {
      comment = string.Format("Закрыто {0} {1}, {2} {3}", 
                                  Calendar.Now.ToShortDateString(),
                                  Calendar.Now.ToShortTimeString(), 
                                  Users.Current.DisplayValue,
                                  string.IsNullOrWhiteSpace(comment)?"":$", ({comment})");
      Sungero.Core.AccessRights.AllowRead(() => {
                                            _obj.ClosingInfo = comment;
                                            _obj.RequestState = ServiceRequests.BaseSRQ.RequestState.Closed;
                                            _obj.CarNumber = carNumber;
                                            _obj.CarModel = carModel;
                                            _obj.Save();
                                          });
    }


    /// <summary>
    /// 
    /// </summary>
    /// 
    [Public]
    public void AddOrUpdateCar()
    {        
      if (!Functions.BaseSRQ.isAllowed(_obj)) return;
      var car = Cars.GetAll(c => c.Pass.Equals(_obj)).FirstOrDefault();
      if (car == null) {
        car = Cars.Create();
      }
      car.CarModel = _obj.CarModel;
      car.CarNumber = _obj.CarNumber;
      car.ParkingPlace = _obj.ParkingPlace;
      car.ValidFrom = _obj.ValidTill;
      car.Pass = _obj;
      car.Renter = _obj.Renter;
      car.Save();
    }

    public void RemoveCar()
    {        
      if (!Functions.BaseSRQ.isAllowed(_obj)) return;
      var car = Cars.GetAll(c => c.Pass.Equals(_obj)).FirstOrDefault();
      if (car != null)  {
        Cars.Delete(car);
      } 
    }    
    
  }
}