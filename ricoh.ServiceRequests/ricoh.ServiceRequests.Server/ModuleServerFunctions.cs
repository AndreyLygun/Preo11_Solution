using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sungero.Core;
using Sungero.CoreEntities;

namespace ricoh.ServiceRequests.Server
{
  public class ModuleFunctions
  {

    [Remote(IsPure = true)]
    public IPass4AssetsMoving GetPass4AssetMoving(long Id)
    {
      var pass = Pass4AssetsMovings.GetAll(p => p.Id == Id);
      return pass.FirstOrDefault();
    }    
    

    [Remote(IsPure = true)]
    public IQueryable<IPass4VisitorCar> GetPasses4VisitorCar(string CarNumber, DateTime? date)
    {
      var passes = Pass4VisitorCars.GetAll().Where(p => p.CarNumber.Contains(CarNumber));
      if (date != null)
        passes = passes.Where(p => p.ValidOn == date);
      return passes;
    }


    /// <summary>
    /// Обрезает текст text до длины length. Если текст обрезан, в конце добавляется многоточие.
    /// </summary>
    /// 
    [Public]
    public string TrimText(string text, int length)
    {
      if (text.Length <= length) return text;
      return text.Substring(0, Math.Min(length-1, text.Length)) + "\x2026";
    }

    /// <summary>
    /// Преобразует список {"Первый", "Второй", "Третий", ...} в строку типа "Первый, Второй, Третий и др." с учётом длины списка и допустимой длиный строки
    /// </summary>
    /// list - список строк, из которых составляем строку
    /// max - количество элементов, которые должны перейти в строку. Если элементов в списке больше, то в конце строки добавляется "и др."
    /// maxLen - максимальная длина строки на выходе. Если при добавлении очередного элемента из списка длина строки превышает maxLen, то в конце строки добавляется "и др."
    [Public]
    public string List2SmartStr(List<string> list, int max, int maxLen)
    {
      string result = "";
      if (list.Count==0) return "";
      foreach(var item in list.Take(max+1)) {
        if (result.Length + item.Length > maxLen - 5) {
          result += "и др.";
          break;
        }
        result += item + ", ";
      }
      if (result.Substring(result.Length-2)==", ")
        result = result.Substring(0, result.Length-2);
      if (list.Count > max)
        result += " и др.";
      return result;
    }

    /// <summary>
    /// Создаёт и запускает задачу на согласование по процессу
    /// </summary>
    [Public(WebApiRequestType = RequestType.Post)]
    public void StartDocumentReviewTask(long requestId) {
      var request = BaseSRQs.Get(requestId);
      var task = Sungero.DocflowApproval.PublicFunctions.Module.Remote.CreateDocumentFlowTask(request);
      task.DisplayValue = "Согласование " + request.Name;
      task.ActiveText = "Согласуйте заявку от " + request.Renter.Name;
      task.Start();
    }
    
    /// <summary>
    /// Прекращает задачу по согласованию указанного документа
    /// </summary>
    [Public(WebApiRequestType = RequestType.Post)]
    public void AbortDocumentReviewTask(long requestId, string Reason) {
      var request = BaseSRQs.Get(requestId);
      if (request == null) return;
      var tasks = Sungero.DocflowApproval.PublicFunctions.Module.Remote.GetDocumentFlowTasks(request);
      foreach (var task in tasks) {
        task.AbortingReason = Reason;
        task.Abort();        
      }
    }
    
    
    /// <summary>
    /// Возвращает в сервисе интеграции информацию о статусе согласования заявки
    /// </summary>
    [Public(WebApiRequestType = RequestType.Post)]
    public string GetApprovalStatus(long requestId) {
      var request = BaseSRQs.Get(requestId);
      if (request == null) return "";
      //      var s = Sungero.Docflow.PublicFunctions.Module.GetDocumentSummary(request).ToString();
      //      var s = Sungero.Docflow.PublicFunctions.OfficialDocument.GetStateView(request).ToString();
      var s = Functions.BaseSRQ.GetApprovalStatus(request);
      return "{'status':"+s+"'}";
    }

  }
}