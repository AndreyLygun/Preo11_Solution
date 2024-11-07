using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Employee;

namespace ricoh.ServiceRequests
{
  partial class PermanentPass4EmployeeServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      if (_obj.EmployeesInAttachedFile??false) {
        _obj.Subject = "Несколько сотрудников";
      } else {
        _obj.Subject = _obj.EmployeeName + " (" + _obj.EmployeePosition + ")";
      }

    }

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      _obj.EmployeesInAttachedFile = false;
    }
  }

}