using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.ParkingPlace;

namespace ricoh.ServiceRequests
{
  partial class ParkingPlaceDriversSharedCollectionHandlers
  {

    public virtual void DriversAdded(Sungero.Domain.Shared.CollectionPropertyAddedEventArgs e)
    {
      _added.Changed = "Добавлено " + Calendar.Today.ToShortDateString() + ", " + Users.Current.Name;
    }
  }

  partial class ParkingPlaceCarsSharedCollectionHandlers
  {

    public virtual void CarsAdded(Sungero.Domain.Shared.CollectionPropertyAddedEventArgs e)
    {
      _added.Changed = "Добавлено " + Calendar.Today.ToShortDateString() + ", " + Users.Current.Name;
    }
  }

  partial class ParkingPlaceSharedHandlers
  {

  }
}