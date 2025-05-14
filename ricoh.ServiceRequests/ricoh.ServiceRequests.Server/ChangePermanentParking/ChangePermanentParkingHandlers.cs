using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.ChangePermanentParking;

namespace ricoh.ServiceRequests
{
  partial class ChangePermanentParkingServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      _obj.Subject  = string.Format("Изменение на парковочном месте {0}", _obj.ParkingPlace.Name);
      _obj.Name = string.Format("Заявка № {0} от {1}: {2}", _obj.Id, _obj.Renter, _obj.Subject);
    }
  }


}