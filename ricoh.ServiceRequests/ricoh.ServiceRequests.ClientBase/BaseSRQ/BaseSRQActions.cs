using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.BaseSRQ;

namespace ricoh.ServiceRequests.Client
{

  partial class BaseSRQActions
  {

    // Отправить заявку на согласование
    public virtual void Send2Approval(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      _obj.Save();
      Functions.BaseSRQ.Remote.CreateAndStartRequestApprovalTask(_obj);
      e.CloseFormAfterAction = true;
    }

    public virtual bool CanSend2Approval(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _obj.RequestState == BaseSRQ.RequestState.Draft;
    }
    
    
    
  }

}