using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Car;

namespace ricoh.ServiceRequests
{
  partial class PermanentPass4CarParkingPlacePropertyFilteringServerHandler<T>
  {

    public override IQueryable<T> ParkingPlaceFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      query = base.ParkingPlaceFiltering(query, e);
      query = query.Where(place => place.Pass == null);
      return query;
    }
  }


  partial class PermanentPass4CarServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      _obj.ValidOn = Calendar.Today.AddDays(1);
      _obj.ValidFrom = _obj.ValidOn;
      _obj.ValidTill = Calendar.SqlMaxValue;
    }

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);      
      var subject = "Постоянная парковка " + _obj.ParkingPlace.Name;
      _obj.Name = $"Постоянная парковка ({_obj.Renter.Name}): {_obj.ParkingPlace.Name}";      
    }
  }

}