using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Site;

namespace ricoh.ServiceRequests.Client
{



  partial class SiteCollectionActions
  {

    public virtual bool CanFixSite2Renter(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

    public virtual void FixSite2Renter(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var dialog = Dialogs.CreateInputDialog("Введите адрес");
      dialog.Buttons.AddOkCancel();
      var renter=dialog.AddSelect("Выберите арендатора", true, Renters.Null);
      var res = dialog.Show();
      if (res == DialogButtons.Cancel) return;
      foreach(var item in _objs) {
        var site = Sites.As(item);
        if (site != null) {
          site.Renter = renter.Value;
          site.Save();
        }        
      }
    }

    public virtual bool CanFreeSite(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

    public virtual void FreeSite(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      foreach(var site in _objs) {
        if (site != null) {
          site.Renter = Renters.Null;
          site.Save();
        }        
      }      
    }
  }

}