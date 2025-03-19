using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Car;

namespace ricoh.ServiceRequests
{
  partial class CarFilteringServerHandler<T>
  {

    public override IQueryable<T> Filtering(IQueryable<T> query, Sungero.Domain.FilteringEventArgs e)
    {
      if (_filter == null)
        return query;
      if (_filter.Today || _filter.Tomorrow)
        query = query.Where(car => Calendar.Between(Calendar.Today, car.ValidFrom??Calendar.SqlMinValue, car.ValidTill??Calendar.SqlMaxValue) && _filter.Today
                            || Calendar.Between(Calendar.Today.AddDays(1), car.ValidFrom??Calendar.SqlMinValue, car.ValidTill??Calendar.SqlMaxValue) && _filter.Tomorrow);
      return query;
    }
  }

  partial class CarServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      _obj.Name = _obj.CarModel + " (" + _obj.CarNumber + "), " + _obj.Renter.Name;
    }
  }

}