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
    public virtual void CloseRequest(Sungero.Domain.Client.ExecuteActionArgs e)
    {
    }

    public virtual bool CanCloseRequest(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return false;
    }


    
    public virtual void ShowPrintForm(Sungero.Domain.Client.ExecuteActionArgs e)
    {
    }

    public virtual bool CanShowPrintForm(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return Users.Current.IncludedIn(Roles.Administrators);
    }

    public virtual void Send2ProcessApproval(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      _obj.Save();
      var task = Sungero.DocflowApproval.PublicFunctions.Module.Remote.CreateDocumentFlowTask(_obj);
      task.Start();
      e.CloseFormAfterAction = true;      
    }

    public virtual bool CanSend2ProcessApproval(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _obj.RequestState == ServiceRequests.BaseSRQ.RequestState.Draft;
    }


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