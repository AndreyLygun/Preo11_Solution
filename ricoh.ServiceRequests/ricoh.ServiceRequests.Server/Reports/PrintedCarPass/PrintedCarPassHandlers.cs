using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace ricoh.ServiceRequests
{
  partial class PrintedCarPassServerHandlers
  {

    public virtual IQueryable<ricoh.ServiceRequests.IPermanentPass4Car> GetPass()
    {
      return ricoh.ServiceRequests.PermanentPass4Cars.GetAll().Where(r => r.Equals(PrintedCarPass.Entity));;
    }

  }
}