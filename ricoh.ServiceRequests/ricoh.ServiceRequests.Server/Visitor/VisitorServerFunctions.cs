using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.Visitor;

namespace ricoh.ServiceRequests.Server
{
  partial class VisitorFunctions
  {

    /// <summary>
    /// Сделать отметку о выдаче пропуска
    /// </summary>
    [Public, Remote]
    public void IssuePass(string PassNum)
    {
      Sungero.Core.AccessRights.AllowRead(()=> {
                                            _obj.CardId = PassNum;
                                            _obj.CardIssuesAt = Calendar.Now;
                                            _obj.Save();
                                          });
    }

  }
}