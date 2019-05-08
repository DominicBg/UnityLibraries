using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraActionFollowPerspective : MonoBehaviour {
	
	public List<Transform> targets = new List<Transform>();
	
	public Vector3 startingPos;
	[SerializeField]
	float boundingBoxPadding = 2f, minFieldOfView = 8f, zoomSpeed = 20f, moveSpeed = 1.1f;
	
	public Vector3 CameraAjust;
	
	Camera currentCamera;
    private bool isInit;

    [SerializeField] UnityEvent OnStartFollowEvent = new UnityEvent();
    [SerializeField] UnityEvent OnStopFollowEvent = new UnityEvent();

    void Awake()
	{
		targets.Clear();
		startingPos = transform.position;
		currentCamera = GetComponent<Camera>();
	}
	
	void Update()
	{
        if (isInit)
        {
            Rect boundingBox = CalculateTargetsBoundingBox();

            transform.position = Vector3.Lerp(transform.position, CalculateCameraPosition(boundingBox), moveSpeed * Time.deltaTime);
            currentCamera.fieldOfView = CalculateFieldOfView(boundingBox);
        }
	}

    public void SetTargets(List<Transform> targets)
    {
        this.targets = targets;
        isInit = true;
        //OnStartFollowEvent.Invoke();
    }

    public void ResetTargets()
    { 
        isInit = false;
        targets.Clear();
        //OnStopFollowEvent.Invoke();
    }
	
	/// <summary>
	/// Calculates a bounding box that contains all the targets.
	/// </summary>
	/// <returns>A Rect containing all the targets.</returns>
	Rect CalculateTargetsBoundingBox()
	{
		float minX = Mathf.Infinity;
		float maxX = Mathf.NegativeInfinity;
		float minY = Mathf.Infinity;
		float maxY = Mathf.NegativeInfinity;
		
		foreach (Transform target in targets)
		{
			if(target != null)
			{
				Vector3 position = target.position;
				
				minX = Mathf.Min(minX, position.x);
				minY = Mathf.Min(minY, position.z);
				maxX = Mathf.Max(maxX, position.x);
				maxY = Mathf.Max(maxY, position.z);
			}
		}
		
		return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
	}
	
	/// <summary>
	/// Calculates a camera position given the a bounding box containing all the targets.
	/// </summary>
	/// <param name="boundingBox">A Rect bounding box containg all targets.</param>
	/// <returns>A Vector3 in the center of the bounding box.</returns>
	Vector3 CalculateCameraPosition(Rect boundingBox)
	{
		//Vector3 boundingBoxCenter = boundingBox.center;
		Vector3 boundingBoxCenter = new Vector3(boundingBox.x, 0, boundingBox.y);
		return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, boundingBoxCenter.z) + CameraAjust;
		
	}
	
	/// <summary>
	/// Calculates a new orthographic size for the camera based on the target bounding box.
	/// </summary>
	/// <param name="boundingBox">A Rect bounding box containg all targets.</param>
	/// <returns>A float for the orthographic size.</returns>
	float CalculateFieldOfView(Rect boundingBox)
	{
		float fieldOfView = currentCamera.fieldOfView;
		Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, 0f, boundingBox.y);
		Vector3 topRightAsViewport = currentCamera.WorldToViewportPoint(topRight);
		
		if (topRightAsViewport.x >= topRightAsViewport.z)
			fieldOfView = Mathf.Abs(boundingBox.width) / currentCamera.aspect / 2f;
		else
			fieldOfView = Mathf.Abs(boundingBox.height) / 2f;

		//Debug.Log(fieldOfView);
		return Mathf.Clamp(Mathf.Lerp(currentCamera.fieldOfView, fieldOfView, Time.deltaTime * zoomSpeed), minFieldOfView, Mathf.Infinity);
	}
}
