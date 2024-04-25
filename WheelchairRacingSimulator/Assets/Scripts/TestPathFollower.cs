using UnityEngine;
using PathCreation;
using WheelchairGame;
public class TestPathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public GameManager gameManager; // Reference to the GameManager
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
        
        if (GameManager.Instance != null && GameManager.Instance.IsCountdownFinished)
        {
            

            // Update the position and rotation on the path
            distanceTravelled += speed * speedMultiplier * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

            // Set the last position to the current position for the next frame
            lastPosition = transform.position;
        }
        

     
    }
}
