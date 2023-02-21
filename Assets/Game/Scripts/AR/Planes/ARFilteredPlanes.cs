using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARFilteredPlanes : MonoBehaviour
{

    
    [SerializeField]
    float minimumPlaneSize;

    [SerializeField]
    Vector2 bigPlaneDimansions;


    public event Action OnHorizontalPlaneFound;

    public event Action OnBigPlaneFound;

    ARPlaneManager arPlaneManager;

    List<ARPlane> arPlanes;

    private void OnEnable()
    {
        arPlanes = new List<ARPlane>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
        arPlaneManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        arPlaneManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null && args.added.Count > 0)
            arPlanes.AddRange(args.added);

        foreach (ARPlane
            plane
            in
            arPlanes
                .Where(plane =>
                    plane.extents.x * plane.extents.y >= minimumPlaneSize)
        )
        {
            if (plane.alignment.IsHorizontal())
            {
                OnHorizontalPlaneFound.Invoke();
            }

            if (
                plane.extents.x * plane.extents.y >=
                bigPlaneDimansions.x * bigPlaneDimansions.y
            )
            {
                OnBigPlaneFound.Invoke();
            }
        }
    }
}
