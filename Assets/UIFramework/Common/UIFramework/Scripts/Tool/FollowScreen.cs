using UnityEngine;

namespace Lenovo.Template.Tool
{
    public enum PivotAxis
    {
        // Rotate about all axes.
        Free,
        // Rotate about an individual axis.
        Y
    }

    public class FollowScreen : MonoBehaviour
    {
        [Header("Billboard")]

        /// <summary>
        /// The axis about which the object will rotate.
        /// </summary>
        [Tooltip("Specifies the axis about which the object will rotate.")]
        public PivotAxis PivotAxis = PivotAxis.Free;

        [Tooltip("Follow camera.")]
        public Camera FollowCamera;

        [Tooltip("Follow distance.")]
        public float DistanceToCamera = 1.5f;

        [Header("Tagalong")]

        [Tooltip("Critical offset area.")]
        public float OffsetX = 1.0f;
        public float OffsetY = 1.0f;
        public float OffsetZ = 1.0f;

        [Tooltip("How fast the object will move to the target position.")]
        public float MoveSpeed = 1.0f;

        /// <summary>
        /// When moving, use unscaled time. This is useful for games that have a pause mechanism or otherwise adjust the game timescale.
        /// </summary>
        [SerializeField]
        [Tooltip("When moving, use unscaled time. This is useful for games that have a pause mechanism or otherwise adjust the game timescale.")]
        private bool useUnscaledTime = true;

        /// <summary>
        /// Used to initialize the initial position of the SphereBasedTagalong before being hidden on LateUpdate.
        /// </summary>
        [SerializeField]
        [Tooltip("Used to initialize the initial position of the SphereBasedTagalong before being hidden on LateUpdate.")]
        private bool hideOnStart;

        [SerializeField]
        [Tooltip("Display a small cube where the cursor position is.")]
        private bool debugDisplayCursorPosition;

        private Vector3 destinationPosition;//面板要移动的目的地位置
        private Vector3 cursorPosition;// 光标的实时位置


        private void OnEnable()
        {
            if (FollowCamera == null)
            {
                FollowCamera = CameraTool.EventSystemCamera;
            }

            this.transform.position = FollowCamera.transform.position + FollowCamera.transform.forward * DistanceToCamera;
            //Update();
        }
        private void Start()
        {
            //GetComponent<Canvas>().worldCamera = FollowCamera;
        }

        private void Update()
        {
            UpdateBillboard();

            UpdateTagalong();
        }

        /// <summary>
        /// Keeps the object facing the camera.
        /// </summary>
        private void UpdateBillboard()
        {
            if (FollowCamera.transform == null)
            {
                return;
            }

            // Get a Vector that points from the target to the main camera.
            Vector3 directionToTarget = FollowCamera.transform.position - transform.position;

            // Adjust for the pivot axis.
            switch (PivotAxis)
            {
                case PivotAxis.Y:
                    directionToTarget.y = 0.0f;
                    break;

                case PivotAxis.Free:
                default:
                    // No changes needed.
                    break;
            }

            // If we are right next to the camera the rotation is undefined. 
            if (directionToTarget.sqrMagnitude < 0.001f)
            {
                return;
            }

            // Calculate and apply the rotation required to reorient the object
            transform.rotation = Quaternion.LookRotation(-directionToTarget);
        }

        // Keep the object following movement.
        private void UpdateTagalong()
        {
            cursorPosition = FollowCamera.transform.position + FollowCamera.transform.forward * DistanceToCamera;
            Vector3 offsetDirToWorld = transform.position - cursorPosition;
            Vector3 offsetDirToCamera = transform.InverseTransformDirection(offsetDirToWorld);

            if (Mathf.Abs(offsetDirToCamera.x) / OffsetX > Mathf.Abs(offsetDirToCamera.y) / OffsetY && Mathf.Abs(offsetDirToCamera.x) / OffsetX > Mathf.Abs(offsetDirToCamera.z) / OffsetZ)
            {
                if (Mathf.Abs(offsetDirToCamera.x) > OffsetX)
                {
                    destinationPosition = cursorPosition + offsetDirToWorld.normalized * OffsetX;

                    float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, destinationPosition, MoveSpeed * deltaTime);
                }
            }
            else if (Mathf.Abs(offsetDirToCamera.y) / OffsetY > Mathf.Abs(offsetDirToCamera.x) / OffsetX && Mathf.Abs(offsetDirToCamera.y) / OffsetY > Mathf.Abs(offsetDirToCamera.z) / OffsetZ)
            {
                if (Mathf.Abs(offsetDirToCamera.y) > OffsetY)
                {
                    destinationPosition = cursorPosition + offsetDirToWorld.normalized * OffsetY;

                    float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, destinationPosition, MoveSpeed * deltaTime);
                }

            }
            else if (Mathf.Abs(offsetDirToCamera.z) / OffsetZ >= Mathf.Abs(offsetDirToCamera.x) / OffsetX && Mathf.Abs(offsetDirToCamera.z) / OffsetZ >= Mathf.Abs(offsetDirToCamera.y) / OffsetY)
            {
                if (Mathf.Abs(offsetDirToCamera.z) > OffsetZ)
                {
                    destinationPosition = cursorPosition + offsetDirToWorld.normalized * OffsetZ;

                    float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, destinationPosition, MoveSpeed * deltaTime);
                }
            }
        }

        private void LateUpdate()
        {
            if (hideOnStart)
            {
                hideOnStart = !hideOnStart;
                gameObject.SetActive(false);
            }
        }

        public void ResetTagetTransform()
        {
            Vector3 directionToTarget = FollowCamera.transform.position - transform.position;
            transform.position = FollowCamera.transform.position + FollowCamera.transform.forward * DistanceToCamera;
            transform.rotation = Quaternion.LookRotation(-directionToTarget);
        }

        public void OnDrawGizmos()
        {
            if (Application.isPlaying == false) { return; }

            Color oldColor = Gizmos.color;

            //显示光标的实时位置
            if (debugDisplayCursorPosition)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(cursorPosition, 0.02f);
            }

            Gizmos.color = oldColor;
        }

        void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                transform.position = new Vector3(0, 0, DistanceToCamera);
                FollowCamera.transform.forward = new Vector3(0, 0, 1.0f);
                Debug.Log("tmac FollowScreen cs OnApplicationPause position = " + transform.position);
            }
        }
    }
}
