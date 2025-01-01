using UnityEngine;

public abstract class ReactiveUI : MonoBehaviour
{

    protected virtual void Start()
    {
        EPInputManager.Instance.OnInputDeviceChanged += OnInputDeviceChange;
    }

    protected virtual void OnDestroy()
    {
        EPInputManager.Instance.OnInputDeviceChanged -= OnInputDeviceChange;
    }

    protected abstract void OnInputDeviceChange();
}