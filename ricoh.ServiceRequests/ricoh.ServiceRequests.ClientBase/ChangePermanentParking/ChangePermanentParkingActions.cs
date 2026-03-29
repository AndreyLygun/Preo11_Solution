using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.ChangePermanentParking;

namespace ricoh.ServiceRequests.Client
{
  partial class ChangePermanentParkingActions
  {
    public override void CloseRequest(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      //base.CloseRequest(e);
      var dlg = Dialogs.CreateInputDialog("Изготовлено пропусков");
      string[] NfcOptions = {"Нет", "Впервые", "Перевыпуск"};
      int NfcIssuedIndex = 
        _obj.NeedNFC == ChangePermanentParking.NeedNFC.NewPass?1:
        (_obj.NeedNFC == ChangePermanentParking.NeedNFC.ReissuePass?2:0);
      var NfcInput = dlg.AddSelect("Электронная карта", true, NfcIssuedIndex).From(NfcOptions);
      int newPrintedPasses = _obj.Cars.Count(car => car.NeedPrintedPass == ChangePermanentParkingCars.NeedPrintedPass.NewPass);      
      var newPrintedPassInput = dlg.AddInteger("Новых ламинатов", true, newPrintedPasses);
      int newReissuedPasses = _obj.Cars.Count(car => car.NeedPrintedPass == ChangePermanentParkingCars.NeedPrintedPass.ReissuePass);
      var reissuedPrinterPassInput = dlg.AddInteger("Перевыпущенных ламинатов", true, newReissuedPasses);      
      var commentInput = dlg.AddString("Комментарий", false);
      if (dlg.Show() != DialogButtons.Ok) return;
      Functions.ChangePermanentParking.Remote.CloseRequest(_obj, commentInput.Value, newPrintedPassInput.Value??0, reissuedPrinterPassInput.Value??0, NfcInput.ValueIndex==1?1:0, NfcInput.ValueIndex==2?1:0);
      _obj.State.Properties.ClosingInfo.IsVisible = true;            
    }

    public override bool CanCloseRequest(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return base.CanCloseRequest(e);
    }

  }

}