using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain.Initialization;

namespace ricoh.ServiceRequests.Server
{
  public partial class ModuleInitializer
  {

    public override bool IsModuleVisible()
    {
      var SecurityRequestsRole = Sungero.CoreEntities.Roles.GetAll(r => r.Name.Equals(Constants.Module.SecurityRequestsRoleName)).FirstOrDefault();
      return SecurityRequestsRole!= null && Users.Current.IncludedIn(SecurityRequestsRole);
    }


    public override void Initializing(Sungero.Domain.ModuleInitializingEventArgs e)
    {

      RequestApprovalTasks.AccessRights.Grant(Roles.AllUsers, DefaultAccessRightsTypes.Create);
      RequestApprovalTasks.AccessRights.Save();
      
      BaseSRQs.AccessRights.Grant(Roles.AllUsers, DefaultAccessRightsTypes.Create);
      BaseSRQs.AccessRights.Save();
      var SequrityRole = CreateRole(Constants.Module.SecurityRequestsRoleName);
      // Люди
      CreateDocumentType("Заявка на пропуск для посетителей", Pass4Visitor.ClassTypeGuid, Constants.Module.Pass4VisitorGuid);
      CreateDocumentType("Заявка на пропуск для сотрудников", PermanentPass4Employee.ClassTypeGuid, Constants.Module.PermanentPass4Employee);
      CreateDocumentType("Дополнительный доступ для сотрудника", Permission4Employee.ClassTypeGuid, Constants.Module.Permission4Employee);      
      CreateDocumentType("Блокировка пропуска для сотрудников", StopPermanentPass4Employee.ClassTypeGuid, Constants.Module.StopPermanentPass4Employee);
      CreateDocumentType("Допуск на выполнение работ", WorkPermission.ClassTypeGuid, Constants.Module.WokrPermission);
      // Машины
      CreateDocumentType("Заявка на гостевую парковку", Pass4VisitorCar.ClassTypeGuid, Constants.Module.Pass4VisitorCarGuid);    
      CreateDocumentType("Изменение постоянной парковки", ChangePermanentParking.ClassTypeGuid, Constants.Module.ChangePermanentParking);
      // ТМЦ
      CreateDocumentType("Разовый ввоз-вывоз ТМЦ", Pass4AssetsMoving.ClassTypeGuid, Constants.Module.Pass4AssetsMovingGuid);
      CreateDocumentType("Внутреннее перемещение ТМЦ", Pass4AssetsInternalMoving.ClassTypeGuid, Constants.Module.Pass4AssetsInternalMovingGuid);
      CreateDocumentType("Постоянный пропуск на ввоз-вывоз ТМЦ",  Pass4AssetsPermanentMoving.ClassTypeGuid, Constants.Module.Pass4AssetsPermanentMoving);

      
      Sites.AccessRights.Grant(Roles.AllUsers, DefaultAccessRightsTypes.Read);
      Sites.AccessRights.Save();
      Renters.AccessRights.Grant(Roles.AllUsers, DefaultAccessRightsTypes.Read);
      Renters.AccessRights.Save();
      ParkingPlaces.AccessRights.Grant(SequrityRole, DefaultAccessRightsTypes.FullAccess);
      ParkingPlaces.AccessRights.Grant(Roles.AllUsers, DefaultAccessRightsTypes.Read);
      ParkingPlaces.AccessRights.Save();
      
      
      TimeSpans.AccessRights.Grant(Roles.AllUsers, DefaultAccessRightsTypes.Read);
      TimeSpans.AccessRights.Save();
      
      Visitors.AccessRights.Grant(Roles.AllUsers, DefaultAccessRightsTypes.Create);
      Visitors.AccessRights.Grant(SequrityRole, DefaultAccessRightsTypes.FullAccess);
      Visitors.AccessRights.Save();
            
      InitTimeSpans();
    }

    
    public void InitTimeSpans() {
      string[] spans = { "Выходные дни", "Будни с 20:00-8:00", "10:00-12:00", "14:00-17:00"};
      foreach(var span in spans) {
        if (TimeSpans.GetAll(ts => ts.Name.Equals(span)).FirstOrDefault() == null) {
          var ts=TimeSpans.Create();
          ts.Name = span;
          ts.Save();          
        }
      }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public IRole CreateRole(string RoleName)
    {
      var Role = Roles.GetAll(r => r.Name.Equals(RoleName)).FirstOrDefault();
      if (Role == null) {
        Role = Roles.Create();
        Role.Name = RoleName;
        Role.Save();
      }
      return Role;
    }
    
    public void CreateDocumentType(string Name, Guid ClassTypeGuid, Guid ClassKindGuid) {
      InitializationLogger.Debug(Name + " " + ClassKindGuid.ToString());
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentType(Name, ClassTypeGuid, Sungero.Docflow.DocumentType.DocumentFlow.Incoming, false);
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentKind(Name, Name, Sungero.Docflow.DocumentKind.NumberingType.NotNumerable,
                                                                              Sungero.Docflow.DocumentType.DocumentFlow.Incoming, true, true,
                                                                              ClassTypeGuid, new Sungero.Domain.Shared.IActionInfo[] {}, ClassKindGuid, true);
    }
    
  }
}