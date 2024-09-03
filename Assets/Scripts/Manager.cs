using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public float maxTimePlayer;
    public float currentTimePlayer;

    public float maxTimeServer;
    public float currentTimeServer;

    public bool shutPlayerLed;
    public bool shutServerLed;

    private void Start()
    {
        maxTimePlayer = currentTimePlayer;
        maxTimeServer = currentTimeServer;
    }

    private void Update()
    {
    }
}
