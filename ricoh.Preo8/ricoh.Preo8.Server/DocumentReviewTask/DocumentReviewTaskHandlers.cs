using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.Preo8.DocumentReviewTask;

namespace ricoh.Preo8
{
  partial class DocumentReviewTaskServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
    }

    public override void BeforeAbort(Sungero.Workflow.Server.BeforeAbortEventArgs e)
    {
      base.BeforeAbort(e);
      
      var request = ServiceRequests.BaseSRQs.As(_obj.AllAttachments.FirstOrDefault());
      if (request != null) {
        request.RequestState = ServiceRequests.BaseSRQ.RequestState.Draft;
      }
    }
  }

}