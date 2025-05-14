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
      var ValidOn = _obj.ValidOn.Value.ToShortDateString();
      if (visitors.Count() > 4) {
        _obj.Subject = string.Format("Посетители на {0}: {1} чел",  ValidOn, visitors.Count());
      } else {
        _obj.Subject = string.Format("Посетители на {0}: {1}",  ValidOn, Functions.Module.List2SmartStr(visitors.ToList(), 4, 120));
      }
      _obj.Name = string.Format("Заявка № {0} от {1}: {2}", _obj.Id, _obj.Renter, _obj.Subject);
    }
  }

}