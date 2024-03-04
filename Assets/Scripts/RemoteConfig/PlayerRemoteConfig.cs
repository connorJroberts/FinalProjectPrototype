using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class PlayerRemoteConfig : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    public struct userAttributes { }
    public struct appAttributes { }

    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async Task Start()
    {
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());

        foreach (var prop in typeof(PlayerData).GetFields())
        {
            if (prop.FieldType == typeof(int)) prop.SetValue(playerData, Convert.ToInt32(RemoteConfigService.Instance.appConfig.config.GetValue(prop.Name).ToString()));
            else prop.SetValue(playerData, Convert.ToSingle(RemoteConfigService.Instance.appConfig.config.GetValue(prop.Name).ToString()));
        }

    }
}
