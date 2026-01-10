using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public float killRange = 10f;
    public float timeToKill = 1;
    [Space]
    public Material seekingMaterial;
    public Material fleeingMaterial;
    [Space]
    public float seekingTime = 5f;
    public float fleeingTime = 3f;

    [SerializeField, Space]
    Transform killIndicator;

    private void Start()
    {
        killIndicator.localScale = new Vector3(killRange * 2, 0.28f, killRange * 2);
    }
}
