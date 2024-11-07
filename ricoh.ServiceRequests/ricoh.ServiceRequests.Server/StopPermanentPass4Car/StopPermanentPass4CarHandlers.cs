using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.StopPermanentPass4Car;

namespace ricoh.ServiceRequests
{
  partial class StopPermanentPass4CarServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      _obj.Subject = _obj.CarNumber;
    }
  }

}