using Interfaces;
using UnityEngine;

public class TimeData : IGameTime
{
    public float DeltaTime
    {
        get => Time.deltaTime;
    }
}