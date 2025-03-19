using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4VisitorCar;

namespace ricoh.ServiceRequests
{

  partial class Pass4VisitorCarServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      var subject = _obj.CarModel + "/" +_obj.CarNumber;;
      _obj.Subject = subject.Substring(0, Math.Min(subject.Length, 250));      
    }
  }

}