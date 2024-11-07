using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4Visitor;

namespace ricoh.ServiceRequests
{
  partial class Pass4VisitorServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);      
      if (string.IsNullOrWhiteSpace(_obj.Visitors)) return;      
      var visitors = _obj.Visitors.Split('\n');
      if (visitors.Count() > 4) {
        _obj.Subject = "Количество посетителей: " + visitors.Count();
      } else {
        _obj.Subject = "Посетители: " + Functions.Module.List2SmartStr(visitors.ToList(), 4, 120);
      }
    }
  }

}