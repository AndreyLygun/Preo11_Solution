using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Permission4Employee;

namespace ricoh.ServiceRequests
{
  partial class Permission4EmployeeAccessSitePropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> AccessSiteFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      query = query.Where(s => s.Type == Site.Type.PassSite);
      return query;
    }
  }

  partial class Permission4EmployeeServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      _obj.Subject = _obj.Permisssion + " / " + _obj.Employee;
    }
  }

}