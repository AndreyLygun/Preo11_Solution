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

    /// <summary>
    /// Возвращает true, если заявка находится в одном из "согласованных" состоянии (согласовано, исполнено), false, если заявка является черновиком, на согласовани или отказано
    /// </summary>
    ///
    public bool isAllowed()
    {
      return _obj.RequestState == ServiceRequests.BaseSRQ.RequestState.Approved
        || _obj.RequestState == ServiceRequests.BaseSRQ.RequestState.Done
        || _obj.RequestState == ServiceRequests.BaseSRQ.RequestState.Closed;
    }


    /// <summary>
    /// 
    /// </summary>
    public void UpdateVersion(string txt, string fileExtension)
    {
      using (var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(txt)))
      {
        if (!_obj.HasVersions)
          _obj.CreateVersionFrom(stream, fileExtension);
        else {
          var lastVersion = _obj.LastVersion;
          _obj.DeleteVersion(lastVersion);
          _obj.CreateVersionFrom(stream, fileExtension);
        }
      }
    }


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
    

    public Sungero.Core.StateView GetApprovalStatus() {
      var stateView = StateView.Create();
      var tasks = Sungero.Workflow.Tasks.GetAll()
        .Where(t => t.AttachmentDetails.Any(a => a.AttachmentId == _obj.Id))
        .OrderByDescending(t => t.Created);
      var task = tasks.FirstOrDefault();
      if (task == null) return stateView;
      stateView.AddDefaultLabel("Заголовок");
      //      foreach (var task in tasks) {
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
          var result = Sungero.DocflowApproval.EntityApprovalAssignments.Info.Properties.Result.GetLocalizedValue(assignment.Result.Value);
          col.AddLabel("Результат: " + result);
          col.AddLineBreak();
          
        } else {
          var status = Sungero.DocflowApproval.EntityApprovalAssignments.Info.Properties.Result.GetLocalizedValue(assignment.Status);
          col.AddLabel(status);
        }
        var rightContent = subblock.AddContent();
      }
      //      }
      return stateView;
    }

  }
}