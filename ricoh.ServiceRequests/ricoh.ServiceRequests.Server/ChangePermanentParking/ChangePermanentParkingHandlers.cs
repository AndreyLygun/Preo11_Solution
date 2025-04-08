using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.ChangePermanentParking;

namespace ricoh.ServiceRequests
{
  partial class ChangePermanentParkingPermanentParkingPassPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> PermanentParkingPassFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      if (_obj.Renter != null) {
        query = query.Where(r => r.Renter.Equals(_obj.Renter));
      } else {
        query = query.Where(r => false);
      }
      return query;
    }
  }

}