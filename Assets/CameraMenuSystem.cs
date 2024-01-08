using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenuSystem : MonoBehaviour
{
    [SerializeField] private Animator cameraAnim;
    public Action onSkyComplete;
    public Action onGroundComplete;
    public void SetCameraOnSky(Action OnAnimComplete)
    {
        onSkyComplete += () => OnAnimComplete.Invoke();
        cameraAnim.SetTrigger("SkyView");
    }
    public void SetCameraOnGround(Action OnAnimComplete)
    {
        onGroundComplete += () => OnAnimComplete.Invoke();
        cameraAnim.SetTrigger("GroundView");
    }

    public void OnSkyComplete()
    {
        onSkyComplete?.Invoke();
        onSkyComplete = null;
    }
    public void OnGroundComplete()
    {
        onGroundComplete?.Invoke();
        onGroundComplete = null;
    }
}
