using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour
{
    [SerializeField] private CutoutSystem cutOutSys;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private RectTransform menuPanel;
    public void Restart()
    {
        cutOutSys.SetRelativePosition(playerPosition);
        menuPanel.gameObject.SetActive(false);
        cutOutSys.StartCutout(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        });
    }
    public void MainMenu()
    {
        cutOutSys.SetRelativePosition(playerPosition);
        menuPanel.gameObject.SetActive(false);
        cutOutSys.StartCutout(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        });
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }

}
