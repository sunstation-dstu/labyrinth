using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Отправитель
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Sender : MonoBehaviour, ISender, IInteractable
{
    [HideInInspector]
    public int instanceId;
    public bool isEnabled = false;
    public List<Receiver> receivers = new List<Receiver>();

    private void Reset()
    {
        GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().bounds.size + new Vector3(1, 1, 0);
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void Start()
    {
        instanceId = transform.gameObject.GetInstanceID();

        foreach (Receiver receiver in receivers)
            receiver.AddSenderState(instanceId, isEnabled);
    }

    private void SwitchStatus()
    {
        isEnabled = !isEnabled;
    }
    public void Send()
    {
        foreach (Receiver receiver in receivers)
        {
            receiver.Receive(instanceId, isEnabled);
        }
    }
    public void Interact()
    {
        SwitchStatus();
        Send();
    }
}