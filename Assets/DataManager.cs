using System.Collections;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public AudioMixer mixer;
    public SettingsData settingData;
    private string settingsPath = $"{Application.dataPath}/settingsData.json";

    private void Awake()
    {
        if(instance == null)
        {
            instance = FindAnyObjectByType<DataManager>();
        }
    }

    public void SaveData(SettingsData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(settingsPath, json);
    }

    public SettingsData LoadData()
    {
        if (!File.Exists(settingsPath))
        {
            settingData = RestoreData();
            return settingData;
        }
        else
        {
            SettingsData settings = JsonUtility.FromJson<SettingsData>(File.ReadAllText(settingsPath));
            settingData = settings;
            return settings;
        }
    }
    public SettingsData RestoreData()
    {
        SettingsData data = new()
        {
            resolutions = Screen.resolutions.ToList().IndexOf(Screen.currentResolution),
            quality = QualitySettings.GetQualityLevel(),
            fullScreen = Screen.fullScreen,
            vSync = QualitySettings.vSyncCount != 0,
        };
        data.bgm = 1;
        SaveData(data);
        return data;
    }

}
public class SettingsData
{
    public int resolutions;
    public int quality;
    public bool fullScreen;
    public bool vSync;
    public float bgm;
    public float sfx;
}