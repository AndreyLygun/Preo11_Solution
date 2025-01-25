using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4AssetsInternalMoving;

namespace ricoh.ServiceRequests
{
  partial class Pass4AssetsInternalMovingServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      // Собираем subject документа из названий ТМЦ
      base.BeforeSave(e);
      var assets = new List<string> {};
      foreach(var asset in  _obj.Inventory.Take(4)) {
        assets.Add(asset.Name);
      }
      _obj.Subject = "Внутр.перемещение ТМЦ"
        + " (" + Functions.Module.List2SmartStr(assets, 3, 250)+")";      
    }

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      _obj.Elevator = false;
      _obj.StorageRoom = false;      
    }
  }


}