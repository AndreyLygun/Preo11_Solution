using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4AssetsMoving;

namespace ricoh.ServiceRequests
{
  partial class Pass4AssetsMovingSharedHandlers
  {

    public virtual void ElevatorChanged(Sungero.Domain.Shared.BooleanPropertyChangedEventArgs e)
    {
      _obj.ElevatorString = (_obj.Elevator??true)?"Требуется":"Не требуется";
    }

    public virtual void StorageRoomChanged(Sungero.Domain.Shared.BooleanPropertyChangedEventArgs e)
    {
      _obj.StorageRoomString = (_obj.StorageRoom??true)?"Требуется":"Не требуется";
    }

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

    public virtual void InventoryChanged(Sungero.Domain.Shared.CollectionPropertyChangedEventArgs e)
    {
      var s = "";
      foreach(var item in _obj.Inventory) {
        if (String.IsNullOrWhiteSpace(item.Name)) continue;
        s = s + item.Name + " \t("
          + "Размер: " + (String.IsNullOrWhiteSpace(item.Size)?"не указан":item.Size) + ") "
          + "\tКол-во: " + (String.IsNullOrWhiteSpace(item.Quantity)?"не указано":item.Quantity) + "\r";
      }
      _obj.InventoryString  = s;
    }

  }
}