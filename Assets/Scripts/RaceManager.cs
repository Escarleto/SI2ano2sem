using UnityEngine;
using System.Collections;
public class RaceManager : MonoBehaviour
{
    /*[System.NonSerialized]*/ public int PlayerLaps = 0;
    [System.NonSerialized] public int LastCheckpointIndex = -1;
    public float DistanceToNextCheckpoint { get; private set; }
    public Transform NextCheckpoint; // O transform do checkpoint que o player está buscando
    [System.NonSerialized] public bool CanCrossFinishLine = false;

    [System.NonSerialized] public int CurrentPlace;

    private PlayerUI PlayerHUD;

    private void Start()
    {
        PlayerLaps = 0;
        NextCheckpoint = TrackPlacer.Instance.Checkpoints[0];
        StartCoroutine(StartRace());
    }

    public void SetNewPlace(int NewPlace)
    {
        if (CurrentPlace == NewPlace) return;
        CurrentPlace = NewPlace;
        PlayerHUD = GetComponent<PlayerUI>();
        PlayerHUD.UpdatePlace(NewPlace);
    }

    public void UpdateDistanceToNextCheckpoint()
    {
        if (NextCheckpoint != null)
            DistanceToNextCheckpoint = Vector3.Distance(transform.position, NextCheckpoint.position);
    }

    public float GetProgress()
    {
        float CheckpointProgress = 0f;

        if (NextCheckpoint != null)
        {
            float Distance = Vector3.Distance(transform.position, NextCheckpoint.position);
            CheckpointProgress = -Distance;
        }

        return PlayerLaps * 1000f + LastCheckpointIndex * 100f + CheckpointProgress;
    }

    private IEnumerator StartRace()
    {
        yield return new WaitForSeconds(2f);
        PlayerHUD.ShowCountdownNum(1);
        yield return new WaitForSeconds(1.5f);
        PlayerHUD.ShowCountdownNum(2);
        yield return new WaitForSeconds(1.5f);
        PlayerHUD.ShowCountdownNum(3);
        yield return new WaitForSeconds(1.5f);
        PlayerHUD.ShowCountdownNum(5);
        GetComponent<PlayerController>().enabled = true;
    }
}
