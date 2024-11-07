using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.BaseSRQ;

namespace ricoh.ServiceRequests
{
  partial class BaseSRQSharedHandlers
  {

    public virtual void RenterChanged(ricoh.ServiceRequests.Shared.BaseSRQRenterChangedEventArgs e)
    {
      // TODO: продумать механизм изменения прав доступа при изменении арендатора в интерфейсе DRX
      //      if (e.OldValue != null) _obj.AccessRights.RevokeAll(e.OldValue);
      //      if (e.NewValue != null) _obj.AccessRights.Grant(e.NewValue, DefaultAccessRightsTypes.Change);
    }

    
    public virtual void RequestStateChanged(Sungero.Domain.Shared.EnumerationPropertyChangedEventArgs e)
    {
      // Запускаем заявку на согласование
//      if (Equals(e.OldValue, BaseSRQ.RequestState.Draft) & Equals(e.NewValue, BaseSRQ.RequestState.OnReview)) {
//        _obj.Save();
//        Functions.BaseSRQ.Remote.CreateAndStartRequestApprovalTask(_obj);
//      }

    }
  }
}