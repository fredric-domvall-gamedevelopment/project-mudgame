using Infrastructure.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Repositories;

public class JsonFileRepository<T>
{
    public async Task<ResultResponse<List<T>>> ReadFromJsonAsync(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                return new ResultResponse<List<T>> { IsSuccess = false, Data = new List<T>(), Information = new List<string> { "JSON file not found." } };

            var jsonData = await File.ReadAllTextAsync(filePath);

            if (string.IsNullOrWhiteSpace(jsonData))
                return new ResultResponse<List<T>> { IsSuccess = false, Data = new List<T>(), Information = new List<string> { "JSON file is empty." } };
            
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            options.Converters.Add(new JsonStringEnumConverter());
            var listData = JsonSerializer.Deserialize<List<T>>(jsonData, options);

            if (listData == null)
                return new ResultResponse<List<T>> { IsSuccess = false, Data = new List<T>(), Information = new List<string> { "Failed to deserialize JSON data." } };

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

            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

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
