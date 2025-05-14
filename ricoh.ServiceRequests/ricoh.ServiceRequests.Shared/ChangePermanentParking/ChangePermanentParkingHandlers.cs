using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.ChangePermanentParking;

namespace ricoh.ServiceRequests
{
  partial class ChangePermanentParkingCarsSharedCollectionHandlers
  {

    public virtual void CarsAdded(Sungero.Domain.Shared.CollectionPropertyAddedEventArgs e)
    {
      _added.NeedPrintedPass = ChangePermanentParkingCars.NeedPrintedPass.Yes;
    }
  }



  partial class ChangePermanentParkingSharedHandlers
  {

    public override void ParkingPlaceChanged(ricoh.ServiceRequests.Shared.SecuritySRQParkingPlaceChangedEventArgs e)
    {
      base.ParkingPlaceChanged(e);
      // При выборе парковочного места заполняем сведения об автомобилях и водителях информацией с нового парковочного места
      if (Equals(e.OldValue, e.NewValue)) return;
      _obj.Cars.Clear();
      _obj.Visitors = "";
      if (_obj.ParkingPlace == null) return;
      foreach(var car in _obj.ParkingPlace.Cars) {
        var newCar = _obj.Cars.AddNew();
        newCar.Model = car.Model;
        newCar.Number = car.Number;
        newCar.NeedPrintedPass = ChangePermanentParkingCars.NeedPrintedPass.No;
      }
      foreach(var driver in _obj.ParkingPlace.Drivers) {
        _obj.Visitors += driver.Name + "\r";
      }
      
    }

    public override void RequestStateChanged(Sungero.Domain.Shared.EnumerationPropertyChangedEventArgs e)
    {
      base.RequestStateChanged(e);      
      if ((e.NewValue == BaseSRQ.RequestState.Done) && (e.OldValue != BaseSRQ.RequestState.Done)) {
       // Когда заявка переводится в состояние "Исполнено", вносим изменения в парковочное место
        if (_obj.ParkingPlace == null) return;    
        var parking = _obj.ParkingPlace;
        parking.Cars.Clear();
        foreach(var car in _obj.Cars) {
          var newCar = parking.Cars.AddNew();
          newCar.Model = car.Model;
          newCar.Number = car.Number;
          newCar.Changed = _obj.Name;
        }
        parking.Drivers.Clear();
        foreach(var driver in _obj.Visitors.Split('\r')) {
          var newDriver = parking.Drivers.AddNew();
          newDriver.Name = driver;
          newDriver.Changed = _obj.Name;
          newDriver.Save();
        }          
      }
    }

  }
}