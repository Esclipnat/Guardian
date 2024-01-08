using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    public Action beforeExitGame;
    public CameraMenuSystem camSystem;
    public SettingsPanelSystem settingsController;
    public CutoutSystem cutoutController;
    [SerializeField] private CanvasGroup menuPanel;
    [SerializeField] private Button PlayBtn;
    [SerializeField] private Button SettingsBtn;
    [SerializeField] private Button ExitBtn;
    [SerializeField] private AudioSource mainMenuTheme;

    private void Start()
    {
        PlayBtn.onClick.AddListener(InitGame);

        SettingsBtn.onClick.AddListener(OpenSettingsPanel);
        ExitBtn.onClick.AddListener(ExitGame);
        settingsController.LoadDataOnPanel();
        settingsController.ApplyChanges();
        settingsController.onQuitingPanel += () =>
        {
            camSystem.SetCameraOnGround(() => menuPanel.DOFade(1, 0.4f).OnComplete(() => menuPanel.interactable = true));
        };
        cutoutController.StartCutout(() =>
        {
            menuPanel.interactable = true;
        }, false);
    }

    public void InitGame()
    {
        StartCoroutine(Fade(mainMenuTheme, 2.0f, 0.0f, () => { cutoutController.StartCutout(() => SceneManager.LoadScene(1)); }));
    }
    public void OpenSettingsPanel()
    {
        menuPanel.DOFade(0, 0.4f);
        camSystem.SetCameraOnSky(() =>
        {
            settingsController.ShowPanel();
        });
    }
    public void ExitGame()
    {
        beforeExitGame?.Invoke();
        cutoutController.StartCutout(() => Application.Quit());
    }

    public IEnumerator Fade(AudioSource source, float duration, float targetVolume, Action onFadeComplete = null)
    {
        float time = 0f;
        float startVolume = source.volume;

        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }
        if (onFadeComplete != null)
        {
            onFadeComplete.Invoke();
        }
    }

}