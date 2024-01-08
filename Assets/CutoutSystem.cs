using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CutoutSystem : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Camera cam;
    private Action onAnimComplete;

    public void StartCutout(Action onComplete, bool outAnim = true)
    {
        onAnimComplete += () =>
        {
            onComplete?.Invoke();
        };
        anim.SetTrigger(outAnim ? "Out" : "In");
    }

    public void SetRelativePosition(Transform relative)
    {
        this.transform.position = cam.WorldToScreenPoint(relative.position);
    }

   public void OnAnimComplete()
   {
        onAnimComplete?.Invoke();
        onAnimComplete = null;
   }
}
