using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace ricoh.ServiceRequests
{
  partial class IssuedPassesClientHandlers
  {

    public override void BeforeExecute(Sungero.Reporting.Client.BeforeExecuteEventArgs e)
    {
      if (!IssuedPasses.BeginDate.HasValue && !IssuedPasses.EndDate.HasValue)
      {
        // Создание диалогового окна для запроса значений параметров
        // BeginDate, EndDate
        var dialog = Dialogs.CreateInputDialog("Период отчёта");
        var beginDate = dialog.AddDate("Дата начала", true, IssuedPasses.BeginDate);
        var endDate = dialog.AddDate("Дата завершения", true, IssuedPasses.EndDate);
        
        if (dialog.Show() == DialogButtons.Ok)
        {
          // Передача введенных значений в параметры BeginDate, EndDate
          // и DocumentRegister.
          IssuedPasses.BeginDate = beginDate.Value.Value;
          IssuedPasses.EndDate = endDate.Value.Value;
        }
        else
          e.Cancel = true;
      }
    }

  }
}