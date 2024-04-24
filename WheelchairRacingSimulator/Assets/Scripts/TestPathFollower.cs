using UnityEngine;
using PathCreation;

public class TestPathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 0;
    private float distanceTravelled;
    private Vector3 lastPosition;
    public SpeedUpdater speedUpdater;
    public float speedMultiplier = 3.6f;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        speed = speedUpdater.speed;
        // Calculate the distance moved in this frame
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);

        // Update the position and rotation on the path
        distanceTravelled += speed * speedMultiplier * Time.deltaTime;

        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);


        // Set the last position to the current position for the next frame
        lastPosition = transform.position;

     
    }
}
