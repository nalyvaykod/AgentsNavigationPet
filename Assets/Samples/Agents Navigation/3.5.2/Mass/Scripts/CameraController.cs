using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera move variables
    public float moveSpeed;
    public float moveTime;
    public Vector3 newPos;


    void Start()
    {
        newPos = transform.position;
    }

    void Update()
    {
        HandleMoveInput();
    }

    void HandleMoveInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPos += (transform.right * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPos += (transform.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPos += (transform.forward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPos += (transform.forward * -moveSpeed);
        }



        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * moveTime);
    }


    /*void ZoomInput()
    {

    }*/

}
