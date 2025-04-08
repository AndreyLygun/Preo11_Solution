using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Car;

namespace ricoh.ServiceRequests.Client
{
  partial class PermanentPass4CarActions
  {
    public virtual void CreateChangingRequest(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var changingRequest = Functions.PermanentPass4Car.Remote.CreateChangingRequest();
      changingRequest.PermanentParkingPass = _obj;
      changingRequest.Show();
    }

    public virtual bool CanCreateChangingRequest(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _obj.RequestState == BaseSRQ.RequestState.Approved;
    }

    public override void ShowPrintForm(Sungero.Domain.Client.ExecuteActionArgs e)
    {      
    }

    public override bool CanShowPrintForm(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return false;
    }

  }

}