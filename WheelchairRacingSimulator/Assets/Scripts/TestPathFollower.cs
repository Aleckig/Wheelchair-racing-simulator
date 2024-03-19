using UnityEngine;
using PathCreation;

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
        distanceTravelled += speed * Time.deltaTime;

        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

        // Laske kokonaiskuljettu matka
        totalDistanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            speedingUp = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            speedingUp = false;
        }

        if (speedingUp)
        {
            speed += 0.3f;
        }
        else
        {
            speed -= 0.2f;
            speed = Mathf.Max(speed, 0f);
        }
    }
}
