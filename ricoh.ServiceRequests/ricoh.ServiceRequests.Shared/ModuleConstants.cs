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

    // Машины
    [Public]
    public static readonly Guid Pass4VisitorCarGuid = Guid.Parse("C9700A7B-01B1-482A-AD43-12E76577C828");    
    [Public]
    public static readonly Guid PermanentPass4Car = Guid.Parse("1065A4C9-0C4F-485C-BEAB-1F6A793D6E98");       
    [Public]
    public static readonly Guid StopPermanentPass4Car = Guid.Parse("8F3F8020-4C47-4B94-98B3-ABD900977A8B");       

    // ТМЦ
    [Public]
    public static readonly Guid Pass4AssetsMovingGuid = Guid.Parse("3FED4EE8-1BA7-4AF7-925D-65CD116D99C4");  
    [Public]
    public static readonly Guid Pass4PermanentAssetsMoving = Guid.Parse("4ADC05D4-A2E6-48E2-B939-35A43EF3A21D");  
    

    
  }
}