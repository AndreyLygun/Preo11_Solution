using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Car;

namespace ricoh.ServiceRequests
{
  partial class PermanentPass4CarParkingFloorPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> ParkingFloorFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      query = query.Where(s => s.Type.Equals(Site.Type.ParkingSite));
      return query;
    }
  }

  partial class PermanentPass4CarServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      var subject = _obj.CarModel + "/" +_obj.CarNumber;;
      _obj.Subject = subject.Substring(0, Math.Min(subject.Length, 250));           
    }
  }

}