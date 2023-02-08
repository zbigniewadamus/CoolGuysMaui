namespace CoolGuys.UseCases._contracts;

public class ResponseDto
{
    public string message { get; set; }
    
}

public class ResponseDto<T>: ResponseDto
{
    public T? data { get; set; }
}