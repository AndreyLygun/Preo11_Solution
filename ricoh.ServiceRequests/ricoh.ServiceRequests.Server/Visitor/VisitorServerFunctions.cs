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
                                      var NewPass = _obj.Passes.AddNew();
                                      NewPass.CardId = PassNum;
                                      NewPass.CardIssuedAt = Calendar.Now;
                                      NewPass.CardIssuedBy = Users.Current;
                                      _obj.CardId = PassNum;
                                      _obj.CardIssuesAt = NewPass.CardIssuedAt;
                                      _obj.Save();
                                });

    }

  }
}