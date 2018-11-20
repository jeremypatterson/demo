using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARSessionOrigin))]
public class PlacementManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    [SerializeField]
    [Tooltip("Instantiate for the placement anchor visual.")]
    GameObject m_MarkerPrefab;

    /// <summary>
    /// The prefab for the placement anchor visual.
    /// </summary>
    public GameObject markerPrefab
    {
        get { return m_MarkerPrefab; }
        set { m_MarkerPrefab = value; }
    }


    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    /// <summary>
    /// The object instantiated to track placement on detected planes.
    /// </summary>
    private GameObject spawnedMarker;

    ARSessionOrigin m_SessionOrigin;
    
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    
    void Awake()
    {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (spawnedMarker != null && spawnedMarker.activeSelf) {
                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, spawnedMarker.transform.position, spawnedMarker.transform.rotation);
                }
                else {
                    spawnedObject.transform.position = spawnedMarker.transform.position;
                    spawnedObject.transform.rotation = spawnedMarker.transform.rotation;
                }
            }
        }



        if (m_SessionOrigin.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), s_Hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = s_Hits[0].pose;

            // create a tracker the first time we need it
            if (spawnedMarker == null)
            {
                spawnedMarker = Instantiate(m_MarkerPrefab, hitPose.position, hitPose.rotation);
            }

            else
            {
                // avoid extra awake calls
                if (!spawnedMarker.activeSelf) {
                    spawnedMarker.SetActive(true);
                }

                spawnedMarker.transform.position = hitPose.position;
                spawnedMarker.transform.rotation = hitPose.rotation;
            }
        }

        else {
            spawnedMarker.SetActive(false);
        }
    }
}
