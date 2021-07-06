using UnityEngine;

namespace CircleBounces
{
    public class LevelPartTransition : MonoBehaviour
    {
        public Vector2 newCirclePosition;
        public Vector2 newCamPosition;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject circle = collision.gameObject;
            if (circle.tag == "Player")
            {
                circle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                circle.transform.position = newCirclePosition;
                Camera.main.transform.position = newCamPosition;
            }
        }
    }
}