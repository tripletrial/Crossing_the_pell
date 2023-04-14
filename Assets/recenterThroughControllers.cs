using UnityEngine;

public class recenterThroughControllers : MonoBehaviour
{
    public GameObject controller1;
    public GameObject controller2;
    public RectTransform canvasElement;

   private void Update()
    {
        // Calculate the midpoint between the two controllers
        Vector3 midpoint = (controller1.transform.position + controller2.transform.position) / 2f;

        // Calculate the direction vector from the midpoint to the canvas element, ignoring the Y axis
        Vector3 direction = canvasElement.position - midpoint;
        direction.y = 0f;

        // Calculate the rotation that would point the canvas toward the midpoint, but only in the Y axis
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        targetRotation.x = 0f;
        targetRotation.z = 0f;

        // Apply the rotation to the canvas element, but keep its position fixed
        canvasElement.rotation = targetRotation;
        Debug.Log(canvasElement.rotation);
        // set element rotation's y to  + 90 degrees
        canvasElement.Rotate(Vector3.up, -90);
    }
}
