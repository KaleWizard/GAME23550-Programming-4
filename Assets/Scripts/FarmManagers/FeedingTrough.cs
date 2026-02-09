using UnityEngine;

public class FeedingTrough : MonoBehaviour
{
    [SerializeField] Transform eatingPositionHolder;
    void Start()
    {
        int positionCount = eatingPositionHolder.childCount;
        for (int i = 0; i < positionCount; i++)
        {
            TroughClaimManager.Instance.AddClaimable(eatingPositionHolder.GetChild(i));
        }
    }
}
