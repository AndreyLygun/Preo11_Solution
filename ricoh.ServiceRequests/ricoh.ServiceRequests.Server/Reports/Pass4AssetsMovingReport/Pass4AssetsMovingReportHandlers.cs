using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace ricoh.ServiceRequests
{
  partial class Pass4AssetsMovingReportServerHandlers
  {

    public virtual IQueryable<ricoh.ServiceRequests.IPass4AssetsMoving> GetRequest()
    {
      return ricoh.ServiceRequests.Pass4AssetsMovings.GetAll().Where(r => r.Equals(Pass4AssetsMovingReport.Entity));
    }

  }
}