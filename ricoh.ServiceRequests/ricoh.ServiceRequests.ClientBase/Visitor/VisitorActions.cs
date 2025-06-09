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
      if (!String.IsNullOrEmpty(_obj.CardId)) {
        e.AddWarning("Этот посетитель уже получил пропуск");
        return;
      }
      if (!Calendar.Today.Equals(_obj.ValidOn)) {
        e.AddWarning("Этот посетитель оформлен не на сегодня");
        return;
      }
      var dlg = Dialogs.CreateInputDialog("Выдача пропуска");
      var name = dlg.AddString("Имя:", false);
      name.IsEnabled = false;
      name.Value = _obj.Name;
      var renter = dlg.AddString("Принимающая сторона:", false);
      renter.IsEnabled = false;
      renter.Value = _obj.Renter.Name;
      
      var PassNum = dlg.AddString("Номер пропуска", true).WithLabel("(приложите пропуск к считывателю)");
      
      dlg.Show();
      if (dlg.IsCanceled) return;
      e.CloseFormAfterAction = true;
      // Исправляем фигню при получении данных из считывателя Elatec
      PassNum.Value = PassNum.Value
        .Replace('Ф', 'A')
        .Replace('И', 'B')
        .Replace('С', 'C')
        .Replace('В', 'D')
        .Replace('У', 'E')
        .Replace('А', 'F');
      Functions.Visitor.Remote.IssuePass(_obj, PassNum.Value);
    }

    public virtual bool CanCheckIn(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

  }

}