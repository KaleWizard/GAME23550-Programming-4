using UnityEngine;

[System.Serializable]
public class Signifier
{
    public enum SignifierType { Bubble, Sprite }

    [Header("General")]
    public SignifierType type;
    public Sprite sprite;
    [Header("Timing")]
    public float fadeIn;
    public float hold;
    public float fadeOut;
    [Header("Placement")]
    public Transform target;
    public Vector3 position;
    public float rotation;
}
