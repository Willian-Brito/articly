using System.Runtime.Serialization;

namespace Articly.Presentation.Api.Response;

[DataContract]
public class BaseResponseAPI
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "payload")]
    public object Payload { get; set; }

    private BaseResponseAPI() { }

    public static BaseResponseAPI Create(object obj, bool success = true)
    {
        var response = new BaseResponseAPI
        { 
            Success = success,
            Payload = obj
        };

        return response;
    }
}