using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Car;

namespace ricoh.ServiceRequests
{

  partial class PermanentPass4CarServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      _obj.ValidOn = Calendar.Today.AddDays(1);
      _obj.ValidFrom = _obj.ValidOn;
      _obj.ValidTill = Calendar.SqlMaxValue;
    }

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);      
      string action = "Постоянная парковка ";
      var subject = action + _obj.CarModel + "/" +_obj.CarNumber;;
      _obj.Subject = subject.Substring(0, Math.Min(subject.Length, 250));           
    }
  }

}