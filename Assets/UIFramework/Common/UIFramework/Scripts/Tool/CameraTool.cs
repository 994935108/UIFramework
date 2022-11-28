using UnityEngine;

public class CameraTool : MonoBehaviour
{
    private static Camera mainCamera;
    private static Camera uiRaycastCamera;
   
    public static Camera Main
    {
        get
        {
            if (mainCamera != null)
            {
                return mainCamera;
            }

            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
                if (cameras.Length == 0)
                {
                    Debug.LogError("The scene  has  none camera");
                    mainCamera = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener)) { tag = "MainCamera" }.GetComponent<Camera>();
                }
                else
                {
                    for (int i = 0; i < cameras.Length; i++)
                    {
                        if (cameras[i].gameObject.tag == "MainCamera" || cameras[i].gameObject.name == "Main Camera")
                        {
                            mainCamera = cameras[i];
                            return mainCamera;
                        }

                        Debug.LogWarning("Plase ensure the scene has a camera whit MainCamera tag");
                        mainCamera = cameras[0];
                        return mainCamera;
                    }

                }
            }
            return mainCamera;
        }
    }

    public static Camera EventSystemCamera
    {
        get
        {
            if (uiRaycastCamera != null)
            {
                return uiRaycastCamera;
            }

            if (uiRaycastCamera == null)
            {
                if (GameObject.Find("EventCamera") != null)
                {
                    uiRaycastCamera = GameObject.Find("EventCamera").GetComponent<Camera>();
                }
            }

            if (uiRaycastCamera == null)
            {
                uiRaycastCamera = Main;
            }

            return uiRaycastCamera;
        }

        set
        {
            uiRaycastCamera = value;
        }
    }

}
