using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.BaseSRQ;

namespace ricoh.ServiceRequests
{
  partial class BaseSRQFilteringServerHandler<T>
  {

    public override IQueryable<T> Filtering(IQueryable<T> query, Sungero.Domain.FilteringEventArgs e)
    {
      if (_filter == null)
        return query;
      query = base.Filtering(query, e);
      if (_filter.Renter!=null) {
        query = query.Where(r => r.Renter.Equals(_filter.Renter));
      }
      return query;
    }
  }


  partial class BaseSRQServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      if (_obj.Renter != null) {
        _obj.AccessRights.Grant(_obj.Renter, DefaultAccessRightsTypes.FullAccess);
      }
      base.BeforeSave(e);      
    }

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      _obj.Subject = "№ " + _obj.Id + " от " + _obj.Created;
      _obj.Name = "-";
      if (Renters.Is(_obj.Author)) {
        _obj.Renter = Renters.As(_obj.Author);
      }
      _obj.Creator = Users.Current.Name;
      _obj.RequestState = BaseSRQ.RequestState.Draft;
    }
  }

}