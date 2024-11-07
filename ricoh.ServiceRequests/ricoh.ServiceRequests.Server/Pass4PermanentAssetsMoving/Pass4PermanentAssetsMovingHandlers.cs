using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4PermanentAssetsMoving;

namespace ricoh.ServiceRequests
{
  partial class Pass4PermanentAssetsMovingServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      if (_obj.Assets!=null && _obj.Assets.Length>100) {
        _obj.Subject = _obj.Assets.Substring(0, 100) + "...";
      } else {
        _obj.Subject = _obj.Assets;
      }
    }
  }

}