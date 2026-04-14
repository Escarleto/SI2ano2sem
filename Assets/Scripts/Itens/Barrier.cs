using System.ComponentModel;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Barrier : MonoBehaviour, Item
{
    private Vector3 BarrierPos;
    private float BarrierRotY;
    [SerializeField] private GameObject BarrierPrefab; // Prefab da barreira a ser instanciada

    public void Use(PlayerController Player)
    {
        //Calcula a posição da barreira atrás do jogador e a rotação para que ela fique alinhada com o jogador
        BarrierPos = Player.MoveDirection.normalized * -1.5f + Player.transform.position; // Calcula a posição da barreira atrás do jogador

        BarrierRotY = Player.MoveDirection == Vector3.zero ? Player.transform.eulerAngles.y : Mathf.Atan2(Player.MoveDirection.x, Player.MoveDirection.z) * Mathf.Rad2Deg; // Rotação da barreira baseada na direção do movimento do jogador
        Instantiate(BarrierPrefab, BarrierPos, Quaternion.Euler(90f, 0f, BarrierRotY)); // Instancia a barreira na posição calculada
    }
}
