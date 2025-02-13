using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Car;

namespace ricoh.ServiceRequests
{
  partial class CarServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      _obj.Name = _obj.CarModel + " (" + _obj.CarNumber + "), " + _obj.Renter.Name;
    }
  }

}