using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Site;

namespace ricoh.ServiceRequests
{
  partial class SiteServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      e.AddInformation("Чтобы перенести изменение на портал, нажмите \"Обновить\" в правом верхнем углу на странице \"Настройки\" на портале ");
    }
  }


}