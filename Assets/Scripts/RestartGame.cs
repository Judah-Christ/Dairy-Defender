using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private PlayerInput PlayerControls;
    [SerializeField] private InputAction _restart;
    [SerializeField] private InputAction _Quit;


    /// <summary>
    /// mapping the button for restart
    /// </summary>
    void Start()
    {
        PlayerControls.currentActionMap.Enable();
        _restart = PlayerControls.currentActionMap.FindAction("Restart");
       _Quit = PlayerControls.currentActionMap.FindAction("Quit");



        _restart.started += Restart_started;
        _restart.canceled += Restart_canceled;
        _Quit.started += Quit_started;
        _Quit.canceled += Quit_canceled;
    }

    private void Quit_canceled(InputAction.CallbackContext context)
    {
        
    }
    private void Quit_started(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    /// <summary>
    /// not in use yet or at all
    /// </summary>
    private void Restart_canceled(InputAction.CallbackContext obj)
    {

    }

    /// <summary>
    /// setting game restart when hit
    /// </summary>
    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// destroy the restart and resetting the value
    /// </summary>
    private void OnDestroy()
    {

    }
}
