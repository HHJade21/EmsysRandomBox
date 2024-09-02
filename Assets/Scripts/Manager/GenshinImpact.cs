using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace codingchildren
{
    public class GenshinImpact : MonoBehaviour
    {
        #region props
        private Transform camera;
        private Camera cam1;
        private Camera cam2;
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
            cam1 = camera.GetComponent<Camera>();
            cam2 = transform.GetChild(0).GetComponentInChildren<Camera>();
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
                isEntered = true;
                target.LookAt(keyframe2);
                Invoke("CameraSwitch", 2f);
            }
            if (isEntered)
            {
                camera.LookAt(target);
                target.position += target.forward * 50 * Time.deltaTime;
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
        private void CameraSwitch()
        {
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
        }
    }
}
