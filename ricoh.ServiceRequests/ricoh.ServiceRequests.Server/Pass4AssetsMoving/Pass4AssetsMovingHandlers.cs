using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using System.Text;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4AssetsMoving;
using ricoh.ServiceRequests.Shared;

namespace ricoh.ServiceRequests
{
  partial class Pass4AssetsMovingFilteringServerHandler<T>
  {

    public override IQueryable<T> PreFiltering(IQueryable<T> query, Sungero.Domain.PreFilteringEventArgs e)
    {
      query = base.PreFiltering(query, e);
      return query;
    }
  }

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
      // Собираем subject документа из названий ТМЦ
      base.BeforeSave(e);
      var assets = new List<string> {};
      foreach(var asset in  _obj.Inventory.Take(4)) {
        assets.Add(asset.Name);
      }     
      var assetsNames = Functions.Module.List2SmartStr(assets, 3, 250);
      var direction = _obj.Info.Properties.MovingDirection.GetLocalizedValue(_obj.MovingDirection); 
      var ValidOn = _obj.ValidOn.Value.ToShortDateString();
      _obj.Subject = $"{direction} ({assetsNames})";      
      _obj.Name = string.Format("Заявка № {0} от {1}: {2} ({3})", _obj.Id, _obj.Renter, direction,assetsNames);
    }
  }



}