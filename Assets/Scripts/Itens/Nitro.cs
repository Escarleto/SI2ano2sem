using UnityEngine;

public class Nitro : MonoBehaviour, Item
{
    [SerializeField] private Sprite ItemIcon; // Ícone do item para exibir na UI
    public Sprite ItemIconUI => ItemIcon; // Retorna o ícone do item para a UI

    public void Use(PlayerController Player)
    {
        Player.StartCoroutine(Player.Nitro()); // Inicia a coroutine do item Nitro no PlayerController para aplicar o efeito de aumento de velocidade
    }
}
