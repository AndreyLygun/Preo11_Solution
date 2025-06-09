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

    public override void AfterSave(Sungero.Domain.AfterSaveEventArgs e)
    {
      base.AfterSave(e);
      Functions.SecuritySRQ.UpdateVisitors(_obj);
    }

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      var car =  ServiceRequests.PublicFunctions.Module.TrimText(_obj.CarModel + " / " +_obj.CarNumber, 100);
      var ValidOn = _obj.ValidOn.Value.ToShortDateString();
      var parkingPlace = (_obj.ParkingType == Pass4VisitorCar.ParkingType.PrivateParking)?"место " + _obj.ParkingPlace?.Name:"общая парковка";
      _obj.Subject = string.Format("Гостевая парковка ({0} на {1})", car, ValidOn);
      _obj.Name = string.Format("Заявка № {0} от {1}: {2}", _obj.Id, _obj.Renter, _obj.Subject);
    }
  }

}