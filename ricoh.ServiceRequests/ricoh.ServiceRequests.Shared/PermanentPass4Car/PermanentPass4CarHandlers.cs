using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Car;

namespace ricoh.ServiceRequests
{
  partial class PermanentPass4CarSharedHandlers
  {

    public virtual void CarsChanged(Sungero.Domain.Shared.CollectionPropertyChangedEventArgs e)
    {
      _obj.CarsString = "";
      foreach(var car in _obj.Cars) {
        if (string.IsNullOrWhiteSpace(car.Model) || string.IsNullOrWhiteSpace(car.Number)) continue;
        var note = string.IsNullOrWhiteSpace(car.Model)?"":string.Format(" ({0})", car.Note);
        _obj.CarsString += car.Model + " / " + car.Number + note + "\r";
      }      
    }
  }


}