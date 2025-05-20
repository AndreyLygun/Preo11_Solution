using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Renter;

namespace ricoh.ServiceRequests
{
  partial class RenterServerHandlers
  {

    public override void AfterSave(Sungero.Domain.AfterSaveEventArgs e)
    {
      var nonInteractiveUsers = Roles.GetAll(r => r.Name == "Неинтерактивные пользователи").FirstOrDefault();
      if (nonInteractiveUsers == null) return;
      if (!_obj.IncludedIn(nonInteractiveUsers)) 
        nonInteractiveUsers.RecipientLinks.AddNew().Member = _obj;
    }

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      _obj.Department = 0;      
    }
  }

}