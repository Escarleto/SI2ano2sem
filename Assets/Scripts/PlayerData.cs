using System.Buffers;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerData
{
    public int PlayerIndex;
    public int DeviceId;
    public Color PlayerColor;
    public Vector3 SpawnPoint;
    public GameObject Hat;

    public PlayerController Controller;
}

