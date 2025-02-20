using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Car;

namespace ricoh.ServiceRequests
{
  partial class PermanentPass4CarClientHandlers
  {

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      base.Showing(e);
      _obj.State.Properties.ValidFrom.IsVisible = (_obj.Action == PermanentPass4Car.Action.NewPass);
    }

    public virtual void ActionValueInput(Sungero.Presentation.EnumerationValueInputEventArgs e)
    {
      var action = e.NewValue;
      _obj.State.Properties.ValidFrom.IsVisible = (_obj.Action == PermanentPass4Car.Action.NewPass);
    }
  }
}