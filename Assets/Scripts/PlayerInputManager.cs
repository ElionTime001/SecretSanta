using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputActions InputActions;

    void Awake()
    {
        if (InputActions == null)
        {
            InputActions = new PlayerInputActions();
            InputActions.Player.Enable();
        }
    }
}
