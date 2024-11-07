using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.Preo8.ApprovalAssignment;

namespace ricoh.Preo8.Server
{
  partial class ApprovalAssignmentFunctions
  {
    [Remote]
    public void AddProcessStep() {
//      var request = ServiceRequests.BaseSRQs.As(_obj.AllAttachments.FirstOrDefault());
//      var processes = ServiceRequests.RequestProcesses.GetAll().Where(rp => (rp.Request.Equals(request)) & (rp.Task.Equals(_obj.Task)));
//      var process = processes.FirstOrDefault();
//      if (process == null) {
//        process = ServiceRequests.RequestProcesses.Create();
//        process.Request = request;
//        process.Task = _obj.Task; 
//      }
//      var step = process.Steps.AddNew();
//      step.DateTime = Calendar.Now.ToString();
//      step.Performer = Users.Current.Name;
//      step.Result = _obj.ActiveText;
//      step.Save();
//      process.Save();             
    }
  }
}