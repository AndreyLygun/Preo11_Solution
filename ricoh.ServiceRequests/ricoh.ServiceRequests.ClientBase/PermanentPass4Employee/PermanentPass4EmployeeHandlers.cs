using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Employee;

namespace ricoh.ServiceRequests
{
  partial class PermanentPass4EmployeeClientHandlers
  {

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      base.Refresh(e);
      _obj.State.Controls.Control.IsVisible = _obj.EmployeePhoto == null;
    }

  }
}