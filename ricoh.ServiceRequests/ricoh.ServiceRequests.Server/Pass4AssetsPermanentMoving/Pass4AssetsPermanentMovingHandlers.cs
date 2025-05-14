using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4AssetsPermanentMoving;

namespace ricoh.ServiceRequests
{
  partial class Pass4AssetsPermanentMovingServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      _obj.CarsString = "";
      foreach(var car in _obj.Cars) {
        _obj.CarsString += string.Format("{0} / {1}", car.Model, car.Number);
        if (!string.IsNullOrWhiteSpace(car.Note)) _obj.CarsString += string.Format("({0})", car.Note);
        _obj.CarsString += "\r";          
      }
    }
  }

}