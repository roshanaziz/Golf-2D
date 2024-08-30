using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public LineRenderer line;
    public Rigidbody2D rb;
    public float dragLimit = 3f;
    public float forceToAdd = 3f;
    private int draggingFingerId = -1;

    private Camera cam;
    private bool isDragging;
    public float stationaryThreshold = 1f;


    Vector3 TouchPosition
    {
        get
        {
            if (draggingFingerId != -1)
            {
                Vector3 pos = cam.ScreenToWorldPoint(Input.GetTouch(draggingFingerId).position);
                pos.z = 0f;
                return pos;
            }
            return Vector3.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
     cam = Camera.main;
     line.positionCount = 2;   
     line.SetPosition(0, Vector2.zero);
     line.SetPosition(1,Vector2.zero);
     line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0){
            foreach (Touch touch in Input.touches){
                Debug.Log("Touch");
                if (IsStationary() && touch.phase == TouchPhase.Began && !isDragging)
                {
                    Debug.Log("Touch Dragging Started!");
                    draggingFingerId = touch.fingerId;
                    DragStart();
                }
                if (isDragging && touch.fingerId == draggingFingerId)
                {
                    Debug.Log("Touch Dragging");
                    Drag();
                }
                if (touch.phase == TouchPhase.Ended && isDragging && touch.fingerId == draggingFingerId)
                {
                    Debug.Log("Touch Drag Ended");
                    DragEnd();
                    draggingFingerId = -1;
                }
            }
        }
    }

    bool IsStationary()
    {
        Debug.Log(rb.velocity.magnitude < stationaryThreshold);
        // Check if the ball's velocity is below the stationary threshold
        return rb.velocity.magnitude < stationaryThreshold;
    }

    void DragStart()
    {
        line.enabled = true;
        isDragging = true;
        line.SetPosition(0,TouchPosition);
        Debug.Log("Drag Start Working");
    }

    void Drag()
    {
        Vector3 startPos = line.GetPosition(0);
        Vector3 currentPos = TouchPosition;

        Vector3 distance = currentPos - startPos;

        if (distance.magnitude <= dragLimit){
            line.SetPosition(1,currentPos);
        }else{
            Vector3 limitVector = startPos + (distance.normalized * dragLimit);
            line.SetPosition(1,limitVector);
        }
        Debug.Log("Drag Working");
    }

    private void DragEnd()
    {
        isDragging = false;
        line.enabled = false;

        Vector3 startPos = line.GetPosition(0);
        Vector3 currentPos = line.GetPosition(1);

        Vector3 distance = currentPos - startPos;
        Vector3 finalForce = distance * forceToAdd;

        rb.AddForce(-finalForce, ForceMode2D.Impulse);
        Debug.Log("Drag End Working");
    }
}
