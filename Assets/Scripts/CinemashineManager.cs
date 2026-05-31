using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemashineManager : MonoBehaviour
{
    [SerializeField] private float impulseForce = 1f;
    public static CinemashineManager Instance { get; private set; }
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(impulseForce);
    }
}
