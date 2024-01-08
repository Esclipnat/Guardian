using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameManagerController : MonoBehaviour
{
    public static GameManagerController instance;
    public CutoutSystem cutSystem;

    public int HP;
    public int candy;
    public List<GameObject> heartList;
    public TextMeshProUGUI pointsText;
    public int currPoints;
    public Action onPointsChanged;
    public Color newColor;
    public GameObject ScreenLoss;
    public GameObject Pause;

    public PlayerInput input;
    private InputAction escPause;
    public float toggleCooldown;
    public bool canToggle;

    private void Awake()
    {
        escPause = input.actions["EscPause"];
        instance = FindAnyObjectByType<GameManagerController>();
    }

    private void Start()
    {
        Time.timeScale = 0;
        cutSystem.StartCutout(() => Time.timeScale = 1, false);
    }

    public void sumPoints(int p) //Receives points to add, subtracts if it's negative
    {
        currPoints += p;
        currPoints = Mathf.Clamp(currPoints, 0, 9999);
        pointsText.text = currPoints.ToString();
        if (p > 0) newColor = Color.green;
        else newColor = Color.red;

        onPointsChanged?.Invoke();
        pointsText.color = newColor;
        StartCoroutine(PointsReturnToWhite());
    }

    public void LoseHP()
    {
        if(HP >= 1)
        {
            HP--;
            heartList[HP].SetActive(false);
        }
        if (HP == 0)
        {
            Time.timeScale = 0;
            ScreenLoss.SetActive(true);
        }
    }
    public void Update()
    {
            if(canToggle)
        {
            TogglePause(Pause.activeSelf);
        }
    }

    public void TogglePause(bool isPaused)
    {
        
        if (!ScreenLoss.activeSelf && escPause.IsPressed())
        {
            StartCoroutine(ReActivateToggle());
            if (!isPaused)
            {
                Time.timeScale = 0;
                Pause.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                Pause.SetActive(false);
            }
            
        }
    }

    IEnumerator PointsReturnToWhite()
    {
        yield return new WaitForSeconds(1);
        pointsText.color = Color.white;
    }
    IEnumerator ReActivateToggle()
    {
        canToggle = false;
        yield return new WaitForSecondsRealtime(toggleCooldown);
        canToggle = true;
    }
}

