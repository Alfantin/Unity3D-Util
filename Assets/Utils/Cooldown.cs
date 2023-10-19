using System;
using UnityEngine;

[Serializable]
public class Cooldown {

    public float Duration;

    private float Timer;

    public Cooldown() {
    }

    public Cooldown(float duration) {
        Duration = duration;
    }

    public bool IsReady { 
        get => Time.time > Timer; 
    }

    public void Next() {
        Timer = Time.time + Duration;
    }

}