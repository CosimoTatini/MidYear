using Assets.Scripts.Interfaces;
using DesignPatterns.Generics;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// GameManager for input controls
/// </summary>
public class GameManager : Singleton<GameManager>
{
    IControllable _currentControllable;
    private InputSystem_Actions _inputs;

    [SerializeField] CinemachineCamera playerControllerCamera;
    [SerializeField] CinemachineCamera carCamera;
    [SerializeField] CinemachineCamera carFreeLookCamera;

    private bool _isFreeLookActive = false;

    public override void Awake()
    {
        base.Awake();

        SetControllable(FindFirstObjectByType<PlayerController>());

        _inputs = new InputSystem_Actions();

        _inputs.Player.Move.performed += Move_performed;
        _inputs.Player.Move.canceled += Move_canceled;
        _inputs.Player.Interact.performed += Interact_performed;
        _inputs.Player.Next.performed += FreeLookCamera_performed;

        _inputs.Enable();
    }

    private void FreeLookCamera_performed(InputAction.CallbackContext context)
    {
        if (!_isFreeLookActive)
        {
            SwitchToCarFreeLookCamera();
        }
        else
        {
            SwitchToCarCamera();
        }
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _currentControllable.Interact();
    }

    private void Move_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _currentControllable.MoveCanceled();
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _currentControllable.Move(obj.ReadValue<Vector2>());
    }

    public void SetControllable(IControllable controllable)
    {
        _currentControllable = controllable;
    }

    public void SwitchToPlayerCamera()
    {
        playerControllerCamera.gameObject.SetActive(true);
        carCamera.gameObject.SetActive(false);
    }

    public void SwitchToCarCamera()
    {
        carCamera.gameObject.SetActive(true);
        carFreeLookCamera.gameObject.SetActive(false);
        playerControllerCamera.gameObject.SetActive(false);
        _isFreeLookActive = false;
    }

    public void SwitchToCarFreeLookCamera()
    {
        carFreeLookCamera.gameObject.SetActive(true);
        carCamera.gameObject.SetActive(false);
        playerControllerCamera.gameObject.SetActive(false);
        _isFreeLookActive = true;
    }
}