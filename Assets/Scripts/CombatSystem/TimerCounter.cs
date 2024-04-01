using System;
using UnityEngine;

public class TimerCounter : MonoBehaviour
{
    public float spawnTimer;
    public float maxSpawnTime = 3;
    public bool allowCounter = true;

    private Action Callback;

    public void RegisterCallback(Action action)
    {
        Callback += action;
    }

    private void OnDisable()
    {
        Callback = null;
    }

    // Update is called once per frame
    private void Update()
    {
        if (allowCounter == false) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            Callback?.Invoke();
            spawnTimer = maxSpawnTime;
        }
    }
}