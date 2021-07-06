 using UnityEngine;

namespace CircleBounce
{
    public class Rotating : MonoBehaviour
    {
        [Header("Rotating")]
        public float rotateSpeed;


        void Update()
        {
            Rotate();
        }


        void Rotate()
        {
            transform.Rotate(0, 0, rotateSpeed * 100 * Time.deltaTime);
        }

    }
}