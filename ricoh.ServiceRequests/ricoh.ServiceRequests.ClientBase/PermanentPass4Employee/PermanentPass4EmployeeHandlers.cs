using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Employee;

namespace ricoh.ServiceRequests
{
  partial class PermanentPass4EmployeeClientHandlers
  {

    public virtual void EmployeesInAttachedFileValueInput(Sungero.Presentation.BooleanValueInputEventArgs e)
    {
      _obj.State.Properties.EmployeeName.IsVisible =!_obj.EmployeesInAttachedFile??false;
      _obj.State.Properties.EmployeePosition.IsVisible = !_obj.EmployeesInAttachedFile??false;
      _obj.State.Properties.EmployeePhoto.IsVisible =!_obj.EmployeesInAttachedFile??false;      
    }

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      base.Refresh(e);
//      _obj.State.Properties.EmployeeName.IsVisible =!_obj.EmployeesInAttachedFile??false;
//      _obj.State.Properties.EmployeePosition.IsVisible = !_obj.EmployeesInAttachedFile??false;
//      _obj.State.Properties.EmployeePhoto.IsVisible =!_obj.EmployeesInAttachedFile??false;
    }

  }
}