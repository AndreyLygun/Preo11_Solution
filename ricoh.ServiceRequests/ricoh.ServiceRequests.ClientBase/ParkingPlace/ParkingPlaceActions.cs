using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.ParkingPlace;

namespace ricoh.ServiceRequests.Client
{

  partial class ParkingPlaceCollectionActions
  {

    public virtual bool CanFixPlace2Renter(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

    public virtual void FixPlace2Renter(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var dialog = Dialogs.CreateInputDialog("Изменение арендатора");
      dialog.Buttons.AddOkCancel();
      var renter=dialog.AddSelect("Выберите арендатора", true, Renters.Null);
      var res = dialog.Show();
      if (res == DialogButtons.Cancel) return;
      foreach(var item in _objs) {
        var place = ParkingPlaces.As(item);
        if (place != null) {
          place.Renter = renter.Value;
          place.Cars.Clear();
          place.Drivers.Clear();
          place.NfcNumber = "";
          place.Save();
        }
      }
    }

    public virtual bool CanFreePlace(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

    public virtual void FreePlace(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      foreach(var place in _objs) {
        if (place != null) {
          place.Renter = Renters.Null;
          place.Cars.Clear();
          place.Drivers.Clear();
          place.NfcNumber = "";
          place.Save();
        }
      }
    }
  }

}