using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.SecuritySRQ;

namespace ricoh.ServiceRequests
{
  partial class SecuritySRQFilteringServerHandler<T>
  {

    public override IQueryable<T> Filtering(IQueryable<T> query, Sungero.Domain.FilteringEventArgs e)
    {
      if (_filter == null) return query;
      query = base.Filtering(query, e);
      if (_filter.Today) {
        query = query.Where(r => Calendar.Between(Calendar.Today, r.ValidFrom??Calendar.SqlMinValue, r.ValidTill??Calendar.SqlMaxValue));
        }
      if (_filter.Tomorrow) {
        query = query.Where(r => Calendar.Between(Calendar.Today.NextDay(), r.ValidFrom??Calendar.SqlMinValue, r.ValidTill??Calendar.SqlMaxValue));
        }
      return query;
    }
  }

  partial class SecuritySRQServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      _obj.ValidOn = Calendar.Today.AddWorkingDays(1);
      _obj.ValidFrom = _obj.ValidOn;
      _obj.ValidTill = _obj.ValidOn;
      _obj.ResponsibleName = Users.Current.Name;
    }
  }

}