using Infrastructure.Models;
using System.Text.Json;

namespace Infrastructure.Repositories;

public class JsonFileRepository<T>
{
    public async Task<ResultResponse<List<T>>> ReadFromJsonAsync(string filePath)
    {
        try
        {
            var jsonData = await File.ReadAllTextAsync(filePath);

            var listData = JsonSerializer.Deserialize<List<T>>(jsonData);

            return new ResultResponse<List<T>>
            {
                IsSuccess = true,
                Data = listData
            };
        }
        catch (Exception ex)
        {
            return new ResultResponse<List<T>>
            {
                IsSuccess = false,
                Information = new List<string> { $"Error reading from JSON file: {ex.Message}" }
            };
        }
    }
}
