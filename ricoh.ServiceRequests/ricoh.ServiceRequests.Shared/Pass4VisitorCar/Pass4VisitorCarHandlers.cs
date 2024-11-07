using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4VisitorCar;

namespace ricoh.ServiceRequests
{
  partial class Pass4VisitorCarSharedHandlers
  {

    public virtual void ValidOnDateTimeChanged(Sungero.Domain.Shared.DateTimePropertyChangedEventArgs e)
    {
      _obj.ValidOn = _obj.ValidOnDateTime;      
    }

  }
}