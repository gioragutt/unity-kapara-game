using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float forwardForce;
    public float slideForce;
    public ForceMode slideForceMode = ForceMode.VelocityChange;

    public KeyCode leftMovementKey = KeyCode.A;
    public KeyCode rightMovementKey = KeyCode.D;

    private void FixedUpdate()
    {
        rigidBody.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (Input.GetKey(leftMovementKey))
        {
            ApplySideForce(-1);
        }

        if (Input.GetKey(rightMovementKey))
        {
            ApplySideForce(1);
        }
    }

    private void ApplySideForce(float forceModifier)
    {
        float force = forceModifier * slideForce * Time.deltaTime;
        rigidBody.AddForce(force, 0, 0, slideForceMode);
    }
}
