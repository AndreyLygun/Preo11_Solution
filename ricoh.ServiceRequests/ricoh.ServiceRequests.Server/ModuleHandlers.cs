using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace ricoh.ServiceRequests.Server
{
  partial class CarsFolderHandlers
  {

    public virtual IQueryable<ricoh.ServiceRequests.ISecuritySRQ> CarsDataQuery(IQueryable<ricoh.ServiceRequests.ISecuritySRQ> query)
    {
      query = query.Where(srq => !string.IsNullOrWhiteSpace(srq.CarNumber));
      return query;
    }
  }


  partial class ServiceRequestsHandlers
  {
  }
}