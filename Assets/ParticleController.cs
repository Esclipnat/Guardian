using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;

    public void SetInstruction(Action<ParticleSystem> particleAction)
    {
        particles.ToList().ForEach(x => particleAction.Invoke(x));
    }
    public void Play()
    {
        particles.ToList().ForEach(x => x.Play());
    }
    public void Stop()
    {
        particles.ToList().ForEach(x => x.Stop());
    }
}
