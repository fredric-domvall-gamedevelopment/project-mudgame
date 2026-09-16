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

    public async Task<ResultResponse<List<T>>> WriteToJsonAsync(string filePath, List<T> listData)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonData = JsonSerializer.Serialize(listData, options);

            await File.WriteAllTextAsync(filePath, jsonData);

            return new ResultResponse<List<T>>
            {
                IsSuccess = true,
                Information = new List<string> { "Successfully written to JSON file." }
            };
        }
        catch (Exception ex)
        {
            return new ResultResponse<List<T>>
            {
                IsSuccess = false,
                Information = new List<string> { $"Error writing to JSON file: {ex.Message}" }
            };
        }
    }
}
