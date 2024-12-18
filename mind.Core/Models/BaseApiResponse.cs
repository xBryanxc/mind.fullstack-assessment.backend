using System.Net;

namespace mind.Core.Models;

public class BaseApiResponse
{
    public HttpStatusCode StatusCode { get; set; }

    public bool IsSuccess { get; set; } = true;

    public List<string>? ErrorMessages { get; set; }

    public object? Result { get; set; }
}

