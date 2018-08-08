using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public bool isSmoothFollow = true;
    public float smoothFactor = 10f;

    void FixedUpdate()
    {
        if (isSmoothFollow)
            SmoothFollow();
        else
            NormalFollow();
    }

    private void NormalFollow()
    {
        transform.position = player.position + offset;
    }

    private void SmoothFollow()
    {
        var desiredPosition = player.position + offset;
        var position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothFactor);
        position.z = player.position.z + offset.z;
        transform.position = position;
        transform.LookAt(player);
    }
}
