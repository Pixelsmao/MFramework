using System.Collections;
using System.Collections.Generic;
using MFramework.Internal;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class GithubUser : User
{
    public GithubUser(UserConfig config) : base(config)
    {
        if (config.UserName == string.Empty || config.AccessToken == string.Empty)
        {
            Debug.LogWarning($"Github用户名或访问令牌未填写，请在MFramework目录中填写相关配置信息。");
        }
    }

    protected override IEnumerator GetRequestRepositories()
    {
        if (requesting) yield break;
        requesting = true;
        var url = $"https://api.github.com/users/{config.UserName}/repos?page={config.Page}&per_page={config.PerPage}";
        using (var request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", $"Bearer {config.AccessToken}");
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                var requestResult = request.downloadHandler.text;
                var githubRepositories = JsonConvert.DeserializeObject<List<GithubRepository>>(requestResult);
                if (githubRepositories != null)
                {
                    foreach (var repository in githubRepositories)
                    {
                        yield return VerifyUnityPackage(repository);
                        repositories.Add(repository);
                    }
                }
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
                requesting = false;
                yield break;
            }
        }

        requesting = false;
    }

    protected override IEnumerator VerifyUnityPackage(RemoteRepository repository)
    {
        var githubRepository = repository as GithubRepository;
        if (githubRepository == null) yield break;
        var url = $"https://api.github.com/repos/{githubRepository.FullName}/contents/";
        using (var request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("User-Agent", repository.Name);
            request.SetRequestHeader("Authorization", $"Bearer {config.AccessToken}");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var repositoryContent = request.downloadHandler.text;
                var fileInfos = JsonConvert.DeserializeObject<List<GithubFileInfo>>(repositoryContent);
                if (fileInfos != null)
                {
                    foreach (var fileInfo in fileInfos)
                    {
                        if (!fileInfo.Name.ToLower().Contains("package.json")) continue;
                        githubRepository.IsUnityPackage = true;
                        githubRepository.CheckInstalled();
                        yield break;
                    }
                }
            }
            else
            {
                Debug.Log($"Repository {repository.Name} Is Unity Package Verify Error: {request.error}");
            }

            githubRepository.IsUnityPackage = false;
        }
    }
}