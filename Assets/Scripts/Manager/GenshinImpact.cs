using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace codingchildren
{
    public class GenshinImpact : MonoBehaviour
    {
        #region props
        private Transform camera;
        private Light light1;
        private Light light2;
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
            light1 = transform.GetChild(0).GetChild(0).GetComponent<Light>();
            light2 = transform.GetChild(0).GetChild(1).GetComponent<Light>();
            keyframe = transform.GetChild(2);
            keyframe2 = transform.GetChild(3);
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

            light1.intensity = 5;
            light1.range = 10;
            light2.intensity = 0.001f;
            light2.range = 0.001f;

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
                light1.intensity = 0;
                light1.range = 0;
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
            if (Input.GetKey(KeyCode.Alpha1))
            {
                target.transform.position += dest.normalized * 100 * Time.deltaTime;
                Debug.Log("키 눌림");
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                target.transform.position += dest2.normalized * 100 * Time.deltaTime;
                Debug.Log("키 눌림");
            }
        }
        private void LightOn()
        {
            StartCoroutine(LightOnCoroutine());
        }
        private IEnumerator LightOnCoroutine()
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                light2.intensity = Mathf.Lerp(5, 30, t);
                light2.range = Mathf.Lerp(10, 30, t);
                yield return null;
            }
        }
    }
}
