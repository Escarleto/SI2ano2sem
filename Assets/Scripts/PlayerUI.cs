using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerUI : MonoBehaviour
{
    private PlayerController Player;
    [SerializeField] private GameObject Canvas;

    private void Start()
    {
        Player = GetComponent<PlayerController>();

        //Configura a cor do Canvas para ser a mesma do jogador usando o PlayerController para acessar a cor do jogador
        foreach (UnityEngine.UI.Image Image in Canvas.GetComponentsInChildren<UnityEngine.UI.Image>())
            Image.color = Player.PlayerColor;
    }
}
