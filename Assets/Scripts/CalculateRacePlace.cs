using System.Buffers;
using UnityEngine;

public class CalculateRacePlace : MonoBehaviour
{
    private void LateUpdate()
    {
        Manager.Instance.UpdateRankings();
    }
}
