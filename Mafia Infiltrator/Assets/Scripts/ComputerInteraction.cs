using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    public CameraController cameraController;

    public void Interact()
    {
        // Disable the camera
        cameraController.DisableCamera();
        Debug.Log("camera Disabled");
    }
}
