using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyController : MonoBehaviour
{
    public List<GameObject> candyModels;

    private void Awake()
    {
        int num;
        num = Random.Range(0,candyModels.Count);
        candyModels[num].gameObject.SetActive(true);
    }
}
