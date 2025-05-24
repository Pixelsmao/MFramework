using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace MFramework.Internal
{
    public class UnityManifest
    {
        [JsonProperty("dependencies")]
        public Dictionary<string, string> dependencies { get; set; } = new Dictionary<string, string>();

        public void AddManifest(string manifestKey, string manifestValue)
        {
            if (!dependencies.TryAdd(manifestKey, manifestValue))
            {
                Debug.Log($"{manifestKey}:{manifestValue} 已经安装!");
            }
        }

        public void RemoveManifest(string manifestKey, string manifestValue)
        {
            if (IsInstalled(manifestKey, manifestValue))
            {
                dependencies.Remove(manifestKey);
            }
        }

        public bool IsInstalled(string manifestKey, string manifestValue)
        {
            return dependencies.ContainsKey(manifestKey) || dependencies.ContainsValue(manifestValue);
        }

        public override string ToString()
        {
            if (dependencies == null || dependencies.Count == 0)
            {
                return "No dependencies";
            }

            var builder = new StringBuilder();
            builder.AppendLine("Dependencies:");
            foreach (var entry in dependencies)
            {
                builder.AppendLine($"{entry.Key}:{entry.Value}");
            }

            return builder.ToString();
        }
    }
}