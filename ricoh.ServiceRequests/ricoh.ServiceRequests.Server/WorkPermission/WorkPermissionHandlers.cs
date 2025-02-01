using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.WorkPermission;

namespace ricoh.ServiceRequests
{
  partial class WorkPermissionServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      if (String.IsNullOrEmpty(_obj.Work)) {
        _obj.Subject = _obj.DocumentKind.Name;
      } else {
        if (_obj.Work?.Length>50) {
          _obj.Subject = _obj.Work.Substring(0, 50) + "... ";
        } else {
          _obj.Subject = _obj.Work;
        }
        if (_obj.Site?.Length>50) {
          _obj.Subject = _obj.Subject + " (" + _obj.Site.Substring(0, 50) + "...)";
        } else if (!String.IsNullOrEmpty(_obj.Site)) {
          _obj.Subject = _obj.Subject + " (" + _obj.Site + ")";
        }       
      }
    }
  }

  partial class WorkPermissionSitesFloorPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> SitesFloorFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      query = query.Where(s => s.Type == ServiceRequests.Site.Type.PassSite);
      return query;
    }
  }

}