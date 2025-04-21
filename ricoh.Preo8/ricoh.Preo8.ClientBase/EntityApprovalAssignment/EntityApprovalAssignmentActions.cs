using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.Preo8.EntityApprovalAssignment;

namespace ricoh.Preo8.Client
{
  partial class EntityApprovalAssignmentActions
  {
    public override void ExtendDeadline(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      base.ExtendDeadline(e);
    }

    public override bool CanExtendDeadline(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      var attach = _obj.AllAttachments.FirstOrDefault();
      // Нельзя запросить продление срока, если к заданию прикреплена сервисная заявка
      return base.CanExtendDeadline(e) && (ServiceRequests.BaseSRQs.As(attach) == null);
    }

  }



}