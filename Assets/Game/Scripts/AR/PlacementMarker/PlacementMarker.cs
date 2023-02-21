using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementMarker : MonoBehaviour
{
    ARRaycastManager rayManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    GameObject marker;

    private void Awake()
    {
        marker = transform.GetChild(0).gameObject;
        rayManager = FindObjectOfType<ARRaycastManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        marker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlaceMarker();
    }

    private void PlaceMarker()
    {
        Vector2 screenPoint = new Vector2(Screen.width / 2, Screen.height / 2);

        if (rayManager.Raycast(screenPoint, hits, TrackableType.PlaneWithinPolygon))
        {
            if (hits.Count > 0)
            {
                Pose hitTransform = hits[0].pose;

                marker.SetActive(true);

                marker.transform.position = hitTransform.position;
                marker.transform.rotation = hitTransform.rotation;

                SpawnManager spawnManager = FindObjectOfType<SpawnManager>();

                spawnManager.CanSpawn = true;
                spawnManager.MarkerObject = marker.gameObject;
            }
        }
    }
}
