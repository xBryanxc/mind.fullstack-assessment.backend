using System.Net;
using System.Text.Json.Serialization;

namespace mind.Core.Models;

public class BaseApiResponse
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? ErrorMessages { get; set; }
    
    public object? Result { get; set; }
}

