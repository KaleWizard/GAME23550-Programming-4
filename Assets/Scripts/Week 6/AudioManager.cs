using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public void PlaySoundEffect(AudioClip clip, AudioSource source = null)
    {
        if (source)
        {
            source.PlayOneShot(clip);
        } else
        {
            source = gameObject.AddComponent<AudioSource>();
            source.PlayOneShot(clip);
            StartCoroutine(RemoveAudioSourceComponent(source));
        }
    }

    private IEnumerator RemoveAudioSourceComponent(AudioSource source)
    {
        while (source.isPlaying)
            yield return null;
        Destroy(source);
    }
}
