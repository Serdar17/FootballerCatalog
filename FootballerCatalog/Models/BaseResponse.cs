namespace Web.Models;

public class BaseResponse
{
    public int StatusCode { get; set; } = 200;
    
    public string Description { get; set; }
    
    public Player Player { get; init; }
    
}