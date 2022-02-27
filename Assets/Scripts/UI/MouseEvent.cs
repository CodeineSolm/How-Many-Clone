using UnityEngine;
using UnityEngine.Events;

public class MouseEvent : MonoBehaviour
{
    public event UnityAction PointerUp;

    private void OnGUI()
    {
        var pointerEvent = Event.current;

        if (pointerEvent.type == EventType.MouseUp)
            PointerUp?.Invoke();
    }
}
