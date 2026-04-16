using UnityEngine;
using System.Collections;

public class RaceManager : MonoBehaviour
{
    [System.NonSerialized] public int PlayerLaps = 1;
    [System.NonSerialized] public int LastCheckpointIndex = -1;
    public float DistanceToNextCheckpoint { get; private set; }
    public Transform NextCheckpoint; // O transform do checkpoint que o player está buscando

    [System.NonSerialized] public int CurrentPlace;

    private PlayerUI playerUI;

    private void Start()
    {
        PlayerLaps = 1;
    }

    public void SetNewPlace(int NewPlace)
    {
        if (CurrentPlace == NewPlace) return;
        CurrentPlace = NewPlace;
        GetComponent<PlayerUI>().UpdatePlace(NewPlace);
    }

    public void UpdateDistanceToNextCheckpoint()
    {
        if (NextCheckpoint != null)
            DistanceToNextCheckpoint = Vector3.Distance(transform.position, NextCheckpoint.position);
    }
}
