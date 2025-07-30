using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Employee;

namespace ricoh.ServiceRequests.Client
{
  partial class PermanentPass4EmployeeFunctions
  {

    /// <summary>
    /// 
    /// </summary>       
    public void OpenPhoto()
    {
      if (!string.IsNullOrWhiteSpace(_obj.EmployeePhotoFileName)) {
        _obj.EmployeePhoto.Open(_obj.EmployeePhotoFileName);
      } else {
        Dialogs.NotifyMessage("Арендатор не прикрепил файл с фотографиями");
      }      
    }

  }
}