using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerShooting))]
public class PlayerInput : MonoBehaviour
{
    private PlayerControls _inputActions;
    private PlayerShooting _playerShooting;

    #region MonoBehaviour

    private void Awake()
    {
        _inputActions = new PlayerControls();
        _playerShooting = GetComponent<PlayerShooting>();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Shooting.performed += OnShootingPerform;
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
        _inputActions.Player.Shooting.performed -= OnShootingPerform;
    }

    #endregion

    private void OnShootingPerform(InputAction.CallbackContext context)
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(_inputActions.Player.MousePosition.ReadValue<Vector2>());
        _playerShooting.TryShoot(target);
    }
}
