using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    private WiresHandler _wiresHandler;
    private PanelOpener _panelOpener;

    private void OnEnable()
    {
        _wiresHandler ??= FindObjectOfType<WiresHandler>();
        _panelOpener ??= FindObjectOfType<PanelOpener>();

        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.MinigameInteractions.MousePosition.performed +=
                i => _wiresHandler.UpdateMousePos(i.ReadValue<Vector2>());
            _playerInput.MinigameInteractions.MouseClick.started += i => _wiresHandler.CheckConnector();
            _playerInput.MinigameInteractions.MouseClick.canceled += i => _wiresHandler.DropWire();
            _playerInput.MinigameInteractions.MousePosition.performed += i => _panelOpener.UpdateMousePosition(i.ReadValue<Vector2>());
            _playerInput.MinigameInteractions.MouseClick.started += i => _panelOpener.ShootRayAtDoors();
        }

        //EnableInput();
    }

    public void DisableInput()
    {
        _playerInput.Disable();
    }

    public void EnableInput()
    {
        _playerInput.Enable();
    }
}
