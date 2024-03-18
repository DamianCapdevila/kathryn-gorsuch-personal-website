using System.Net.Http.Json;
using BlazorApp.Models;

namespace BlazorApp.Services;

public sealed class ImageService : IDisposable
{
    private readonly HttpClient _client;
    private readonly Task<List<Image>?> _getImagesTask;

    public ImageService(HttpClient client)
    {
        _client = client;
        _getImagesTask =
            _client.GetFromJsonAsync<List<Image>>(
                "sample-data/images.json");
    }

    internal async Task<Image?> GetPictureAsync(Func<Image, bool> predicate)
    {
        var pictures = await _getImagesTask;
        return pictures?.FirstOrDefault(predicate);
    }

    internal async Task<List<Image>?> GetPicturesAsync(Func<Image, bool> predicate)
    {
        var pictures = await _getImagesTask;
        return pictures?.Where(predicate).ToList();
    }
    
    public void Dispose() => _client.Dispose();
}
