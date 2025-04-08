using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ricoh.ServiceRequests.PermanentPass4Car;

namespace ricoh.ServiceRequests.Server
{
  partial class PermanentPass4CarFunctions
  {

    /// <summary>
    /// 
    /// </summary>       
    /// 
    [Public, Remote]
    public static IChangePermanentParking CreateChangingRequest()
    {
      return ChangePermanentParkings.Create();      
    }

  }
}