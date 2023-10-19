using System.Collections;
using UnityEngine;

namespace SpringRun {

    public class SinPos : MonoBehaviour {

        public Vector3 direction;
        public float minSpeed = 10;
        public float maxSpeed = 20;
        public float wait = 0f;
        public bool randomize;

        private IEnumerator Start() {

            var pos = transform.position;
            var posA = pos - transform.TransformDirection(direction);
            var posB = pos + transform.TransformDirection(direction);
            var speed = Random.Range(minSpeed, maxSpeed);

            if (randomize) {
                transform.position = Vector3.Lerp(posA, posB, Random.value);
            }

            var wait = new WaitForSeconds(this.wait);
            while (true) {

                yield return transform.MoveTowards(posA, speed);
                yield return wait;

                yield return transform.MoveTowards(posB, speed);
                yield return wait;

            }

        }

    }

}