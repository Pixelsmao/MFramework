using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace MFramework.Internal
{
    public class GiteeUser : User
    {
        public GiteeUser(UserConfig config) : base(config)
        {
            if (config.UserName == string.Empty || config.AccessToken == string.Empty)
            {
                Debug.LogWarning($"Gitee用户名或访问令牌未填写，请在MFramework目录中填写相关配置信息。");
            }
        }

        protected override IEnumerator GetRequestRepositories()
        {
            if (requesting) yield break;
            requesting = true;
            var url =
                $"https://gitee.com/api/v5/users/{config.UserName}/repos?page={config.Page}&per_page={config.PerPage}&access_token={config.AccessToken}";
            using (var request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.Success)
                {
                    var jsonResponse = request.downloadHandler.text;
                    var giteeRepositories = JsonConvert.DeserializeObject<List<GiteeRepository>>(jsonResponse);
                    if (giteeRepositories != null)
                    {
                        foreach (var repository in giteeRepositories)
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
            var giteeRepository = repository as GiteeRepository;
            var url = $"https://gitee.com/api/v5/repos/{repository.Author}/{repository.Name}/contents/?ref=main";
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.url += "&access_token=" + config.AccessToken;
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.Success)
                {
                    var repositoryContent = request.downloadHandler.text;
                    var fileInfos = JsonConvert.DeserializeObject<List<GiteeFileInfo>>(repositoryContent);
                    if (fileInfos != null)
                    {
                        foreach (var fileInfo in fileInfos)
                        {
                            if (!fileInfo.Name.ToLower().Contains("package.json")) continue;
                            // ReSharper disable once PossibleNullReferenceException
                            giteeRepository.IsUnityPackage = true;
                            giteeRepository.CheckInstalled();
                            yield break;
                        }
                    }
                }
                else
                {
                    Debug.Log($"Repository {repository.Name} Is Unity Package Verify Error: {request.error}");
                }

                if (giteeRepository != null) giteeRepository.IsUnityPackage = false;
            }
        }
    }
}