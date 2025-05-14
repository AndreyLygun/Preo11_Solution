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
      var names = new List<string>();
      foreach(var emp in _obj.Employees) {
        if (!string.IsNullOrWhiteSpace(emp.Name))
          names.Add(emp.Name.Split(' ')[0]);
        }
      _obj.Subject = string.Format("Пропуска для сотрудников ({0})", Functions.Module.List2SmartStr(names, 5, 50));
      _obj.Name = string.Format("Заявка № {0} от {1}: {2}", _obj.Id, _obj.Renter, _obj.Subject);
    }
  }

}