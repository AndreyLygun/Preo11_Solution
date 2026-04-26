using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.SecuritySRQ;

namespace ricoh.ServiceRequests
{
  partial class SecuritySRQSharedHandlers
  {

    public virtual void ValidOnChanged(Sungero.Domain.Shared.DateTimePropertyChangedEventArgs e)
    {
      if (!_obj.ValidFrom.HasValue)
        _obj.ValidFrom = _obj.ValidOn;
      if (!_obj.ValidTill.HasValue)
        _obj.ValidTill = _obj.ValidOn;
    }

  }
}