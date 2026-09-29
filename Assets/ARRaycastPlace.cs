/*rayacsting projects an invisible line from origin point to 
hit items in the scene. In this script all the obhects hit by the ray
will be stored in a list.*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ARRaycastPlace : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject objectToPlace;
    //public Camera arCamera;

    //list that will store all points hit by ray
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            GameObject note = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
            
            TMP_InputField input = note.GetComponentInChildren<TMP_InputField>(true);
            if (input != null)
            {
                StartCoroutine(ActivateNextFrame(input));
            }
        }
        
    }
       IEnumerator ActivateNextFrame(TMP_InputField input)
    {
        // Wait a frame so the new object's Start/OnEnable have run
        // and the touch that placed it has finished being processed.
        yield return null;

        input.Select();
        input.ActivateInputField();
    }
}
