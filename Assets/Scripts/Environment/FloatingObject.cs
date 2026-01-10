using UnityEngine;
using System.Collections.Generic;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] List<Mesh> meshes;

    [SerializeField] float posVariance = 3f;
    [SerializeField] float speed = 5f;
    [SerializeField] float minCameraDistance = 10f;
    [SerializeField] float displacement = 8f;

    [SerializeField] float scaleVariance = 0.5f;

    private Camera targetCam;
    private float targetPosY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetCam = Camera.main;
        targetPosY = transform.position.y + Random.Range(-posVariance, posVariance);

        Vector3 pos = transform.position;
        pos.y = targetPosY;
        transform.position = pos;

        if (meshes.Count > 0)
        {
            MeshFilter filter = GetComponent<MeshFilter>();
            filter.mesh = meshes[Random.Range(0, meshes.Count)];
        }

        transform.localScale = Vector3.one * Random.Range(1 - scaleVariance, 1 + scaleVariance);

        transform.localEulerAngles = new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));

        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetColor("_BaseColor", Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1));
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.SetPropertyBlock(propertyBlock);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        float targetThisFrame = targetPosY;
        if (Vector3.Distance(targetCam.transform.position, transform.position) < minCameraDistance)
            targetThisFrame += displacement;

        pos.y = Mathf.Lerp(pos.y, targetThisFrame, speed * Time.deltaTime);

        transform.position = pos;
    }
}
