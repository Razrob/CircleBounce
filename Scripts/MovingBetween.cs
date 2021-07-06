 using UnityEngine;

namespace CircleBounce
{
    public class MovingBetween : MonoBehaviour
    {

        [Header("ForMovingBetween")]
        public Vector2 pos0;
        public Vector2 pos1;
        private Vector2 directionPosition;
        public float moveSpeed;

        void Start()
        {
            transform.position = new Vector3(pos0.x, pos0.y, transform.position.z);
            directionPosition = pos1;
        }

        void Update()
        {
            MoveBetween();
        }


        void MoveBetween()
        {
            if ((Vector2)transform.position == directionPosition)
            {
                if (directionPosition == pos0) directionPosition = pos1;
                else directionPosition = pos0;
            }
            transform.position = Vector2.MoveTowards(transform.position, directionPosition, Time.deltaTime * moveSpeed);
        }
    }
}