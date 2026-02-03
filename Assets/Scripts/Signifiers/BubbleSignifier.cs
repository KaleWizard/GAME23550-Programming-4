using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSignifier : MonoBehaviour
{
    [SerializeField] Image iconUI;
    [SerializeField] Transform positionTransform;
    [SerializeField] CanvasGroup bubbleGroup;

    Transform target;

    float fadeIn;
    float holdTime;
    float fadeOut;

    Camera mainCam;

    public void Init(Signifier signifier)
    {
        iconUI.sprite = signifier.sprite;
        positionTransform.localPosition = signifier.position;
        positionTransform.eulerAngles = new Vector3(0,0,signifier.rotation);

        target = signifier.target;

        fadeIn = signifier.fadeIn;
        holdTime = signifier.hold;
        fadeOut = signifier.fadeOut;

        StartCoroutine(FadeInHoldOut());

        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        transform.position = target.position;
        transform.forward = transform.position - mainCam.transform.position;
    }

    IEnumerator FadeInHoldOut()
    {
        float timer = 0;
        while (timer < fadeIn)
        {
            timer += Time.deltaTime;
            bubbleGroup.alpha = timer / fadeIn;
            yield return null;
        }

        yield return new WaitForSeconds(holdTime);

        timer = 0;
        while (timer < fadeOut)
        {
            timer += Time.deltaTime;
            bubbleGroup.alpha = 1 - (timer / fadeOut);
            yield return null;
        }

        Destroy(gameObject);
    }
}
