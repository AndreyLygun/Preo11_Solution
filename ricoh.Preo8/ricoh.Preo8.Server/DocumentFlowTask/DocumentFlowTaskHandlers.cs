using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.Preo8.DocumentFlowTask;

namespace ricoh.Preo8
{
  partial class DocumentFlowTaskServerHandlers
  {

    public override void BeforeAbort(Sungero.Workflow.Server.BeforeAbortEventArgs e)
    {
      foreach(var attach in _obj.AllAttachments) {
        var request = ricoh.ServiceRequests.BaseSRQs.As(attach);
        if (request != null && request.RequestState != ricoh.ServiceRequests.BaseSRQ.RequestState.Draft)  {
          request.RequestState = ServiceRequests.BaseSRQ.RequestState.Draft;
          request.Save();
        }
      }
      base.BeforeAbort(e);
    }
  }

}