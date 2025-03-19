using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.StopPermanentPass4Car;

namespace ricoh.ServiceRequests
{
  partial class StopPermanentPass4CarPermanentPass4CarPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> PermanentPass4CarFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      query = query.Where(pass => (pass.Renter.Equals(_obj.Renter)) && (pass.ValidTill >= Calendar.Today) && (Functions.BaseSRQ.isAllowed(_obj)));
      return query;
    }
  }

  partial class StopPermanentPass4CarServerHandlers
  {

    public override void AfterSave(Sungero.Domain.AfterSaveEventArgs e)
    {
      base.AfterSave(e);
      if (Functions.BaseSRQ.isAllowed(_obj)) {
        var cars = Cars.GetAll(c => c.Renter.Equals(_obj.Renter) && c.CarNumber.ToLower().Equals(_obj.CarNumber.ToLower()));
        foreach(var car in cars) {
          if (car.ValidTill > _obj.ValidTill) 
            car.ValidTill = _obj.ValidTill;
          car.Pass = _obj;
        }
      }
    }

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      _obj.Subject = _obj.CarNumber;
    }
  }

}