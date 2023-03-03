using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectToBeSpawned;
    private GameObject spawnedObject = null;

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
            if (canSpawn && spawnedObject == null)
            {
                spawnedObject = Instantiate(
                    gameObjectToBeSpawned,
                    markerObject.transform.position,
                    Quaternion.identity
                );
            }
        }
    
    }
}
