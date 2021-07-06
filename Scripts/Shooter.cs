using System.Collections; 
using UnityEngine;

namespace CircleBounce
{
    public class Shooter : MonoBehaviour
    {

        [Header("ForShooting")]
        public GameObject bulletPrefab;
        public float shootForce;
        public float shootDelay;
        public float timeOffset;
        public float bulletLifeTime;

        void Start()
        {
            StartCoroutine(Shooting());
        }

        IEnumerator Shooting()
        {
            if (timeOffset > 0f)
            {
                yield return new WaitForSeconds(timeOffset);
                timeOffset = 0;
            }

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * 100 * shootForce);
            yield return new WaitForSeconds(shootDelay);
            StartCoroutine(Shooting());
            StartCoroutine(bulletDelete(bullet));
        }

        IEnumerator bulletDelete(GameObject bullet)
        {
            yield return new WaitForSeconds(bulletLifeTime);
            Destroy(bullet);
        }
    }
}