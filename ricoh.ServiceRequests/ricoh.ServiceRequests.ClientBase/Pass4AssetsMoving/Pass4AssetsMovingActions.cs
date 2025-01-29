using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4AssetsMoving;

namespace ricoh.ServiceRequests.Client
{
  partial class Pass4AssetsMovingActions
  {
    public override void ShowPrintForm(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      base.ShowPrintForm(e);
      var report = Reports.GetPass4AssetsMovingReport();
      report.Entity = _obj;
      report.Open();
    }

    public override bool CanShowPrintForm(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return base.CanShowPrintForm(e);
    }

  }


}