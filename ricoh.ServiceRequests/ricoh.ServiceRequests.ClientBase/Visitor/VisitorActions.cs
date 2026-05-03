using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Visitor;

namespace ricoh.ServiceRequests.Client
{
  internal static class VisitorStaticActions
  {
    public static void Refresh(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      
    }

    public static bool CanRefresh(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

  }





  partial class VisitorActions
  {

    /// <summary>
    ///  Выдать пропуск посетителю
    /// </summary>
    /// <param name="e"></param>
    public virtual void CheckIn(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var dlg = Dialogs.CreateInputDialog("Выдача пропуска");
      var name = dlg.AddString("Имя:", false);
      name.IsEnabled = false;
      name.Value = _obj.Name;
      var renter = dlg.AddString("Принимающая сторона:", false);
      renter.IsEnabled = false;
      renter.Value = _obj.Renter.Name;    
      var PassNum = dlg.AddString("Номер пропуска", true).WithLabel("(приложите пропуск к считывателю)");
      var SendNotification = dlg.AddBoolean("Уведомить автора заявки о выдаче пропуска");
      SendNotification.Value = _obj.SendNotification??false;
      
      dlg.Show();
      if (dlg.IsCanceled) return;
      // Исправляем фигню при получении данных из считывателя Elatec
      PassNum.Value = PassNum.Value
        .Replace('Ф', 'A')
        .Replace('И', 'B')
        .Replace('С', 'C')
        .Replace('В', 'D')
        .Replace('У', 'E')
        .Replace('А', 'F');
      Functions.Visitor.Remote.IssuePass(_obj, PassNum.Value);
      if (SendNotification.Value??false) {
        Functions.Module.Remote.EnqueueSendMailAsync(_obj.Request.CreatorMail, "Ваш посетитель получил пропуск", "Ваш посетитель получил пропуск");        
      }

    }

    public virtual bool CanCheckIn(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _obj.ValidOn == Calendar.Today;
    }

  }

}