using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuState
{
    [SerializeField] private MouseClickSystem mouseClick;
    [SerializeField] private PlayerInput inputs;

    private InputAction moveAction;
    private InputAction summitAction;
    private InputAction cancelAction;
    public void PlayGame()
    {

    }
    public void SettingsPanel()
    {

    }
    public void ExitGame()
    {

    }

    public enum ButtonType { Play, Settings, Exit, None}
}
