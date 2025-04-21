using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Workflow;
using ricoh.Preo8.DocumentReviewTask;
using ricoh.ServiceRequests;
using ThisBlock = ricoh.Preo8.Server.DocumentReviewTaskBlocks.ChangeRequestStatusBlockricohHandlers;

namespace ricoh.Preo8.Server.DocumentReviewTaskBlocks
{
  partial class EmailNotificationricohHandlers
  {

    public virtual void EmailNotificationricohExecute()
    {
      var mail = Mail.CreateMailMessage();
      if (String.IsNullOrWhiteSpace(_block.Toricoh)) 
        return;
      mail.To.Add(_block.Toricoh);
      if (!String.IsNullOrWhiteSpace(_block.CCricoh)) 
        mail.CC.Add(_block.CCricoh);
      mail.Subject = _block.Subjectricoh;
      mail.Body = _block.Textricoh;
      Mail.Send(mail);
    }
  }

  partial class ChangeRequestStatusBlockricohHandlers
  {
    public virtual void ChangeRequestStatusBlockricohExecute()
    {
      var doc = _obj.DocumentForReviewGroup.OfficialDocuments.FirstOrDefault();
      if (doc == null) return;
      var request = BaseSRQs.As(doc);
      request.RequestStateDescription += Calendar.Now+ ": "+ _block.Descriptionricoh + "\n";
      if (_block.NewStatericoh == ThisBlock.NewStatericoh.Draftricoh) {
        request.  RequestState = ServiceRequests.BaseSRQ.RequestState.Draft;
      } else if (_block.NewStatericoh == ThisBlock.NewStatericoh.Approvedricoh) {
        request.RequestState = ServiceRequests.BaseSRQ.RequestState.Approved;
      } else if (_block.NewStatericoh == ThisBlock.NewStatericoh.Denied) {
        request.RequestState = ServiceRequests.BaseSRQ.RequestState.Denied;
      } else if (_block.NewStatericoh == ThisBlock.NewStatericoh.OnApproving) {
        request.RequestState = ServiceRequests.BaseSRQ.RequestState.OnReview;
      }
    }
  }

  partial class ProcessVisitorsricohHandlers
  {
    public virtual void ProcessVisitorsricohExecute()
    {
      #region Получаем запрос правильного вида и список посетителей из запроса
      // Весь этот код ниже нужен, чтобы вытащить список посетителей или из заявки типа Pass4Visitors, или из заявки типа Pass4VisitorCar
      // Наверное, стоило бы объединить эти два типа, или наследовать один от другого, но переделывать слишком много.
      var doc = _obj.DocumentForReviewGroup.OfficialDocuments.FirstOrDefault();
      if (doc == null) return;
      
      string visitorsList="";
      ISecuritySRQ request = null;
      var pass4Visitors = ServiceRequests.Pass4Visitors.As(doc);
      if (pass4Visitors != null) {
        request = SecuritySRQs.As(pass4Visitors);
        visitorsList = pass4Visitors.Visitors;
      }
      var pass4VisitorCars = Pass4VisitorCars.As(doc);
      if (pass4VisitorCars !=null) {
        request = ServiceRequests.SecuritySRQs.As(pass4VisitorCars);
        visitorsList = pass4VisitorCars.Visitors;
      }
      var names = visitorsList.Split('\n');
      if (names.Count() == 0) return;
      #endregion
      
      #region В любом случае вначале удаляем из этого запроса посетителей...
      foreach (var visitor in ServiceRequests.Visitors.GetAll(v => v.Request.Equals(request))) {
        if (string.IsNullOrEmpty(visitor.CardId))
          // ... которые ещё не получили пропуск.
          Visitors.Delete(visitor);
      }
      #endregion
      
      #region Если нужно, добавляем посетителей
      if (_block.Actionricoh.Equals(DocumentReviewTaskBlocks.ProcessVisitorsricohHandlers.Actionricoh.Add)) {
        foreach (var name in names) {
          var visitor = Visitors.Create();
          visitor.Name = name;
          visitor.Request = request;
          visitor.ValidOn = request.ValidOn;
          visitor.Renter = request.Renter;
          visitor.AccessRights.Grant(request.Renter, DefaultAccessRightsTypes.Read);
          visitor.AccessRights.Grant(request.Author, DefaultAccessRightsTypes.Read);
          visitor.Save();
        }
      }
      #endregion
      
    }
  }


  partial class PrepareDraftResolutionBlockHandlers
  {

  }
  partial class ProcessResolutionBlockHandlers
  {

  }
  partial class ReviewReworkBlockHandlers
  {

  }
  partial class SendReviewToAddresseeBlockHandlers
  {

  }
  partial class DeleteObsoleteDraftResolutionsBlockHandlers
  {

  }
  partial class WaitForAddresseesReviewBlockHandlers
  {

  }
  partial class SetNewAddresseeBlockHandlers
  {

  }
  partial class SendReviewTasksToAddresseesBlockHandlers
  {

  }
}