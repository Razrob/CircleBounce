using System.Collections; 
using UnityEngine;

namespace CircleBounce
{
    public class CircleController : MonoBehaviour
    {
        private Vector2 pos;
        private GameManager gameManager;
        private Rigidbody2D rigidbody2d;
        private int bounceCount = 0;

        [SerializeField] private float speed = 0;

        public LineRenderer line;

        private bool isActive = true;
        private Touch touch;
        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            rigidbody2d = GetComponent<Rigidbody2D>();
            line.startColor = GetComponent<SpriteRenderer>().color;
        }


        private void OnMouseDown()
        {
            if (!isActive) return;

            pos = (Vector2)gameObject.transform.position;
        }

        private void OnMouseDrag()
        {
            if (!isActive) return;

            Vector2 position = ((Vector2)transform.position * 2 - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)) - (Vector2)transform.position;
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)transform.position + position.normalized, position);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit2d.point);

        }

        IEnumerator timeScaleUpper(float time)
        {
            yield return new WaitForSeconds(time);
            if (Time.timeScale * 1.5f <= 100)
            {
                Time.timeScale *= 1.5f;
                StartCoroutine(timeScaleUpper(time * 1.5f));
            }
        }

        private void OnMouseUp()
        {
            if (!isActive) return;

            line.SetPositions(new Vector3[2]);
            //if (Input.touchCount > 0) touch = Input.GetTouch(0);
            //pos = -((Vector2)Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0)) - pos);
            pos = -((Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - pos);
            rigidbody2d.AddForce(pos.normalized * 100f * speed);
            isActive = false;
            pos = Vector2.zero;
            StartCoroutine(timeScaleUpper(10));

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            string colTag = collision.gameObject.tag;
            if (colTag == "Obstacle") gameManager.ObstacleCollision();
            else if (colTag == "Finish") gameManager.levelFinish();
            else
            {
                gameManager.collisionCountChange();
                if (Values.requireBounceCount >= 0)
                {
                    bounceCount++;
                    if (Values.requireBounceCount <= bounceCount)
                    {
                        rigidbody2d.velocity = Vector2.zero;
                        bounceCount = 0;
                        isActive = true;
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Obstacle") gameManager.ObstacleCollision();
        }
    }
}