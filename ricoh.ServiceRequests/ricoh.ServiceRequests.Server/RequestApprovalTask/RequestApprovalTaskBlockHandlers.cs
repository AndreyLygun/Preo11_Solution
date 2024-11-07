using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Workflow;
using ricoh.ServiceRequests.RequestApprovalTask;
using ThisBlock = ricoh.ServiceRequests.Server.RequestApprovalTaskBlocks.ChangeRequestStatusHandlers;

namespace ricoh.ServiceRequests.Server.RequestApprovalTaskBlocks
{
  partial class ProcessVisitorsHandlers
  {

    public virtual void ProcessVisitorsExecute()
    {
      var request = SecuritySRQs.As(_obj.Request4Approval.BaseSRQs.FirstOrDefault());
      if (request == null | string.IsNullOrWhiteSpace(request.Visitors)) return;
      var names = request.Visitors.Split('\n');
      var visitors2delete = Visitors.GetAll().Where(v => Equals(v.Request, request));
      foreach(var visitor in visitors2delete)
        Visitors.Delete(visitor);
      
      foreach(var name in names) {
        if (string.IsNullOrWhiteSpace(name)) continue;
        var visitor = Visitors.Create();
        visitor.Name = name;
        visitor.Request = request;
        visitor.ValidOn = request.ValidOn;
        visitor.Renter = request.Renter;
        visitor.AccessRights.Grant(request.Renter, DefaultAccessRightsTypes.Read);
        visitor.AccessRights.Grant(request.Author, DefaultAccessRightsTypes.Read);
        visitor.Save();
      }
    }
  }

  partial class ChangeRequestStatusHandlers
  {

    public virtual void ChangeRequestStatusExecute()
    {
      var request = _obj.Request4Approval.BaseSRQs.FirstOrDefault();
      if (request == null) return;
      if (_block.NewStatus ==   ThisBlock.NewStatus.Draft) {
        request.RequestState = ServiceRequests.BaseSRQ.RequestState.Draft;
      } else if (_block.NewStatus == ThisBlock.NewStatus.OnReview) {
        request.RequestState = ServiceRequests.BaseSRQ.RequestState.OnReview;
      } else if (_block.NewStatus == ThisBlock.NewStatus.Approved) {
        request.RequestState = ServiceRequests.BaseSRQ.RequestState.Approved;
      } else if (_block.NewStatus == ThisBlock.NewStatus.Denied) {
        request.RequestState = ServiceRequests.BaseSRQ.RequestState.Denied;
      } else if (_block.NewStatus == ThisBlock.NewStatus.Done) {
        request.RequestState = ServiceRequests.BaseSRQ.RequestState.Done;
      } else if (_block.NewStatus == ThisBlock.NewStatus.Closed) {
        request.RequestState = ServiceRequests.BaseSRQ.RequestState.Closed;
      }
      
    }
  }

}