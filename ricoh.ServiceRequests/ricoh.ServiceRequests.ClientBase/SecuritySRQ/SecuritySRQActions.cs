using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.SecuritySRQ;

namespace ricoh.ServiceRequests.Client
{
  partial class SecuritySRQActions
  {
    public override void CloseRequest(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var dlg = Dialogs.CreateInputDialog("Закрываем заявку",
                                          string.Format("Дата/Время: {0} / {1}", Calendar.UserNow.ToLongDateString(), Calendar.UserNow.ToShortTimeString()));
      var carModel = dlg.AddString("Модель автомобиля", false, _obj.CarModel);
      var carNumber = dlg.AddString("Госномер автомобиля", false, _obj.CarNumber);
      dlg.Buttons.AddOkCancel();
      if (dlg.Show() != DialogButtons.Ok) return;
      Functions.SecuritySRQ.Remote.CloseRequestWithCarInfo(_obj, carModel.Value, carNumber.Value);

    }

    public override bool CanCloseRequest(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return base.CanCloseRequest(e);
    }

  }

}