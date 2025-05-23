using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Workflow;

namespace RICOH.ProcessBlockCollection.Server.ProcessBlockCollectionBlocks
{
  partial class ProcessCarPassHandlers
  {

    public virtual void ProcessCarPassExecute()
    {
      var securityRequest = ricoh.ServiceRequests.SecuritySRQs.As(_obj.AllAttachments.Where(a => ricoh.ServiceRequests.SecuritySRQs.Is(a)).FirstOrDefault());
      if (securityRequest == null) return;
      var carModel = securityRequest.CarModel.Trim();
      var carNumber = securityRequest.CarNumber;
      var Renter = securityRequest.Renter;
      var cars = ricoh.ServiceRequests.Cars.GetAll().Where(car =>
                                                           car.CarModel.ToUpper() == carModel.ToUpper() &&
                                                           car.CarNumber.ToUpper() == carNumber.ToUpper() &&
                                                           Equals(car.Pass, securityRequest));
      if (_block.Action.Equals(ProcessBlockCollectionBlocks.ProcessCarPassHandlers.Action.Add)) {
        if (cars == null) {
          var car = ricoh.ServiceRequests.Cars.Create();
          car.CarModel = carModel;
          car.CarNumber = carNumber;
          car.Renter = Renter;
          car.Pass = securityRequest;
          car.ValidFrom = securityRequest.ValidFrom;
          car.ValidTill = securityRequest.ValidTill;
          car.AccessRights.Grant(Renter, DefaultAccessRightsTypes.Change);
          car.Save();
        } else {
          foreach(var car in cars) {
            car.ValidFrom = securityRequest.ValidFrom;
            car.ValidTill = securityRequest.ValidTill;
          }
        }
      } else if (_block.Action.Equals(ProcessBlockCollectionBlocks.ProcessCarPassHandlers.Action.Remove ))  {
        foreach(var car in cars) {
          ricoh.ServiceRequests.Cars.Delete(car);
        }
      }
      
      

    }
  }

  partial class SendMailHandlers
  {

    public virtual void SendMailExecute()
    {
      var message = Mail.CreateMailMessage();
      if (string.IsNullOrWhiteSpace(_block.To) || string.IsNullOrWhiteSpace(_block.Subject)) return;
      message.Subject = _block.Subject;
      message.Body = _block.Body;
      message.IsBodyHtml = true;
      message.To.Add(_block.To);
      foreach(var employee in _block.ToEmployee)
        if (!string.IsNullOrWhiteSpace(employee.Email))
          message.To.Add(employee.Email);
      if (_block.DocumentToAttach != null) {
        if (_block.DocumentToAttach.HasVersions)
          message.AddAttachment(_block.DocumentToAttach.LastVersion);
        else
          Logger.DebugFormat("В блоке {0} в документе {1} отсутствует версия для отправке на почту {2}",
                             _block.Id, _block.DocumentToAttach.Name, string.Join(",", message.To));
      }
      try {
        Mail.Send(message);
      } catch(Sungero.Core.MailException e) {
        if (_block.RetrySettings.RetryIteration < 2) {
          _block.RetrySettings.NextRetryTime = Calendar.Now.AddMinutes(15);
          _block.RetrySettings.Retry = true;
        }        
        Logger.ErrorFormat("Ошибка при отправке e-mail в блоке {0} (Id: {1}): {2}", _block.Title, _block.Id, e.Message);
      }

    }
  }

}