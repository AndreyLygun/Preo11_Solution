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
      message.To.Add(_block.To);
      message.Subject = _block.Subject;
      message.Body = _block.Body;
      Mail.Send(message);
    }
  }

}