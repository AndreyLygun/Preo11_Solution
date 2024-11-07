using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Visitor;

namespace ricoh.ServiceRequests
{
  partial class VisitorFilteringServerHandler<T>
  {

    public override IQueryable<T> Filtering(IQueryable<T> query, Sungero.Domain.FilteringEventArgs e)
    {
      if (_filter == null)
        return query;
      if (_filter.Yesterday) {
        query = query.Where(v => v.ValidOn.Equals(Calendar.Today.AddDays(-1)));
      } else if (_filter.Today) {
        query = query.Where(v => v.ValidOn.Equals(Calendar.Today));
      } else if (_filter.Tomorrow) {
        query = query.Where(v => v.ValidOn.Equals(Calendar.Today.AddDays(1)));
      } else if (_filter.Range) {
        if (_filter.DateRangeFrom != null)
          query = query.Where(v => v.ValidOn >= _filter.DateRangeFrom);
        if (_filter.DateRangeTo != null)
          query = query.Where(v => v.ValidOn <= _filter.DateRangeTo);
      }
      return query;
    }
  }

  partial class VisitorServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      _obj.ValidOn = Calendar.Today;
    }
  }


}