using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.WorkPermission;

namespace ricoh.ServiceRequests
{
  partial class WorkPermissionSitesSitePropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> SitesSiteFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      query = query.Where(s => s.Type == ServiceRequests.Site.Type.PassSite);
      return query;
    }
  }

}