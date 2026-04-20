using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private int CheckPointIndex; // Defina 0, 1, 2... no Inspector
    [SerializeField] private Transform SpawnPos;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        RaceManager Racer = other.GetComponent<RaceManager>();
        if (Racer == null) return;

        Racer.LastCheckpointIndex = CheckPointIndex;
        int NextIndex = CheckPointIndex + 1;

        if (NextIndex >= TrackPlacer.Instance.Checkpoints.Length)
            NextIndex = 0;

        Racer.NextCheckpoint = TrackPlacer.Instance.Checkpoints[NextIndex];

        Racer.CanCrossFinishLine = true;

        other.GetComponent<CurrentData>().Data.SpawnPoint = SpawnPos.position;
    }
}