using System;
using Newtonsoft.Json;

namespace MFramework.Internal
{
    public class GiteeRepository : RemoteRepository
    {
        [JsonProperty("id")] public long Id { get; set; }
        [JsonProperty("full_name")] public string FullName { get; set; } = string.Empty;
        [JsonProperty("human_name")] public string HumanName { get; set; } = string.Empty;
        [JsonProperty("url")] public string url { get; set; } = string.Empty;
        [JsonProperty("namespace")] public GiteeNamespace Namespace { get; set; }
        [JsonProperty("path")] public string Path { get; set; } = string.Empty;
        [JsonProperty("Name")] public override string Name { get; set; } = string.Empty;
        [JsonProperty("owner")] public GiteeUserInfo Owner { get; set; }
        [JsonProperty("assigner")] public GiteeUserInfo Assigner { get; set; }
        [JsonProperty("description")] public override string Description { get; set; } = string.Empty;
        [JsonProperty("private")] public bool @private { get; set; }
        [JsonProperty("public")] public bool @public { get; set; }
        [JsonProperty("internal")] public bool @internal { get; set; }
        [JsonProperty("fork")] public bool fork { get; set; }
        [JsonProperty("html_url")] public override string HtmlUrl { get; set; }

        [JsonProperty("ssh_url")] public string SshUrl { get; set; } = string.Empty;
        [JsonProperty("forks_url")] public string ForksUrl { get; set; } = string.Empty;
        [JsonProperty("keys_url")] public string KeysUrl { get; set; } = string.Empty;
        [JsonProperty("collaborators_url")] public string CollaboratorsUrl { get; set; } = string.Empty;
        [JsonProperty("hooks_url")] public string HooksUrl { get; set; } = string.Empty;
        [JsonProperty("branches_url")] public string BranchesUrl { get; set; } = string.Empty;
        [JsonProperty("tags_url")] public string TagsUrl { get; set; } = string.Empty;
        [JsonProperty("blobs_url")] public string BlobsUrl { get; set; } = string.Empty;
        [JsonProperty("stargazers_url")] public string StargazersUrl { get; set; } = string.Empty;
        [JsonProperty("contributors_url")] public string ContributorsUrl { get; set; } = string.Empty;
        [JsonProperty("commits_url")] public string CommitsUrl { get; set; } = string.Empty;
        [JsonProperty("comments_url")] public string CommentsUrl { get; set; } = string.Empty;
        [JsonProperty("issue_comment_url")] public string IssueCommentUrl { get; set; } = string.Empty;
        [JsonProperty("issues_url")] public string IssuesUrl { get; set; } = string.Empty;
        [JsonProperty("pulls_url")] public string PullsUrl { get; set; } = string.Empty;
        [JsonProperty("milestones_url")] public string MilestonesUrl { get; set; } = string.Empty;
        [JsonProperty("notifications_url")] public string NotificationsUrl { get; set; } = string.Empty;
        [JsonProperty("labels_url")] public string LabelsUrl { get; set; } = string.Empty;
        [JsonProperty("releases_url")] public string ReleasesUrl { get; set; } = string.Empty;
        [JsonProperty("recommend")] public string Recommend { get; set; } = string.Empty;
        [JsonProperty("gvp")] public bool Gvp { get; set; }
        [JsonProperty("homepage")] public string Homepage { get; set; } = string.Empty;
        [JsonProperty("language")] public string Language { get; set; } = string.Empty;
        [JsonProperty("forks_count")] public int ForksCount { get; set; }
        [JsonProperty("stargazers_count")] public int StargazersCount { get; set; }
        [JsonProperty("watchers_count")] public int WatchersCount { get; set; }
        [JsonProperty("default_branch")] public string DefaultBranch { get; set; } = string.Empty;
        [JsonProperty("open_issues_count")] public int OpenIssuesCount { get; set; }
        [JsonProperty("has_issues")] public bool HasIssues { get; set; }
        [JsonProperty("has_wiki")] public bool HasWiki { get; set; }
        [JsonProperty("issue_comment")] public bool IssueComment { get; set; }
        [JsonProperty("can_comment")] public bool CanComment { get; set; }

        [JsonProperty("pull_requests_enabled")]
        public bool PullRequestsEnabled { get; set; }

        [JsonProperty("has_page")] public bool HasPage { get; set; }
        [JsonProperty("license")] public string License { get; set; } = string.Empty;
        [JsonProperty("outsourced")] public bool Outsourced { get; set; }
        [JsonProperty("project_creator")] public string ProjectCreator { get; set; } = string.Empty;
        [JsonProperty("members")] public string[] Members { get; set; }
        [JsonProperty("pushed_at")] public DateTime PushedAt { get; set; }
        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }
        [JsonProperty("parent")] public string Parent { get; set; } = string.Empty;
        [JsonProperty("paas")] public string Paas { get; set; } = string.Empty;
        [JsonProperty("stared")] public string Stared { get; set; } = string.Empty;
        [JsonProperty("watched")] public string Watched { get; set; } = string.Empty;
        [JsonProperty("permission")] public Permissions Permission { get; set; }
        [JsonProperty("relation")] public string Relation { get; set; } = string.Empty;
        [JsonProperty("assignees_number")] public int AssigneesNumber { get; set; }
        [JsonProperty("testers_number")] public int TestersNumber { get; set; }
        [JsonProperty("assignee")] public GiteeUserInfo[] Assignee { get; set; }
        [JsonProperty("testers")] public GiteeUserInfo[] Testers { get; set; }
        [JsonProperty("status")] public string Status { get; set; } = string.Empty;
        [JsonProperty("programs")] public string[] Programs { get; set; }

        [JsonProperty("issue_template_source")]
        public string IssueTemplateSource { get; set; } = string.Empty;

        public override string ManifestKey => $"com.{Owner.Name.ToLower()}.{Name.ToLower()}";
        public override string Author => Owner.Name;
        public override bool IsUnityPackage { get; set; }

        public class GiteeNamespace
        {
            [JsonProperty("id")] public long Id { get; set; }
            [JsonProperty("type")] public string Type { get; set; }
            [JsonProperty("name")] public string Name { get; set; }
            [JsonProperty("path")] public string Path { get; set; }
            [JsonProperty("html_url")] public string HtmlUrl { get; set; }
        }

        /// <summary>
        /// 仓库权限信息
        /// </summary>
        public class Permissions
        {
            [JsonProperty("pull")] public bool CanPull { get; set; }

            [JsonProperty("push")] public bool CanPush { get; set; }

            [JsonProperty("admin")] public bool IsAdmin { get; set; }
        }
    }
}