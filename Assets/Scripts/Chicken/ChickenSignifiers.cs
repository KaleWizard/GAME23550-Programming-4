using UnityEngine;

public class ChickenSignifiers : MonoBehaviour
{
    [Header("Prefabs")]
    // Signifier prefabs
    [SerializeField] SpriteSignifier spriteSigPrefab;
    [SerializeField] BubbleSignifier bubbleSigPrefab;

    [Header("Signifiers")]
    // Bubble Signifiers
    [SerializeField] Signifier eggLayingSignifier;
    [SerializeField] Signifier sleepySignifier;

    // Sprite Signifiers
    [SerializeField] Signifier fleeSignifier;
    [SerializeField] Signifier hungerSignifier;
    [SerializeField] Signifier cowerSignifier;

    // Generic signifier method for each signifier
    public void SignifyEggLaying() => SpawnSignifier(eggLayingSignifier);
    public void SignifySleepy() => SpawnSignifier(sleepySignifier);
    public void SignifyFlee() => SpawnSignifier(fleeSignifier);
    public void SignifyHunger() => SpawnSignifier(hungerSignifier);
    public void SignifyCower() => SpawnSignifier(cowerSignifier);



    private void SpawnSignifier(Signifier signifier)
    {
        if (signifier.type == Signifier.SignifierType.Bubble)
            BubbleSignifier(signifier);
        else if (signifier.type == Signifier.SignifierType.Sprite)
            SpriteSignifier(signifier);
    }

    private void BubbleSignifier(Signifier signifier)
    {
        BubbleSignifier newObj = Instantiate(bubbleSigPrefab);
        newObj.Init(signifier);
    }

    private void SpriteSignifier(Signifier signifier)
    {
        SpriteSignifier newObj = Instantiate(spriteSigPrefab);
        newObj.Init(signifier);
    }
}
