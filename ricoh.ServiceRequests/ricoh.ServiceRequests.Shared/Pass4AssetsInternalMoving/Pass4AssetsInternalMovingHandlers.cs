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

    public virtual void InventoryChanged(Sungero.Domain.Shared.CollectionPropertyChangedEventArgs e)
    {
      var s = "";
      foreach(var item in _obj.Inventory) {
        if (String.IsNullOrWhiteSpace(item.Name)) continue;
        s = s + item.Name + " \t("
          + "Размер: " + (String.IsNullOrWhiteSpace(item.Size)?"не указан":item.Size) + ") "
          + "\tКол-во: " + (String.IsNullOrWhiteSpace(item.Quantity)?"не указано":item.Quantity)
          + (String.IsNullOrWhiteSpace(item.Note)?(", " + item.Note):"")  + "\r";
      }
      _obj.InventoryString  = s;
    }

    public virtual void ElevatorTimeSpanChanged(Sungero.Domain.Shared.CollectionPropertyChangedEventArgs e)
    {
      if (_obj.ElevatorTimeSpan.Count > 0) {
        var s = "";
        foreach(var span in _obj.ElevatorTimeSpan) {
          s = s + span.Name + "; ";          
        }
        _obj.ElevatorTimeSpanString = s;
      } else {
        _obj.ElevatorTimeSpanString = "Лифт не требуется";
      }
    }

    public virtual void BuildingMaterialsChanged(Sungero.Domain.Shared.BooleanPropertyChangedEventArgs e)
    {
      _obj.BuildingMaterialsString = (_obj.BuildingMaterials??false)?"Да":"Нет";
    }

  }
}