using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnPressHandling;

    private Task _pressTask;
    private bool _isHandlingStarted;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHandlingStarted = true;
        _pressTask = HandleProcess();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHandlingStarted = false;
        if (_pressTask != null && !_pressTask.IsCompleted)
        {
            _pressTask = null;
        }
    }

    private async Task HandleProcess()
    {
        while(_isHandlingStarted)
        {
            OnPressHandling?.Invoke();
            await Task.Yield();
        }
    }
}
