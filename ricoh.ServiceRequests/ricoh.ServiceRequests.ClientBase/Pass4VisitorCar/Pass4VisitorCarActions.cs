using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4VisitorCar;

namespace ricoh.ServiceRequests.Client
{
  partial class Pass4VisitorCarActions
  {
    public override void CloseRequest(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      base.CloseRequest(e);
    }

    public override bool CanCloseRequest(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

  }

}