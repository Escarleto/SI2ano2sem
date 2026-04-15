using UnityEngine;
using System.Dynamic;

public class Giant : MonoBehaviour, Item
{
    [SerializeField] private Sprite ItemIcon;
    public Sprite ItemIconUI => ItemIcon; // Retorna o ícone do item para a UI

    public void Use(PlayerController Player)
    {
        Player.StartCoroutine(Player.Giant()); // Inicia a coroutine para aplicar o efeito do item Giant
    }
}
