using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Employee;

namespace ricoh.ServiceRequests.Client
{
  partial class PermanentPass4EmployeeActions
  {
    public virtual void DownloadPhotos(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      if (!string.IsNullOrWhiteSpace(_obj.EmployeePhotoFileName)) {
        _obj.EmployeePhoto.Open(_obj.EmployeePhotoFileName);
      } else {
        Dialogs.NotifyMessage("Арендатор не прикрепил файл с фотографиями");
      }
    }

    public virtual bool CanDownloadPhotos(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return !string.IsNullOrWhiteSpace(_obj.EmployeePhotoFileName);
    }

  }

}