using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using System.Text;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Pass4AssetsMoving;
using ricoh.ServiceRequests.Shared;

namespace ricoh.ServiceRequests
{
  partial class Pass4AssetsMovingFloorPropertyFilteringServerHandler<T>
  {

  }

  partial class Pass4AssetsMovingLoadingSitePropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> LoadingSiteFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      query = query.Where(s => s.Type.Equals(Site.Type.LoadingSite));
      return query;
    }
  }


  partial class Pass4AssetsMovingServerHandlers
  {

    public override void AfterSave(Sungero.Domain.AfterSaveEventArgs e)
    {
      base.AfterSave(e);

    }

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      // Собираем subject документа из названий ТМЦ
      base.BeforeSave(e);
      var assets = new List<string> {};
      foreach(var asset in  _obj.Inventory.Take(4)) {
        assets.Add(asset.Name);
      }
      _obj.Subject = _obj.Info.Properties.MovingDirection.GetLocalizedValue(_obj.MovingDirection)
        + " (" + Functions.Module.List2SmartStr(assets, 3, 250)+")";
      
      var report = Reports.GetPass4AssetsMovingReport();
      report.Entity = _obj;
      if (_obj.HasVersions) 
        report.ExportTo(_obj.LastVersion);
      else
        report.ExportTo(_obj);

      
      // Добавляем вложение с текстом заявки
//      var inventory = "";
//      foreach (var inv in _obj.Inventory) {
//        inventory += "\n\r" + inv.Name + " (Размер: " + inv.Size + ", кол-во: " + inv.Quantity + ")";
//      }
//      var descrption =  "Арендатор: " + _obj.Renter.Name +
//        "\n\rАвтор заявки: " + _obj.Creator +
//        "\n\rОтветственный сотрудник: " + _obj.ResponsibleName + " / " + _obj.ResponsiblePhone + ", " + _obj.ResponsibleMail +
//        "\n\rЗАЯВКА ----------------------------" +
//        "\n\rНаправление перемещения: " + _obj.Info.Properties.MovingDirection.GetLocalizedValue(_obj.MovingDirection) +
//        "\n\rДата перемещения: " + _obj.ValidOn.ToString() +
//        "\n\rМесто разгрузки: " + _obj.LoadingSite.Name +
//        "\n\rВремя ввоза-вывоза: " + _obj.TimeSpan.Name +
//        "\n\rЭтаж: " + _obj.Floor +
//        "\n\rГрузовой лифт: " + (_obj.Elevator.Value?"Да":"Нет") +
//        "\n\rКомната временного хранения: " + (_obj.StorageRoom.Value?"Да":"Нет") +
//        "\n\rТМЦ --------------------------------" +
//        inventory +
//        "\n\rПЕРЕВОЗЧИК -------------------------" +
//        "\n\rАвтомобиль: " + _obj.CarModel + ", г/н " + _obj.CarNumber +
//        "\n\rГрузчики:" +
//        "\n\r" + _obj.Visitors;
//      var ext = "txt";
//      Functions.BaseSRQ.UpdateVersion(_obj, descrption, "txt");

    }

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      _obj.Elevator = false;
      _obj.StorageRoom = false;
    }
  }



}