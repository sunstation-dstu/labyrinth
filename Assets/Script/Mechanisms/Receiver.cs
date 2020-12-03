using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Приёмник
/// </summary>
public class Receiver : MonoBehaviour, IReceiver
{
    public bool isEnabled = true;
    public GameObject subjectToEnable;
    public Dictionary<int, bool> sendersStates = new Dictionary<int, bool>();

    private void Start()
    {
        CheckStatus();
    }

    private void CheckStatus()
    {
        isEnabled = true;
        foreach (var senderId in sendersStates.Keys)
        {
            if (!GetSenderState(senderId))
            {
                isEnabled = false;
                break;
            }
        }

        if (subjectToEnable)
            subjectToEnable.SetActive(isEnabled);
    }

    public void AddSenderState(int senderId, bool state)
    {
        sendersStates.Add(senderId, state);
        CheckStatus();
    }
    public bool GetSenderState(int senderId)
    {
        return sendersStates[senderId];
    }

    public virtual void Receive(int senderId, bool state)
    {
        sendersStates[senderId] = state;
        CheckStatus();
    }
}