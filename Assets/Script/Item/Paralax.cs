using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] Material[] materials;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        if (materials.Length > 0)
        {
            int randomIndex = Random.Range(0, materials.Length);
            meshRenderer.material = materials[randomIndex];
        }
    }

    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(xOffset * Time.deltaTime, yOffset * Time.deltaTime);

    }
}
