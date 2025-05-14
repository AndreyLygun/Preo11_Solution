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
      _obj.Subject = string.Format("Дополнительный доступ ({0})", _obj.Employee);
      _obj.Name = string.Format("Заявка № {0} от {1}: {2}", _obj.Id, _obj.Renter, _obj.Subject);
      string[] sites = new string[_obj.Access.Count];
      foreach(var item in _obj.Access) {
        sites.Append(item.Site.Name);
      }
      _obj.AccessStr = string.Join(",", sites);            
    }
  }

}