using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Посреднический механизм
/// </summary>
public class Mediator : Receiver, ISender, IReceiver
{
    [HideInInspector]
    public int instanceId;
    public List<Receiver> receivers = new List<Receiver>();

    private void Start()
    {
        instanceId = transform.gameObject.GetInstanceID();

        foreach (Receiver receiver in receivers)
            receiver.sendersStates.Add(instanceId, isEnabled);

        CheckStatus();
        Send();
    }

    private void CheckStatus()
    {
        isEnabled = true;
        foreach (var key in sendersStates.Keys)
        {
            if (!sendersStates[key])
            {
                isEnabled = false;
                break;
            }
        }

        if (subjectToEnable)
            subjectToEnable.SetActive(isEnabled);
    }
    public override void Receive(int senderId, bool state)
    {
        sendersStates[senderId] = state;
        CheckStatus();
        Send();
    }
    public void Send()
    {
        foreach (Receiver receiver in receivers)
        {
            receiver.Receive(instanceId, isEnabled);
        }
    }
}