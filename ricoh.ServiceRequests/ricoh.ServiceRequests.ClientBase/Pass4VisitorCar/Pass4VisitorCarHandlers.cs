using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4VisitorCar;

namespace ricoh.ServiceRequests
{
  partial class Pass4VisitorCarClientHandlers
  {

    public override void RenterValueInput(ricoh.ServiceRequests.Client.BaseSRQRenterValueInputEventArgs e)
    {
      base.RenterValueInput(e);
      _obj.ParkingPlace = ParkingPlaces.Null;
    }

    public virtual void ParkingTypeValueInput(Sungero.Presentation.EnumerationValueInputEventArgs e)
    {
      var privateParking = _obj.ParkingType == Pass4VisitorCar.ParkingType.PrivateParking;
      _obj.State.Properties.Duration.IsVisible = privateParking;
      _obj.State.Properties.ParkingPlace.IsVisible = !privateParking;
    }

    public virtual void ValidOnDateTimeValueInput(Sungero.Presentation.DateTimeValueInputEventArgs e)
    {
      _obj.ValidOn = _obj.ValidOnDateTime;
    }

  }
}