using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    public Transform[] wheels; // Array to hold references to the wheel transforms
    public float[] wheelDiameters = new float[3] { 24f, 24f, 20f }; // Default wheel diameters in inches
    public float speedMultiplier = 3.6f; // Speed multiplier to convert speed from m/s to km/h
    public SpeedUpdater speedUpdater;
    // Update is called once per frame
    void Update()
    {
        // Iterate through each wheel
        for (int i = 0; i < wheels.Length; i++)
        {
            // Calculate rotation speed based on wheel diameter and speed multiplier
            float rotationSpeed = (speedUpdater.speed / (Mathf.PI * wheelDiameters[i])) * speedMultiplier;

            // Rotate the wheel
            wheels[i].Rotate(Vector3.back, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
