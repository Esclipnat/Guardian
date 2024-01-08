using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SettingsPanelSystem : MonoBehaviour
{
    [SerializeField] private CanvasGroup panel;
    [SerializeField] private Button apply_Btn;
    [SerializeField] private Button back_Btn;
    [SerializeField] private TMP_Dropdown resolution_DD;
    [SerializeField] private TMP_Dropdown gameQuality_DD;
    [SerializeField] private Toggle fullScreen_TG;
    [SerializeField] private Toggle vSync_TG;
    [SerializeField] private Slider bgm_SD;
    [SerializeField] private Slider sfx_SD;
    [SerializeField] private AudioMixer mixer;
    public Action onQuitingPanel;

    private void Awake()
    {
        apply_Btn.onClick.AddListener(ApplyChanges);
        back_Btn.onClick.AddListener(HidePanel);
    }

    public void ShowPanel(Action onComplete = null)
    {
        LoadDataOnPanel();
        panel.DOFade(1, 1).OnComplete(() =>
        {
            panel.interactable = true;
            onComplete?.Invoke();
        });
    }

    public void HidePanel()
    {
        panel.interactable = false;
        panel.DOFade(0, 1).OnComplete(() =>
        {
            onQuitingPanel?.Invoke();
        });
    }

    private List<TMP_Dropdown.OptionData> GetResolutions()
    {
        List<TMP_Dropdown.OptionData> resolutions = new();
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            TMP_Dropdown.OptionData resolution = new();
            resolution.text = $"{Screen.resolutions[i].width}x{Screen.resolutions[i].height}";
            resolutions.Add(resolution);
        }
        return resolutions;
    }

    public void LoadDataOnPanel()
    {
        SettingsData data = DataManager.instance.LoadData();
        resolution_DD.options = GetResolutions();
        resolution_DD.value = data.resolutions;
        gameQuality_DD.value = data.quality;
        fullScreen_TG.isOn = data.fullScreen;
        vSync_TG.isOn = data.vSync;
        bgm_SD.value = data.bgm;
        //fx_SD.value = data.sfx;
    }
    public void SaveDataFromPanel()
    {
        DataManager.instance.SaveData(new()
        {
            resolutions = resolution_DD.value,
            quality = gameQuality_DD.value,
            fullScreen = fullScreen_TG.isOn,
            vSync = vSync_TG.isOn,
            bgm = bgm_SD.value,
            //sfx = sfx_SD.value
        });
    }
    public void ApplyChanges()
    {
        QualitySettings.SetQualityLevel(gameQuality_DD.value);
        Screen.SetResolution((int)GetCurrentSelectedResolution().x, (int)GetCurrentSelectedResolution().y, 
            fullScreen_TG.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
        QualitySettings.vSyncCount = vSync_TG.isOn ? 1 : 0;
        mixer.SetFloat("Volumen", MathF.Log10(bgm_SD.value) * 20);
        SaveDataFromPanel();
    }

    private Vector2 GetCurrentSelectedResolution()
    {
        return new Vector2(Screen.resolutions[resolution_DD.value].width, Screen.resolutions[resolution_DD.value].height);
    }
}

