using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace ricoh.ServiceRequests
{
  partial class ReportServerHandlers
  {

    public virtual IQueryable<ricoh.ServiceRequests.IPass4AssetsMoving> GetPass4AssetsMoving()
    {
      return ricoh.ServiceRequests.Pass4AssetsMovings.GetAll();
    }

  }
}