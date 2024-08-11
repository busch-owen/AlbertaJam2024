using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    private WiresHandler _wiresHandler;
    private PanelOpener _panelOpener;
    private FusesHandler _fusesHandler;

    private void OnEnable()
    {
        _wiresHandler ??= FindObjectOfType<WiresHandler>();
        _panelOpener ??= FindObjectOfType<PanelOpener>();
        _fusesHandler ??= FindObjectOfType<FusesHandler>();

        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.MinigameInteractions.MousePosition.performed +=
                i => _wiresHandler.UpdateMousePos(i.ReadValue<Vector2>());
            _playerInput.MinigameInteractions.MouseClick.started += i => _wiresHandler.CheckConnector();
            _playerInput.MinigameInteractions.MouseClick.canceled += i => _wiresHandler.DropWire();
            
            _playerInput.MinigameInteractions.MousePosition.performed += i => _panelOpener.UpdateMousePosition(i.ReadValue<Vector2>());
            _playerInput.MinigameInteractions.MouseClick.started += i => _panelOpener.ShootRayAtDoors();
            
            _playerInput.MinigameInteractions.MousePosition.performed += i => _fusesHandler.UpdateMousePosition(i.ReadValue<Vector2>());
            _playerInput.MinigameInteractions.MouseClick.started += i => _fusesHandler.ClickFuse();
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
