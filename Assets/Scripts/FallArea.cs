using UnityEngine;

public class FallArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController Player = other.GetComponent<PlayerController>();
            CurrentData Data = other.GetComponent<CurrentData>();

            Data.Respawn();

            Rigidbody RB = other.GetComponent<Rigidbody>();
            if (RB != null)
                RB.linearVelocity = Vector3.zero;
        }
    }
}
