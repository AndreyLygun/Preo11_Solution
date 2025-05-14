using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.ZZZZPermanentPass4Car;

namespace ricoh.ServiceRequests.Client
{
  partial class ZZZZPermanentPass4CarActions
  {

    public override void ShowPrintForm(Sungero.Domain.Client.ExecuteActionArgs e)
    {      
    }

    public override bool CanShowPrintForm(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return false;
    }

  }

}