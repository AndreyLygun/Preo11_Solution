using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.SecuritySRQ;

namespace ricoh.ServiceRequests
{
  partial class SecuritySRQParkingPlacePropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> ParkingPlaceFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      if (!Equals(_obj.Renter, Renters.Null))
        query = query.Where(site => site.Renter.Equals(_obj.Renter));
          else query = query.Where(site => false); // Арендатор в завявке не выбран, поэтому возвращаем пустой список.
      return query;
    }
  }

  partial class SecuritySRQFilteringServerHandler<T>
  {

    public override IQueryable<T> Filtering(IQueryable<T> query, Sungero.Domain.FilteringEventArgs e)
    {
      if (_filter == null) return query;
      query = base.Filtering(query, e);
      return query;
    }
  }

  partial class SecuritySRQServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      if (_obj.CarNumber != null)
        _obj.CarNumber = _obj.CarNumber.Replace(" ", "").ToUpper();
    }

    private void AddOrUpdateCar() {
      var car = Cars.GetAll(c => c.Pass.Equals(_obj)).FirstOrDefault();
      if (car == null) {
        car = Cars.Create();
      } else {
        if (string.IsNullOrWhiteSpace(_obj.CarNumber)) {
          // Госномер не заполнен, считаем, что машина отсутствует
          Cars.Delete(car);
          return;
        }
      }
      car.CarModel = _obj.CarModel;
      car.CarNumber = _obj.CarNumber;
      car.ParkingPlace = _obj.ParkingPlace;
      car.ValidFrom = _obj.ValidFrom;
      car.ValidTill = _obj.ValidTill;
      car.Pass = _obj;
      car.Renter = _obj.Renter;
      car.Save();
    }
    
    private void RemoveCar() {
      var car = Cars.GetAll(c => c.Pass.Equals(_obj)).FirstOrDefault();
      if (car != null)  {
        Cars.Delete(car);
      }
    }
    
    public override void AfterSave(Sungero.Domain.AfterSaveEventArgs e)
    {
      base.AfterSave(e);
      if (Functions.BaseSRQ.isAllowed(_obj)) {
        Sungero.Core.AccessRights.AllowRead(
          () => AddOrUpdateCar()
         );
      } else {
        Sungero.Core.AccessRights.AllowRead(
          () => RemoveCar()
         );
      }
    }

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