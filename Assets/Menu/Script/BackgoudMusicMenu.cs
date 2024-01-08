using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgoudMusicMenu : MonoBehaviour
{
    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0f;
        StartCoroutine(Fade(true,source,2f,1f));
        StartCoroutine(Fade(false, source, 2f, 0f));
    }

    public IEnumerator Fade(bool fadeIn,AudioSource sourse,float duration,float targetVolumen)
    {
        if (!fadeIn) 
        {
            double lenhtOfSourse = (double)source.clip.samples / source.clip.frequency;
            yield return new WaitForSecondsRealtime((float)(lenhtOfSourse-duration));
        }
        float time = 0f;
        float startVolumen = sourse.volume;
        while (time < duration)
        {
            time+= Time.deltaTime;
            sourse.volume = Mathf.Lerp(startVolumen,targetVolumen,time/duration);
            yield return null; 
        }
        yield break;
    }
}
