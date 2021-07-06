 using UnityEngine;

namespace CircleBounce
{
    public class Portal : MonoBehaviour
    {
        public enum PortalType { input, output }
        public GameObject connectedPortal;
        public PortalType portalType;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (portalType == PortalType.input && collision.gameObject.tag == "Player")
            {
                Rigidbody2D rb2D = collision.gameObject.GetComponent<Rigidbody2D>();
                float angle = -Vector2.SignedAngle(rb2D.velocity, Vector2.up);
                collision.transform.position = connectedPortal.transform.position;
                Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * connectedPortal.transform.right;

                velocity = Quaternion.Euler(new Vector3(0, 0, angle)) * velocity;
                rb2D.velocity = velocity;
            }
        }
    }
}