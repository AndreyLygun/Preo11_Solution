using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4VisitorCar;

namespace ricoh.ServiceRequests.Client
{
  partial class Pass4VisitorCarFunctions
  {

    /// <summary>
    /// 
    /// </summary>
    /// 
    [LocalizeFunction("Гостевой автопропск по номеру авто", "Запрашивает номер авто и ищет заявку на это авто")]
    public void FindByCarNumber()
    {
      var dlg = Dialogs.CreateInputDialog("Поиск заявки по номеру авто");
      var carNum = dlg.AddString("Введите цифры из номер авто", true);
      var date = dlg.AddDate("Дата въезда", true, Calendar.Today);
      if (dlg.Show() == DialogButtons.Ok) {
        var cars = Pass4VisitorCars.GetAll( r => (r.CarNumber.Contains(carNum.Value)) && (r.ValidOn == date.Value));
        if (cars.Count() == 0) {
          Dialogs.CreateConfirmDialog("Не найдено заявок с таким номером автомобиля");
          return;
        }
        if (cars.Count() == 1)
          cars.First().Show();
        else 
          cars.Show();
      }
    }

  }
}