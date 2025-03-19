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