using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Brigit.Runtime;

public static class VersionChecker
{
    public static async Task CheckAsync(string currentVersion, string repoInfo)
    {
        try 
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Brigit-VersionChecker");
            var json = await client.GetStringAsync($"https://api.github.com/repos/{repoInfo}/releases/latest");
            using var doc = JsonDocument.Parse(json);
            var tag = doc.RootElement.GetProperty("tag_name").GetString();
            if (tag != null && !tag.Contains(currentVersion))
            {
            }
        }
        catch {}
    }
}
