using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private float amount;
    [SerializeField] private List<ParticleController> bloodParticles = new();
    private int currentIndex;

    public static BloodSpawnerController instance;

    private void Awake()
    {
        instance = FindAnyObjectByType<BloodSpawnerController>();
        SetupSystem();
    }

    public void StopAllParticles(){ bloodParticles.ForEach(x => x.Stop()); }

    private void SetupSystem()
    {
        for (int i = 0; i < amount; i++) 
        {
            ParticleController newParticle = Instantiate(bloodPrefab, this.transform).GetComponent<ParticleController>();
            bloodParticles.Add(newParticle);
            newParticle.transform.position = new Vector3(999, 999, 999);
        }
          

    }

    public void SpawnNewBlood(Vector3 positionInWorldSpace)
    {
        if (currentIndex >= amount) currentIndex = 0;
        currentIndex++;
        bloodParticles[currentIndex].transform.position = new Vector3(positionInWorldSpace.x, this.transform.position.y, positionInWorldSpace.z);
        bloodParticles[currentIndex].Stop();
        bloodParticles[currentIndex].Play();
    }
}
