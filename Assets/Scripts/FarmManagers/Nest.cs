using UnityEngine;

public class Nest : MonoBehaviour
{
    [SerializeField] Transform claimPosition;

    void Start()
    {
        NestClaimManager.Instance.AddClaimable(claimPosition);
    }
}
