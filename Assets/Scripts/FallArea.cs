using UnityEngine;

public class FallArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController Player = other.GetComponent<PlayerController>();
            CurrentData Data = other.GetComponent<CurrentData>();

            Player.transform.position = Data.Data.SpawnPoint;
            Player.RB.linearVelocity = Vector3.zero;

            Rigidbody RB = other.GetComponent<Rigidbody>();
            if (RB != null)
                RB.linearVelocity = Vector3.zero;
        }
    }
}
