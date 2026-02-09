using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class ClaimManager
{
    private List<Transform> unclaimed = new();
    private List<Transform> claimed = new();

    public void AddClaimable(Transform t)
    {
        if (t == null)
            Debug.LogError("Attempted to add null claim!");
        unclaimed.Add(t);
    }

    public Transform ClaimRandom()
    {
        if (unclaimed.Count == 0) return null;

        int index = Random.Range(0, unclaimed.Count);
        Transform target = unclaimed[index];
        unclaimed.RemoveAt(index);
        claimed.Add(target);
        return target;
    }

    public void RescindClaim(Transform claim)
    {
        if (claim == null)
            Debug.LogError("Attempted to rescind claim to null!");

        claimed.Remove(claim);
        unclaimed.Add(claim);
    }
}
