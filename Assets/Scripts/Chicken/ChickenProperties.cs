using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.AI;

public class ChickenProperties : MonoBehaviour
{
    public enum HealhProperties { Satiety, Energy, EggTimer }

    [Header("Movement")]
    public float baseMaxSpeed = 7f;
    public float fleeModifier = 2f;
    public float hungerModifier = 0.5f;
    public float tiredModifier = 0.5f;
    [Space]
    public Transform claimedTransform = null;

    [Header("Health")]
    public float satiety;
    public float energy;
    public float eggTimer;
    [Space]
    public float satietyLossRate = 2f;
    public float energyLossRate = 2f;
    [Space]
    public float minSatiety = 15f;
    public float minEnergy = 15f;
    public float maxEggTime = 60f;
    public float minEggTime = 30f;

    [Header("Dog")]
    public float timeToCalm = 8f; // Seconds to exit Flee/Cower state after last dog bark
    public float barkMaxDistance = 20f;

    public bool DogNear => timeSinceBark < timeToCalm;
    private float timeSinceBark = 100;
    private Vector3 lastBarkPosition;

    const float minPropertyValue = 50f;
    const float maxPropertyValue = 90f;

    Blackboard agentBlackboard;
    NavMeshAgent navAgent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        satiety = Random.Range(minPropertyValue, maxPropertyValue);
        energy = Random.Range(minPropertyValue, maxPropertyValue);
        eggTimer = Random.Range(minEggTime, maxEggTime);

        agentBlackboard = GetComponent<Blackboard>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateSpeed();
    }

    void UpdateHealth()
    {
        timeSinceBark += Time.deltaTime;
        eggTimer -= Time.deltaTime;

        // Satiety
        float satietyLoss = satietyLossRate;
        satietyLoss /= (satiety < minSatiety ? hungerModifier : 1);
        satietyLoss /= (energy < minEnergy ? tiredModifier : 1);
        satietyLoss /= (timeSinceBark < timeToCalm ? fleeModifier : 1);
        satiety -= satietyLoss * Time.deltaTime;

        // Energy
        float energyLoss = energyLossRate;
        energyLoss /= (satiety < minSatiety ? hungerModifier : 1);
        energyLoss /= (energy < minEnergy ? tiredModifier : 1);
        energyLoss /= (timeSinceBark < timeToCalm ? fleeModifier : 1);
        energy -= energyLoss * Time.deltaTime;
    }

    void UpdateSpeed()
    {
        float speed = baseMaxSpeed;
        speed *= (satiety < minSatiety ? hungerModifier : 1);
        speed *= (energy < minEnergy ? tiredModifier : 1);
        speed *= (timeSinceBark < timeToCalm ? fleeModifier : 1);

        navAgent.speed = speed;
    }

    void OnDogBark(Transform dog)
    {
        lastBarkPosition = dog.position;
        if (Vector3.Distance(lastBarkPosition, transform.position) < barkMaxDistance)
            timeSinceBark = 0;
    }
}
