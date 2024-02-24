using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelchairGame
{
    public class TestMovement : MonoBehaviour
    {
        public float accelerationSpeed = 5f;
        public float decelerationSpeed = 2f;

        private Rigidbody rb;
        private bool accelerating = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                accelerating = true;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                accelerating = false;
            }
        }

        private void FixedUpdate()
        {
            if (accelerating)
            {
                Accelerate();
            }
            else
            {
                Decelerate();
            }
        }

        private void Accelerate()
        {
            rb.AddForce(transform.forward * accelerationSpeed, ForceMode.Acceleration);
        }

        private void Decelerate()
        {
            Vector3 currentVelocity = rb.velocity;
            Vector3 decelerationForce = -currentVelocity.normalized * decelerationSpeed;

            rb.AddForce(decelerationForce, ForceMode.Acceleration);
        }
    }
}
