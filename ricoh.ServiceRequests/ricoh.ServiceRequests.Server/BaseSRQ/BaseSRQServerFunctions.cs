using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.BaseSRQ;
using Sungero.Workflow.Task;

namespace ricoh.ServiceRequests.Server
{
  partial class BaseSRQFunctions
  {


    [Remote]
    public long CreateAndStartRequestApprovalTask()
    {
      var task = RequestApprovalTasks.Create();
      if (_obj.Author != null) task.AccessRights.Grant(_obj.Author, DefaultAccessRightsTypes.Read);
      if (_obj.Renter != null) task.AccessRights.Grant(_obj.Renter, DefaultAccessRightsTypes.Read);
      _obj.AccessRights.Save();
      task.Subject = "Согласуйте " + _obj.Name;
      task.Request4Approval.BaseSRQs.Add(_obj);
      task.Start();
      return task.Id;
    }
    

    public new Sungero.Core.StateView GetStateViewXml() {
      var tasks = Sungero.Workflow.Tasks.GetAll()
        .Where(t => t.AttachmentDetails
               .Any(a => a.AttachmentId == _obj.Id))
        .OrderBy(t => t.Created);
      var stateView = StateView.Create();
      stateView.AddDefaultLabel("Заголовок");
      foreach (var task in tasks) {
        var block = stateView.AddBlock();
        block.Entity = task;
        block.DockType = DockType.Bottom;
        block.IsExpanded = true;
        block.ShowBorder = false;
        block.AssignIcon(StateBlockIconType.User, StateBlockIconSize.Small);
        block.AddLabel( GetUserActionText(task.Author, "Заявка отправлена " + task.Created.ToString(), task.StartedBy));

        var assignments = Sungero.Workflow.Assignments.GetAll(a => Equals(a.Task, task)).OrderBy(a => a.Created);
        var boldStyle = StateBlockLabelStyle.Create();
        boldStyle.FontWeight = FontWeight.Bold;

        foreach(var assignment in assignments) {
          var subblock = block.AddChildBlock();
          subblock.AssignIcon(StateBlockIconType.OfEntity, StateBlockIconSize.Large);
          subblock.Entity = assignment;
          var col = subblock.AddContent();
          col.AddLabel(assignment.ThreadSubject, boldStyle);          
          col.AddLineBreak();
          if (assignment.Result.HasValue) {
            col.AddLabel(assignment.CompletedBy + " за " + assignment.Performer.Name + " " + assignment.Completed.ToUserTime());
            col.AddLineBreak();
            col.AddLabel(assignment.ActiveText);
            col.AddLineBreak();
            
            col.AddLabel("Результат: " + RequestApprovalAssignments.Info.Properties.Result.GetLocalizedValue(assignment.Result.Value));
            col.AddLineBreak();
            
          } else {           
            var status = RequestApprovalAssignments.Info.Properties.Status.GetLocalizedValue(assignment.Status);
            col.AddLabel(status);
          }
          //          col.AddLabel("Result.ToString " + assignment.Result.Value.ToString());
          //          col.AddLineBreak();
          //          col.AddLabel("Subject " + assignment.Subject);

          var rightContent = subblock.AddContent();

          //          rightContent.

        }
      }
      return stateView;
    }

  }
}