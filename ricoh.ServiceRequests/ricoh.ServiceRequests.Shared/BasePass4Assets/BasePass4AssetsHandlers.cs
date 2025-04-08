using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.BasePass4Assets;

namespace ricoh.ServiceRequests
{
  partial class BasePass4AssetsSharedHandlers
  {

    public virtual void AssetsChanged(Sungero.Domain.Shared.CollectionPropertyChangedEventArgs e)
    {
      var s = "";
      foreach(var item in _obj.Assets) {
        if (String.IsNullOrWhiteSpace(item.Name)) continue;
        s = s + item.Name + " \t("
          + "Размер: " + (String.IsNullOrWhiteSpace(item.Size)?"не указан":item.Size) + ") "
          + "\tКол-во: " + (String.IsNullOrWhiteSpace(item.Quantity)?"не указано":item.Quantity)
          + (String.IsNullOrWhiteSpace(item.Note)?(", " + item.Note):"") + "\r";
      }
      _obj.AssetsString  = s;
    }

  }
}