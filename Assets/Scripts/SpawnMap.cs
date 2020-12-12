using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.Experimental.XR

using UnityEngine.XR.ARSubsystems;
using System;

public class SpawnMap : MonoBehaviour
{
    public GameObject placementIndicator;
    //private ARSessionOrigin arOrigin;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    public GameObject map;

    void Start()
    {
        //arOrigin = FindObjectOfType<ARSessionOrigin>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        if(placementPoseIsValid && Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    void PlaceObject()
    {
        Instantiate(map, PlacementPose.position, PlacementPose.rotation);
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid || true)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
		{
            placementIndicator.SetActive(false);
		}
	}

    private void UpdatePlacementPose()
	{
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid || true)
		{
            PlacementPose = hits[0].pose;
		}
	}
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR.ARFoundation;
// using UnityEngine.XR.ARSubsystems;

// [RequireComponent(typeof(ARRaycastManager))]
// public class SpawnMap : MonoBehaviour
// {


//     public GameObject map;
//     private GameObject spawnedMap;
//     private ARRaycastManager _arRaycastManager;
//     private Vector2 touchPos;

//     static List<ARRaycastHit> hits = new List<ARRaycastHit>();

//     void Awake()
//     {
//         _arRaycastManager = gameObject.GetComponent<ARRaycastManager>();
//     }

//     bool GetTouchPosition(out Vector2 touchPos)
//     {
//         if (Input.touchCount > 0)
//         {   
//             touchPos = Input.GetTouch(0).position;
//             return true;
//         }
//         touchPos = default;
//         return false;
//     }
//     // Start is called before the first frame update
//     void Start()
//     {

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(!GetTouchPosition(out Vector2 touchPos))
//         {
//             return;
//         }
//         if(_arRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
//         {
//             var hitPose = hits[0].pose;
//             if(spawnedMap = null)
//             {
//                 spawnedMap = Instantiate(map, hitPose.position, hitPose.rotation);
//                 print("map SPAWNED");
//             }
//             else
//             {
//                 spawnedMap.transform.position = hitPose.position;
//             }
//         }
//     }
// }
