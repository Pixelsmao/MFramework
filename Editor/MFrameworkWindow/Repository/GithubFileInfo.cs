using Newtonsoft.Json;

public class GithubFileInfo
{
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("path")] public string Path { get; set; }
    [JsonProperty("sha")] public string GUID { get; set; }
    [JsonProperty("size")] public string Size { get; set; }
    [JsonProperty("url")] public string Url { get; set; }
    [JsonProperty("html_url")] public string HtmlUrl { get; set; }
    [JsonProperty("git_url")] public string GitUrl { get; set; }
    [JsonProperty("download_url")] public string DownloadUrl { get; set; }
    [JsonProperty("type")] public string Type { get; set; }
    [JsonProperty("_links")] public Links links { get; set; }

    public class Links
    {
        [JsonProperty("self")] public string Self { get; set; }
        [JsonProperty("git")] public string Git { get; set; }
        [JsonProperty("html")] public string Html { get; set; }
    }
}