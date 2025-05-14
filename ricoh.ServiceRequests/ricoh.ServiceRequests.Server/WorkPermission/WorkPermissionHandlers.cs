using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.WorkPermission;

namespace ricoh.ServiceRequests
{
  partial class WorkPermissionServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      _obj.Subject = "Выполнение работ (" + ServiceRequests.PublicFunctions.Module.TrimText(_obj.Work, 50) + ")";
      _obj.Name = string.Format("Заявка № {0} от {1}: {2}", _obj.Id, _obj.Renter, _obj.Subject);
    }
  }

}