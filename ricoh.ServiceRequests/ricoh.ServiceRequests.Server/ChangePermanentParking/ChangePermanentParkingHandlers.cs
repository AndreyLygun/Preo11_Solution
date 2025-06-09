using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.ChangePermanentParking;

namespace ricoh.ServiceRequests
{
  partial class ChangePermanentParkingServerHandlers
  {

    public override void AfterSave(Sungero.Domain.AfterSaveEventArgs e)
    {
      base.AfterSave(e);
      if (_obj.RequestState == BaseSRQ.RequestState.Approved)
        Sungero.Core.AccessRights.AllowRead(() => {
                                              var parkingPlace = _obj.ParkingPlace;
                                              parkingPlace.Cars.Clear();
                                              foreach (var car in _obj.Cars) {
                                                if (string.IsNullOrWhiteSpace(car.Number)) continue;
                                                var newCar = parkingPlace.Cars.AddNew();
                                                newCar.Model = car.Model;
                                                newCar.Number = car.Number;
                                                newCar.Note = car.Note;
                                              }
                                              parkingPlace.Drivers.Clear();
                                              if (!string.IsNullOrWhiteSpace(_obj.Visitors)) {
                                                foreach (var driver in _obj.Visitors.Split('\n')) {
                                                  if (string.IsNullOrWhiteSpace(driver)) continue;
                                                  var newDriver = parkingPlace.Drivers.AddNew();
                                                  newDriver.Name = driver;
                                                }
                                              }
                                              Logger.DebugFormat("Обновили информацию в парковочном месте '{0}' по заявке № {1}", parkingPlace.Name, _obj.Id);
                                            });
    }
    
    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      _obj.Subject  = string.Format("Изменение на парковочном месте {0}", _obj.ParkingPlace.Name);
      _obj.Name = string.Format("Заявка № {0} от {1}: {2}", _obj.Id, _obj.Renter, _obj.Subject);
      _obj.CarsString = "";
      foreach(var car in _obj.Cars) {
        var needLaminat = car.NeedPrintedPass==ChangePermanentParking.NeedNFC.Yes?" Требуется ламинат":"";
        var comment = string.IsNullOrEmpty(car.Note)?"":(string.Format(" ({0})",  car.Note));
        _obj.CarsString += string.Format("{1} / {2}{3}{0}\n", needLaminat, car.Model, car.Number, comment);
      }
    }
  }

}