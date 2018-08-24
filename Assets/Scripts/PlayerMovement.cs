using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody rigidBody;
        public float forwardForce = 4000f;
        public float sidewaysForce = 120f;
        public ForceMode sidewaysForceMode = ForceMode.VelocityChange;

        [HideInInspector]
        public bool IsMovingLeft
        {
            get; set;
        }

        [HideInInspector]
        public bool IsMovingRight
        {
            get; set;
        }

        private void FixedUpdate()
        {
            rigidBody.AddForce(0, 0, forwardForce * Time.deltaTime);
            if (IsMovingLeft)
            {
                ApplySidewaysForce(-1f);
            }
            if (IsMovingRight)
            {
                ApplySidewaysForce(1f);
            }
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
}