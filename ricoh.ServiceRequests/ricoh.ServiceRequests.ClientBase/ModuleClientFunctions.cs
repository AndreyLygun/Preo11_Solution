using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace ricoh.ServiceRequests.Client
{
  public class ModuleFunctions
  {

    /// <summary>
    /// 
    /// </summary>
    /// 
    [LocalizeFunction("ShowPass4VisitorCarByCarNumber_FunctionName", "")]
    public static void ShowPass4VisitorCarByCarNumber()
    {
      var dlg = Dialogs.CreateInputDialog("Поиск заявки по номеру авто");
      var carNum = dlg.AddString("Введите цифры из номер авто", true);
      var date = dlg.AddDate("Дата въезда", false, Calendar.Today);
      if (dlg.Show() == DialogButtons.Ok) {
        var cars = Functions.Module.Remote.GetPasses4VisitorCar(carNum.Value, date.Value);
        if (cars.Count() == 0) {
          var msg = Dialogs.CreateConfirmDialog("Не найдено заявок с таким номером автомобиля");
          msg.Show();
          return;
        }
        if (cars.Count() == 1)
          cars.First().Show() ;
        else 
          cars.Show();
      }
    }
    
    [LocalizeFunction("ShowPassAssetMovingById_FunctionName", "")]
    public static void ShowPass4AssetMovingById()
    {
      var dlg = Dialogs.CreateInputDialog("Поиск заявки ввоз-вывоз ТМЦ по номер");
      var Id = dlg.AddInteger("Введите номер заявки", true);
      if (dlg.Show() == DialogButtons.Ok) {
        var pass = Functions.Module.Remote.GetPass4AssetMoving(Id.Value??0);
        if (pass == null) {
          var msg = Dialogs.CreateConfirmDialog("Не найдено заявок с таким номером");
          msg.Show();
          return;
        } else pass.Show();
      }
    }    

  }
}