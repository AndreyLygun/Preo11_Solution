using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4AssetsMoving;
using ricoh.ServiceRequests.Shared;

namespace ricoh.ServiceRequests
{
  partial class Pass4AssetsMovingFloorPropertyFilteringServerHandler<T>
  {

  }

  partial class Pass4AssetsMovingLoadingSitePropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> LoadingSiteFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      query = query.Where(s => s.Type.Equals(Site.Type.LoadingSite));
      return query;
    }
  }


  partial class Pass4AssetsMovingServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      var assets = new List<string> {};
      foreach(var asset in  _obj.Inventory.Take(4)) {
        assets.Add(asset.Name);        
      }
      _obj.Subject = _obj.Info.Properties.MovingDirection.GetLocalizedValue(_obj.MovingDirection) 
        + " (" + Functions.Module.List2SmartStr(assets, 3, 250)+")";
    }

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      _obj.Subject = "-";
      _obj.Elevator = false;
      _obj.StorageRoom = false;
    }
  }



}