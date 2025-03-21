using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4VisitorCar;

namespace ricoh.ServiceRequests
{

  partial class Pass4VisitorCarServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      var subject = _obj.CarModel + " / " +_obj.CarNumber;
      _obj.Subject = subject.Substring(0, Math.Min(subject.Length, 120));
      var ValidOn = _obj.ValidOn.Value.ToShortDateString();
      string parkingPlace;
      if (_obj.ParkingType == Pass4VisitorCar.ParkingType.PrivateParking) {
        parkingPlace = "место " + _obj.ParkingPlace?.Name;
      } else  {
        parkingPlace = "общая парковка";
      }      
      _obj.Name = $"Гостевая парковка ({_obj.Renter.Name} на {ValidOn}: {_obj.Subject}, {parkingPlace})";
    }
  }

}