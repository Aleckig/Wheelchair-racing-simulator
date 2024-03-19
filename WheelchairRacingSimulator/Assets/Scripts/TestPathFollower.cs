using UnityEngine;
using PathCreation;
using System.Diagnostics;

public class TestPathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 0;
    private float distanceTravelled;
    private bool speedingUp = false;
    public float totalDistanceTraveled = 0f;
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the distance moved in this frame
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);

        // Update the position and rotation on the path
        distanceTravelled += speed * Time.deltaTime;

        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

        // Handle input for speeding up
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speedingUp = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            speedingUp = false;
        }

        // Adjust speed based on input
        if (speedingUp)
        {
            speed += 0.3f;
        }
        else
        {
            speed -= 0.2f;
            speed = Mathf.Max(speed, 0f);
        }

        // If there's actual movement, update the total distance traveled
        if (speed != 0)
        {
            // Update the total distance traveled
            totalDistanceTraveled += distanceThisFrame;
        }

        // Set the last position to the current position for the next frame
        lastPosition = transform.position;

     
    }
}