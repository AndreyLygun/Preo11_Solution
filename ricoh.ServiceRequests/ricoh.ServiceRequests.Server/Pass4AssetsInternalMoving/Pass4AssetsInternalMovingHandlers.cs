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
      var assetsNames = Functions.Module.List2SmartStr(assets, 3, 250);
      var ValidOn = _obj.ValidOn.Value.ToShortDateString();
      _obj.Subject = assetsNames;
      _obj.Name = $"Внутр.перемещение ТМЦ ({_obj.Renter.Name} на {ValidOn}: {assetsNames})";
    }
    


    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
    }
  }


}