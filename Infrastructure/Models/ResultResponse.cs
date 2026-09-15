namespace Infrastructure.Models;
public class ResultResponse<T>
{
    public T? Data { get; set; }
    public List<string>? Information { get; set; } = new List<string>();
    public bool IsSuccess { get; set; }
}
