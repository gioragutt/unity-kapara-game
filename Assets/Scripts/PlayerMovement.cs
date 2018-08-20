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
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetKey(leftMovementKey))
        {
            ApplySidewaysForce(-1);
        }

        if (Input.GetKey(rightMovementKey))
        {
            ApplySidewaysForce(1);
        }
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        var touchForce = DetectTouchForce();
        if (touchForce != 0)
        {
            ApplySidewaysForce(touchForce);
        }
#endif
    }

    private float DetectTouchForce()
    {
        if (Input.touchCount == 0)
            return 0;

        var screenWidth = Screen.width;
        var touchForce = 0f;
        foreach (Touch touch in Input.touches)
            touchForce += TouchDirection(touch, screenWidth);
        return System.Math.Sign(touchForce);
    }

    private static float TouchDirection(Touch touch, int screenWidth)
    {
        return touch.position.x <= screenWidth / 2 ? -1 : 1;
    }

    private void ApplySidewaysForce(float forceModifier)
    {
        float force = forceModifier * sidewaysForce * Time.deltaTime;
        rigidBody.AddForce(force, 0, 0, sidewaysForceMode);
    }

    public void StopPlayer()
    {
        enabled = false;
        rigidBody.AddForce(rigidBody.velocity * -0.5f);
    }
}
