using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicUI
{
    public class EnemyUIController : BasicUIController
    {
        public Transform cam;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            cam = GameObject.Find("MainCam").transform;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + cam.forward);
        }
    }
}
