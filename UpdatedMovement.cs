using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float rotationSpeed = 200f;
    private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;
    private float cameraPitch = 0f;
    [SerializeField] private float lookSpeed = 20f;
    

    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position; // gameObject.transform.position
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move *= moveSpeed;

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        if (Input.GetMouseButton(1)) // right mouse button click; this is for rotating the player's view left/right
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * mouseX);
        }

        if (Input.GetMouseButton(0)) // left mouse button click to pan/tilt camera up and down
        {
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;

            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80); // prevent flipping

            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    
        }

        if (transform.position.y < -1f)
        {
            ResetPlayer();
        }
        
    }

    void ResetPlayer()
    {
        transform.SetPositionAndRotation(startingPosition, Quaternion.identity);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }
}
