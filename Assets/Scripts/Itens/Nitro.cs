using UnityEngine;

public class Nitro : MonoBehaviour, Item
{
    public void Use(PlayerController Player)
    {
        Player.MoveSpeed *= 3.5f;
        Player.Invoke("ResetMoveSpeed", 3f); // Chama o método ResetMoveSpeed após 3 segundos para resetar a velocidade do jogador
    }
}
