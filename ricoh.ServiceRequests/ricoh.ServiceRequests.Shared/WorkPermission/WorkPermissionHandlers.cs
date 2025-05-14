using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.WorkPermission;

namespace ricoh.ServiceRequests
{
  partial class WorkPermissionSharedHandlers
  {

    public virtual void TillDateTimeChanged(Sungero.Domain.Shared.DateTimePropertyChangedEventArgs e)
    {
      _obj.ValidTill = _obj.TillDateTime;
    }

    public virtual void FromDateTimeChanged(Sungero.Domain.Shared.DateTimePropertyChangedEventArgs e)
    {
      _obj.ValidFrom = _obj.FromDateTime;           
    }

  }
}