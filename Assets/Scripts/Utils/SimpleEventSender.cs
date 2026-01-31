using UnityEngine;
using UnityEngine.Events;

public class SimpleEventSender : MonoBehaviour
{
    [SerializeField] UnityEvent eventToSend;

    public void SendEvent()
    {
        eventToSend?.Invoke();
    }
}
