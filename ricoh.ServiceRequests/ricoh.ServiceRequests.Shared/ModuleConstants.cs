using System;
using Sungero.Core;

namespace ricoh.ServiceRequests.Constants
{
  public static class Module
  {
    public const string SecurityRequestsRoleName = "Сервисные заявки: доступ к заявкам в СБ";
    
    // Люди
    [Public]
    public static readonly Guid Pass4VisitorGuid = Guid.Parse("F4FAA406-B788-4389-95A7-33981750EC3A");
    [Public]
    public static readonly Guid PermanentPass4Employee = Guid.Parse("57A49412-0CA7-4C63-8A39-9C0A9CC2BA26");
    [Public]
    public static readonly Guid Permission4Employee = Guid.Parse("AAC62686-D5B1-45CD-8984-0D5CEBCC4E41");       
    [Public]
    public static readonly Guid StopPermanentPass4Employee = Guid.Parse("9D51F4F4-A83F-4809-AC10-92233FBD64B0");       
    [Public]
    public static readonly Guid WokrPermission = Guid.Parse("3154D122-8ED4-43A3-93EE-FEA682BBABF0");     

    // Машины
    [Public]
    public static readonly Guid Pass4VisitorCarGuid = Guid.Parse("C9700A7B-01B1-482A-AD43-12E76577C828");    
    [Public]
    public static readonly Guid PermanentPass4Car = Guid.Parse("1065A4C9-0C4F-485C-BEAB-1F6A793D6E98");       
    [Public]
    public static readonly Guid StopPermanentPass4Car = Guid.Parse("8F3F8020-4C47-4B94-98B3-ABD900977A8B");    
    [Public]
    public static readonly Guid ChangePermanentParking = Guid.Parse("6A8A2C77-1839-4A23-8B11-2348B5E0693E");

    // ТМЦ
    [Public]
    public static readonly Guid Pass4AssetsMovingGuid = Guid.Parse("3FED4EE8-1BA7-4AF7-925D-65CD116D99C4");  
    [Public]
    public static readonly Guid Pass4AssetsInternalMovingGuid = Guid.Parse("4C5F5034-8C84-4A1D-9F4A-6B05AC79CD3C");     
    [Public]
    public static readonly Guid Pass4PermanentAssetsMoving = Guid.Parse("4ADC05D4-A2E6-48E2-B939-35A43EF3A21D");  

    [Public]
    public static readonly Guid Pass4AssetsPermanentMoving = Guid.Parse("5B971A91-4605-46EE-A89E-66E98F9566FE");      
    
    

    
  }
}