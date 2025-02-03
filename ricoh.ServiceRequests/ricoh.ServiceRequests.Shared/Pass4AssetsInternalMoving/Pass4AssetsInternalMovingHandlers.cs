using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4AssetsInternalMoving;

namespace ricoh.ServiceRequests
{
  partial class Pass4AssetsInternalMovingSharedHandlers
  {

    public virtual void ElevatorTimeSpanChanged(Sungero.Domain.Shared.CollectionPropertyChangedEventArgs e)
    {
      if (_obj.ElevatorTimeSpan.Count > 0) {
        _obj.Elevator = true;
        var s = "";
        foreach(var span in _obj.ElevatorTimeSpan) {
          s = s + span.Name + "; ";
        }
        _obj.ElevatorTimeSpanString = s;
      } else {
        _obj.Elevator = false;
        _obj.ElevatorTimeSpanString = "Лифт не требуется";
      }
    }

    public virtual void BuildingMaterialsChanged(Sungero.Domain.Shared.BooleanPropertyChangedEventArgs e)
    {
      _obj.BuildingMaterialsString = (_obj.BuildingMaterials??false)?"Да":"Нет";
    }

  }
}