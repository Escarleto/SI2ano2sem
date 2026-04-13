using UnityEngine;

public class FallArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleporta o jogador de volta para o ponto de spawn definido no PlayerController
            other.GetComponent<PlayerController>().transform.position = other.GetComponent<PlayerController>().SpawnPoint;
            Rigidbody RB = other.GetComponent<Rigidbody>();
            if (RB != null)
            {
                RB.linearVelocity = Vector3.zero; // Reseta a velocidade do jogador para evitar que ele continue se movendo após ser teletransportado
            }
        }
    }
}
