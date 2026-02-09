using UnityEngine;

public class ChickenProperties : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 0;

    public Transform claimedTransform = null;

    [Header("Health")]
    public float satiety;
    public float energy;

    const float minPropertyValue = 50f;
    const float maxPropertyValue = 90f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        satiety = Random.Range(minPropertyValue, maxPropertyValue);
        energy = Random.Range(minPropertyValue, maxPropertyValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
