using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.StopPermanentPass4Car;

namespace ricoh.ServiceRequests
{
  partial class StopPermanentPass4CarClientHandlers
  {

    public virtual void PermanentPass4CarValueInput(ricoh.ServiceRequests.Client.StopPermanentPass4CarPermanentPass4CarValueInputEventArgs e)
    {
      if (e.NewValue != PermanentPass4Cars.Null) {
        _obj.CarModel = e.NewValue.CarModel;
        _obj.CarNumber = e.NewValue.CarNumber;
        _obj.ParkingPlace = e.NewValue.ParkingPlace;
      }      
    }

    public override void RenterValueInput(ricoh.ServiceRequests.Client.BaseSRQRenterValueInputEventArgs e)
    {
      base.RenterValueInput(e);
      _obj.PermanentPass4Car = PermanentPass4Cars.Null;
    }

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      base.Refresh(e);
      _obj.State.Properties.ValidOn.IsVisible = _obj.Action.Equals(StopPermanentPass4Car.Action.StopPass);
    }
  }

}