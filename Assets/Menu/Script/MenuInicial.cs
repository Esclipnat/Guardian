using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class MenuInicial : MonoBehaviour
{
    private AudioSource source;
    private void Start()
    {
        source = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        source.volume = 0f;

    }
    public void Jugar()
    {
        StartCoroutine(Fade(source, 2.0f, 0.0f, () => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }));
    }
    public void Salir()
    {
        Debug.Log("Saliendo....");
        Application.Quit();
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
