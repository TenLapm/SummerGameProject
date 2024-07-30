using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum powerUpType
{
    Default, UpSize, Explosion, Clone, JamBomb, JamShot
}

public enum Instant
{
    Default, on , off
}

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUp")]


public class PowerUps : ScriptableObject
{
    public new string name;
    public powerUpType type;
    public Sprite artwork;
    public float duration;
    public float scale;
    public List<GameObject> Clone = new List<GameObject>();
    public Instant instant;
}
