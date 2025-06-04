using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.BaseSRQ;

namespace ricoh.ServiceRequests
{
  partial class BaseSRQClientHandlers
  {

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      base.Showing(e);
      if (Users.Current.IncludedIn(Roles.Administrators)) return;
      if (!_obj.RequestState.Equals(BaseSRQ.RequestState.Draft)) {
        _obj.State.IsEnabled = false;
      }      
      if (!Users.Current.Equals(_obj.Renter) && !Users.Current.Equals(_obj.Author)) {
        _obj.State.IsEnabled = false;
      }
      _obj.State.Properties.ClosingInfo.IsVisible = !string.IsNullOrWhiteSpace(_obj.ClosingInfo);
    }
  }
}