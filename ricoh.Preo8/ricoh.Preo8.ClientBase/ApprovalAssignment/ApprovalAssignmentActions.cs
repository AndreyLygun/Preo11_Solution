using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.Preo8.ApprovalAssignment;

namespace ricoh.Preo8.Client
{
  partial class ApprovalAssignmentActions
  {

    public virtual void Deny(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      var dlg=Dialogs.CreateInputDialog("Укажите причину отказа");
      var reason = dlg.AddString("Причина отказа", true);
      var result = dlg.Show();
      if (!result.Equals(DialogButtons.Ok)) {
        return;
      }
      _obj.ActiveText = "Отказано: " + reason.Value;
      var request = ServiceRequests.BaseSRQs.As(_obj.AllAttachments.FirstOrDefault());
      if (request.AccessRights.IsGranted(DefaultAccessRightsTypes.Change, Users.Current)) {
        request.RequestStateDescription += Calendar.Now + ": " + _obj.ActiveText + " (" + Users.Current.Name + ")\n";
      }
    }

    public virtual bool CanDeny(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      return true;
    }

    public virtual void Approve(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      var request = ServiceRequests.BaseSRQs.As(_obj.AllAttachments.FirstOrDefault());
      if (request.AccessRights.IsGranted(DefaultAccessRightsTypes.Change, Users.Current)) {
        request.RequestStateDescription += Calendar.Now + ": " + _obj.ActiveText + " (" + Users.Current.Name + ")\n";
      }
      //Functions.ApprovalAssignment.Remote.AddProcessStep(_obj);
    }

    public virtual bool CanApprove(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      return true;
    }

  }

}