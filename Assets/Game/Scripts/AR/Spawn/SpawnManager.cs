using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectToBeSpawned;

    bool canSpawn = false;

    private GameObject markerObject;

    public bool CanSpawn
    {
        set { canSpawn = value; }
    }
    public GameObject MarkerObject
    {
        set { markerObject = value; }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        SpawnObject();
    }

    private void SpawnObject()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            if (canSpawn)
            {
                var spawnedObject = Instantiate(
                    gameObjectToBeSpawned,
                    markerObject.transform.position,
                    Quaternion.identity
                );
            }
        }
    }
}
