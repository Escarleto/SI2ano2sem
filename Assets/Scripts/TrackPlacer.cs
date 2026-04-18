using UnityEngine;

public class TrackPlacer : MonoBehaviour
{
    public static TrackPlacer Instance;

    public Transform[] Checkpoints;

    private void Awake()
    {
        Instance = this;
    }

}
