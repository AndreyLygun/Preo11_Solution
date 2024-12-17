using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Workflow;

namespace RICOH.ProcessBlockCollection.Server.ProcessBlockCollectionBlocks
{
  partial class SendMailHandlers
  {

    public virtual void SendMailExecute()
    {
      var message = Mail.CreateMailMessage();
      message.To.Add(_block.To);
      message.Subject = _block.Subject;
      Mail.Send(message);
    }
  }

}