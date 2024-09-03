using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace codingchildren
{
    public class GenshinImpact : MonoBehaviour
    {
        #region props
        private Transform camera;
        private ParticleSystem particle;
        private Light light;
        private Transform target;
        private Transform keyframe;
        private Transform keyframe2;
        private Vector3 dest;
        private Vector3 dest2;
        #endregion

        #region Init & Reset
        private Vector3 initCamPos;
        private Vector3 initCamRot;

        private Vector3 initTargetPos;
        private Vector3 initTargetRot;
        private void Init()
        {
            target = transform.GetChild(0);
            camera = transform.GetChild(1);
            keyframe = transform.GetChild(2);
            keyframe2 = transform.GetChild(3);
            light = transform.GetChild(0).GetComponentInChildren<Light>();
            particle = transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
            particle.Stop();
            initCamPos = camera.transform.position;
            initCamRot = camera.transform.eulerAngles;

            initTargetPos = target.position;
            initTargetRot = target.eulerAngles;
            dest = keyframe.position - target.position;
            dest2 = keyframe2.position - keyframe.position;
        }
        private void Reset()
        {
            camera.transform.position = initCamPos;
            camera.transform.eulerAngles = initCamRot;

            target.position = initTargetPos;
            target.eulerAngles = initTargetRot;
            particle.Stop();
            isTriggered = false;
            isEntered = false;
        }
        #endregion

        #region Resource

        [SerializeField]
        private Material skybox;
        #endregion

        [SerializeField]
        private bool isTriggered = false;
        [SerializeField]
        private bool isEntered = false;
        [ContextMenu("이히히")]
        public void Trigger()
        {
            particle.Play();
            isTriggered = true;
        }
        private void Awake()
        {
            Init();
            Reset();
        }
        private void Update()
        {
            if (Vector3.Distance(keyframe.position, target.position) < 0.2f)
            {
                target.LookAt(keyframe2);
                if (!isEntered)
                {
                    StartCoroutine(LightOnCoroutine());
                }
                isEntered = true;
            }
            if (isEntered)
            {
                camera.LookAt(target);
                target.position += target.forward * 30 * Time.deltaTime;
                return;
            }
            if (isTriggered)
            {
                target.position += dest.normalized * 100 * Time.deltaTime;
            }
            return;
        }
        private IEnumerator LightOnCoroutine()
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                light.intensity = Mathf.Lerp(5, 30, t);
                light.range = Mathf.Lerp(10, 30, t);
                yield return null;
            }
        }
    }
}
