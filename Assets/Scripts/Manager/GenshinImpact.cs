using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using UnityEngine;
using System.Threading.Tasks;

namespace codingchildren
{
    public class GenshinImpact : MonoBehaviour
    {
        private Animation animation;
        private Camera camera;
        [SerializeField]
        private Transform target;
        [SerializeField]
        private Material skybox;

        private bool isTriggered;
        public void Setter()
        {
            //스카이 박스 수정
            RenderSettings.skybox = skybox;
            //카메라 시작 세팅
            Camera cam = Camera.main;
            cam.transform.position = Vector3.zero;
            cam.transform.rotation = Quaternion.Euler(-50f, -173f, -5f);
        }
        [ContextMenu("이히히")]
        public async void Trigger()
        {
            animation.Play();
            await Task.Delay(5000);
            // Code to execute after 5 seconds of delay


        }
        private void Awake()
        {
            animation = this.GetComponent<Animation>();
            camera = Camera.main;
        }
        private void Update()
        {
            camera.transform.LookAt(target);
        }
    }
}
