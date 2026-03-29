using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.ChangePermanentParking;

namespace ricoh.ServiceRequests.Server
{
  partial class ChangePermanentParkingFunctions
  {

    /// <summary>
    /// Записывает в заявку информацию о закрытии (в т.ч. количество оформленных пропусков), меняет статус на Closed.
    /// Срабатывает независимо от наличия у пользователя прав на заявку
    /// </summary>
    
    [Remote, Public]
    public void CloseRequest(string comment, int NumOfNewPrintedPass, int NumOfReissuedPrintedPasses, int NumOfNewNFC, int NumReissuedNFC)
    {
      comment = string.Format("Исполнено {0} {1}, {2} {3}", 
                                  Calendar.Now.ToShortDateString(),
                                  Calendar.Now.ToShortTimeString(), 
                                  Users.Current.DisplayValue,
                                  string.IsNullOrWhiteSpace(comment)?"":$" ({comment})");
      Sungero.Core.AccessRights.AllowRead(() => {
                                            _obj.ClosingInfo = comment;
                                            _obj.NumOfNewPrintedPass = NumOfNewPrintedPass;
                                            _obj.NumOfReissuedPrintedPasses = NumOfReissuedPrintedPasses;
                                            _obj.NumOfNewNFC = NumOfNewNFC;
                                            _obj.NumReissuedNFC = NumReissuedNFC;
                                            _obj.RequestState = ServiceRequests.BaseSRQ.RequestState.Done;                                            
                                            _obj.Save();
                                          });
    }    

  }
}