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
      if (Equals(e.OldValue, e.NewValue)) return;
      _obj.Cars.Clear();
      _obj.Visitors = "";
      if (_obj.PermanentParkingPass == null) return;
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
      if ((e.NewValue == BaseSRQ.RequestState.Approved) && (e.OldValue != BaseSRQ.RequestState.Approved)) {
        if (_obj.PermanentParkingPass == null) return;

        var permanentParking = _obj.PermanentParkingPass;
        permanentParking.Visitors = _obj.Visitors;
        permanentParking.Cars.Clear();
        foreach(var car in _obj.Cars) {
          var newCar = permanentParking.Cars.AddNew();
          newCar.Model = car.Model;
          newCar.Number = car.Number;
        }
        _obj.PermanentParkingPass.Save();
      }
    }

    public override void RenterChanged(ricoh.ServiceRequests.Shared.BaseSRQRenterChangedEventArgs e)
    {
      base.RenterChanged(e);
    }

    public virtual void PermanentParkingPassChanged(ricoh.ServiceRequests.Shared.ChangePermanentParkingPermanentParkingPassChangedEventArgs e)
    {
      if (Equals(e.OldValue, e.NewValue)) return;
      _obj.Cars.Clear();
      _obj.Visitors = "";
      if (_obj.PermanentParkingPass == null) return;
      foreach(var car in _obj.PermanentParkingPass.Cars) {
        var newCar = _obj.Cars.AddNew();
        newCar.Model = car.Model;
        newCar.Number = car.Number;
        newCar.NeedPrintedPass = ChangePermanentParkingCars.NeedPrintedPass.No;
      }
      _obj.Visitors = _obj.PermanentParkingPass.Visitors;
    }

  }
}