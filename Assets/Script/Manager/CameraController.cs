using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineConfiner cinemachineConfiner;
    public CinemachineVirtualCamera virtualCamera;
    public GameObject spawnObject;
    [SerializeField] GameObject confiderShape;

    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineConfiner = GetComponent<CinemachineConfiner>();
    }

    private void Start()
    {
        cinemachineConfiner.m_BoundingShape2D = confiderShape.GetComponent<PolygonCollider2D>();
        spawnObject = FindObjectOfType<PlayerController>().SpawnPlayer();
        virtualCamera.Follow = spawnObject.transform;
    }
}
