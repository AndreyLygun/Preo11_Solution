using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Car;

namespace ricoh.ServiceRequests
{
  partial class PermanentPass4CarParkingFloorOldPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> ParkingFloorOldFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      query = query.Where(s => s.Type.Equals(Site.Type.ParkingSite));
      return query;
    }
  }

  partial class PermanentPass4CarServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      _obj.ValidOn = Calendar.Today.AddDays(1);
      _obj.ValidFrom = Calendar.Today;
      _obj.ValidTill = Calendar.Today.AddDays(365);
    }

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      string action = "";
      if (_obj.Action == PermanentPass4Car.Action.NewPass) action = "Оформление автопропуска: ";
      if (_obj.Action == PermanentPass4Car.Action.ContinuePass) action = "Продление автопропуска: ";            
      var subject = action + _obj.CarModel + "/" +_obj.CarNumber;;
      _obj.Subject = subject.Substring(0, Math.Min(subject.Length, 250));           
    }
  }

}