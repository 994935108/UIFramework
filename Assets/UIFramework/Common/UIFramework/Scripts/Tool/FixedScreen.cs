using UnityEngine;


namespace Lenovo.Template.Tool
{
    /// <summary>
    /// 固定屏幕
    /// </summary>
    public class FixedScreen : MonoBehaviour
    {
        public Camera FixedCamera;

        public float DistanceToCamera = 1.5f;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            UpdateUITrans();
        }

        private void OnEnable()
        {
            if (FixedCamera == null)
            {
                FixedCamera = CameraTool.Main;
            }

            FixedUpdate();
        }

        void UpdateUITrans()
        {
            this.transform.position = FixedCamera.transform.position + FixedCamera.transform.forward * DistanceToCamera;
            transform.rotation = FixedCamera.transform.rotation;
        }

    }

}