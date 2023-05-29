namespace Backend;

public class ApiEndpoint
{
    public string Path { get; }
    public Delegate Handler { get; }
    public HttpMethod Method { get; set; } = HttpMethod.Get;

    public ApiEndpoint(string path, Delegate handler, HttpMethod? method = null)
    {
        Path = path;
        Handler = handler;
        Method = method ?? HttpMethod.Get;
    }
}