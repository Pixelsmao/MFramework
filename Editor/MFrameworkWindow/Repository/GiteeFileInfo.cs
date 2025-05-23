using Newtonsoft.Json;

namespace MFramework.Internal
{
    public class GiteeFileInfo
    {
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("size")] public string Size { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("path")] public string Path { get; set; }
        [JsonProperty("sha")] public string GUID { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("html_url")] public string HtmlUrl { get; set; }
        [JsonProperty("download_url")] public string DownloadUrl { get; set; }
        [JsonProperty("_links")] public Links links { get; set; }

        public class Links
        {
            [JsonProperty("self")] public string Self { get; set; }
            [JsonProperty("html")] public string Html { get; set; }
        }
    }
}