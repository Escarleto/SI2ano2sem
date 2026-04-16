using System.Buffers;
using UnityEngine;

public class CurrentData : MonoBehaviour
{
    public PlayerData Data;
    private PlayerController Controller;

    private void Awake()
    {
        Controller = GetComponent<PlayerController>();

        if (Data == null)
            Data = new PlayerData();

        Data.Controller = Controller;
    }

    public void Initialize()
    {
        Manager.Instance.PlayersInGame[Data.PlayerIndex] = Data;
    }
}
