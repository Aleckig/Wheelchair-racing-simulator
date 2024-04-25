using UnityEngine;
using PathCreation;
using WheelchairGame;
public class TestPathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public GameManager gameManager; // Reference to the GameManager
    public SpeedMultiplier speedMultiplier;

    public float speed = 0;
    private float distanceTravelled;
    private Vector3 lastPosition;
    public SpeedUpdater speedUpdater;
    public float speedM = 3.6f;

    private void Start()
    {
        lastPosition = transform.position;
        GameObject gameObject = GameObject.FindGameObjectWithTag("Slider");
        speedMultiplier = gameObject.GetComponent<SpeedMultiplier>();
    }

    private void Update()
    {
        
        if (GameManager.Instance != null && GameManager.Instance.IsCountdownFinished)
        {

            speedM = speedMultiplier.speedMultiplier;
            // Update the position and rotation on the path
            distanceTravelled += speed * speedM * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

            // Set the last position to the current position for the next frame
            lastPosition = transform.position;
        }
        

     
    }
}
