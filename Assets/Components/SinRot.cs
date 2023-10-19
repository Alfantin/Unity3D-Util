using System.Collections;
using UnityEngine;

namespace SpringRun {

    public class SinRot : MonoBehaviour {

        public float minSpeed = 10;
        public float maxSpeed = 20;

        public Vector3 minRot = Vector3.zero;
        public Vector3 maxRot = Vector3.right * 90f;

        public float wait = 0f;
        public bool randomize;

        private IEnumerator Start() {

            var rotA = Quaternion.Euler(minRot);
            var rotB = Quaternion.Euler(maxRot);
            var speed = Random.Range(minSpeed, maxSpeed);

            if (randomize) {
                transform.rotation = Quaternion.Lerp(rotA, rotB, Random.value);
            }

            var wait = new WaitForSeconds(this.wait);
            while (true) {

                yield return transform.RotateTowards(rotA, speed);
                yield return wait;

                yield return transform.RotateTowards(rotB, speed);
                yield return wait;

            }

        }

    }

}