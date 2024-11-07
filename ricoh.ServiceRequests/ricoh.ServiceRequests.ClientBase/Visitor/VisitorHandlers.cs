using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Visitor;

namespace ricoh.ServiceRequests
{
  partial class VisitorClientHandlers
  {

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      if (_obj.Request != null  | _obj.CardId != null) {
        // если создан при одобрении заявки на пропуск или карта посетителя уже выдана, то отключаем возможность изменять данные
        _obj.State.Properties.Name.IsEnabled = false;
        _obj.State.Properties.Renter.IsEnabled = false;
        _obj.State.Properties.ValidOn.IsEnabled = false;
      }
        
    }

  }
}