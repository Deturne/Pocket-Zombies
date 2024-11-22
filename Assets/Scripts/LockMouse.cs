

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class CursorHelper : MonoBehaviour
{
#if UNITY_EDITOR
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            ForceMousePositionToCenterOfGameWindow();
            ForceClickMouseButtonInCenterOfGameWindow();
        }
    }
#endif

    public static void ForceMousePositionToCenterOfGameWindow()
    {
#if UNITY_EDITOR
        // Force the mouse to be in the middle of the game screen
        var game = UnityEditor.EditorWindow.GetWindow(typeof(UnityEditor.EditorWindow).Assembly.GetType("UnityEditor.GameView"));
        Vector2 warpPosition = game.rootVisualElement.contentRect.center; // never let it move
        Mouse.current.WarpCursorPosition(warpPosition);
        InputState.Change(Mouse.current.position, warpPosition);
#endif
    }


    public static void ForceClickMouseButtonInCenterOfGameWindow()
    {
#if UNITY_EDITOR
        var game = UnityEditor.EditorWindow.GetWindow(typeof(UnityEditor.EditorWindow).Assembly.GetType("UnityEditor.GameView"));
        Vector2 gameWindowCenter = game.rootVisualElement.contentRect.center;

        Event leftClickDown = new Event();
        leftClickDown.button = 0;
        leftClickDown.clickCount = 1;
        leftClickDown.type = EventType.MouseDown;
        leftClickDown.mousePosition = gameWindowCenter;

        game.SendEvent(leftClickDown);
#endif
    }
}
