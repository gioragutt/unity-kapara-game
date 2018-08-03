using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float forwardForce = 4000f;
    public float sidewaysForce = 120f;
    public ForceMode sidewaysForceMode = ForceMode.VelocityChange;

    public KeyCode leftMovementKey = KeyCode.A;
    public KeyCode rightMovementKey = KeyCode.D;

    private void FixedUpdate()
    {
        rigidBody.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (Input.GetKey(leftMovementKey))
        {
            ApplySidewaysForce(-1);
        }

        if (Input.GetKey(rightMovementKey))
        {
            ApplySidewaysForce(1);
        }
    }

    private void ApplySidewaysForce(float forceModifier)
    {
        float force = forceModifier * sidewaysForce * Time.deltaTime;
        rigidBody.AddForce(force, 0, 0, sidewaysForceMode);
    }
}
