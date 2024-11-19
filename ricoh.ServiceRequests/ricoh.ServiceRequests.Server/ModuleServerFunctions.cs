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

    /// <summary>
    /// 
    /// </summary>
    public string TrimStringTo(string text, int length)
    {
      return text.Substring(0, Math.Min(length, text.Length));
    }

    /// <summary>
    /// Преобразует список {"Первый", "Втроей", "Третий", ...} в строку типа "Первый, Второй, Третий и др." с учётом длины списка и допустимой длиный строки
    /// </summary>
    /// list - список строк, из которых составляем строку
    /// max - количество элементов, которые должны перейти в строку. Если элементов в списке больше, то в конце строки добавляется "и др."
    /// maxLen - максимальная длина строки на выходе. Если добавлении очередного элемента из списка длина строки превышает maxLen, то в конце строки добавляется "и дрю"
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
    /// 
    /// </summary>
    [Public(WebApiRequestType = RequestType.Post)]
    public void StartDocumentReviewTask(long requestId) {
      var request = BaseSRQs.Get(requestId);
      var task = Sungero.DocflowApproval.PublicFunctions.Module.Remote.CreateDocumentFlowTask(request);
      task.Start();
    }
    
    /// 
    /// </summary>
    [Public(WebApiRequestType = RequestType.Post)]
    public string GetApprovalStatus(long requestId) {
      var request = BaseSRQs.Get(requestId);
      if (request == null) return null;
      var s = Functions.BaseSRQ.GetStateViewXml(request).ToString();
      return "{'status':"+s+"'}";
    }

  }
}