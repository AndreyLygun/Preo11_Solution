using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.RequestApprovalAssignment;

namespace ricoh.ServiceRequests.Client
{
  partial class RequestApprovalAssignmentActions
  {
    public virtual void Deny(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      var dlg=Dialogs.CreateInputDialog("Укажите причину отказа");
      var reason = dlg.AddString("Причина отказа", true);
      var result = dlg.Show();
      if (!result.Equals(DialogButtons.Ok)) {
        e.Cancel();
        return;
      }
      _obj.ActiveText = "Отказано: " + reason.Value;
      var Task = RequestApprovalTasks.As(_obj.Task);
//      Task.ApprovalLog += Calendar.Now + ": " + _obj.ActiveText + "(" + Users.Current + ")";    
    }

    public virtual bool CanDeny(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      return true;
    }

    public virtual void Approve(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      var Task = RequestApprovalTasks.As(_obj.Task);
//      Task.ApprovalLog += Calendar.Now + ": " + _obj.ActiveText + "(" + Users.Current + ")";      
    }

    public virtual bool CanApprove(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      return true;
    }

  }


}