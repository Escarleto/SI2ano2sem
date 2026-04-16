using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GetPlayers : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;

    private void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        List<PlayerData> Players = Manager.Instance.PlayersInGame;

        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i] == null) continue;

            PlayerData data = Players[i];

            InputDevice device = InputSystem.GetDeviceById(data.DeviceId);

            PlayerInput playerObj = PlayerInput.Instantiate(
                PlayerPrefab,
                playerIndex: data.PlayerIndex,
                pairWithDevice: device,
                controlScheme: "Gamepad"
            );

            // 👇 AQUI É O PULO DO GATO
            CurrentData playerData = playerObj.GetComponent<CurrentData>();

            playerData.Data.PlayerIndex = data.PlayerIndex;
            playerData.Data.DeviceId = data.DeviceId;
            playerData.Data.PlayerColor = data.PlayerColor;
            playerData.Data.SpawnPoint = Manager.Instance.PlayerSpawnPoints[data.PlayerIndex];

            // se usar hat por índice:
            playerData.Data.Hat = data.Hat;

            // posição
            playerObj.transform.position = playerData.Data.SpawnPoint;
            playerObj.transform.Rotate(Vector3.up * 90f);
        }
    }
}