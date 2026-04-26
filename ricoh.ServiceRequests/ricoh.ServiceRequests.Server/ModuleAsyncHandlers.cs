using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace ricoh.ServiceRequests.Server
{
  public class ModuleAsyncHandlers
  {

    public virtual void SendEMail(ricoh.ServiceRequests.Server.AsyncHandlerInvokeArgs.SendEMailInvokeArgs args)
    {
      var logPostfix = string.Format("To = '{0}', Subject = '{1}'", args.Addresses, args.Subject);
      Logger.DebugFormat("SendNotificationMail. Start. {0}", logPostfix);

      if (string.IsNullOrWhiteSpace(args.Addresses))
      {
        Logger.ErrorFormat("SendNotificationMail. Addresses is empty ({0})", logPostfix);
        return;
      }

      try
      {
        using (var smtpClient = new System.Net.Mail.SmtpClient())
          using (var message = new System.Net.Mail.MailMessage())
        {
          foreach(string rec in args.Addresses.Split(',')) {
            message.To.Add(rec);            
          }

          message.Subject = args.Subject ?? string.Empty;
          message.Body = args.Body ?? string.Empty;
          message.SubjectEncoding = System.Text.Encoding.UTF8;
          message.BodyEncoding = System.Text.Encoding.UTF8;
          message.HeadersEncoding = System.Text.Encoding.UTF8;
          message.IsBodyHtml = false;

          smtpClient.Timeout = 10000;
          smtpClient.Send(message);
        }

        Logger.DebugFormat("SendNotificationMail. Sent successfully. {0}", logPostfix);
      }
      catch (System.Net.Mail.SmtpException ex)
      {
        Logger.ErrorFormat("SendNotificationMail. SMTP error. {0}", ex, logPostfix);
      }
      catch (System.Net.Sockets.SocketException ex)
      {
        Logger.ErrorFormat("SendNotificationMail. Socket error. {0}", ex, logPostfix);
      }
      catch (Exception ex)
      {
        Logger.ErrorFormat("SendNotificationMail. Unexpected error. {0}", ex, logPostfix);
      }

      Logger.DebugFormat("SendNotificationMail. Finish. {0}", logPostfix);
    }

  }
}