using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UserConfig", menuName = "Scriptable Objects/MFramework/UserConfig")]
public class UserConfig : ScriptableObject
{
    public string UserName = string.Empty;
    [TextArea(5, 10)] public string AccessToken = string.Empty;
    public int Page = 1;
    public int PerPage = 50;
}