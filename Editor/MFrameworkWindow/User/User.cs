using System;
using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEngine;

namespace MFramework.Internal
{
    public abstract class User
    {
        public readonly UserConfig config;
        public List<RemoteRepository> repositories { get; protected set; } = new List<RemoteRepository>();
        protected bool requesting;

        protected User(UserConfig config)
        {
            this.config = config;
        }

        public void TryRequestRepositories()
        {
            try
            {
                EditorCoroutineUtility.StartCoroutine(GetRequestRepositories(), this);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        protected abstract IEnumerator GetRequestRepositories();
        protected abstract IEnumerator VerifyUnityPackage(RemoteRepository repository);
    }
}