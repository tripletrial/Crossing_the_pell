using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recenteringThroughControllerLocation : MonoBehaviour
{
    // This function gets the left and right controller GameObjects within the Ovr Camera Rig and get the center of those two GameObjects\
    // Then it changes the looking direction fo the Camera towards the center of those two GameObjects to recenter

    GameObject targetLeftController;
    GameObject targetRightController;
    public GameObject leftController;
    public GameObject rightController;
    public Transform VRtrackingspace;
    public Transform VRcamera;
    public bool recenter = false;

    void Update()
    {
        if (recenter)
        {
            // find active gameobject with tag "controller_l" and "controller_r"
            targetLeftController = GameObject.FindWithTag("controller_l");
            targetRightController = GameObject.FindWithTag("controller_r");

            // Get the positions of the target controllers
            Vector3 targetLeftPos = targetLeftController.transform.position;
            Vector3 targetRightPos = targetRightController.transform.position;

            // Calculate the center point between the two target controllers
            Vector3 centerPos = (targetLeftPos + targetRightPos) / 2f;

            Vector3 leftPos = leftController.transform.position;
            Vector3 rightPos = rightController.transform.position;
            // Debug.Log("leftPos: " + leftPos);
            // Debug.Log("rightPos: " + rightPos);


            // Calculate the center point between the two controllers
            Vector3 centerPos2 = (leftPos + rightPos) / 2f;

            // calcualte the xyz differece between the two pairs of controllers
            Vector3 diff = centerPos - centerPos2;
            // Debug.Log("diff: " + diff);

            // move the VRtrackingspace by the difference
            VRtrackingspace.position += diff;

            //calculate the y angle of two lines from centerPos to leftPos and centerPos to targetLeftPos
            Debug.Log("centerPos: " + centerPos);
            Debug.Log("leftPos: " + leftPos);
            Debug.Log("targetLeftPos: " + targetLeftPos);
            float angle = Vector3.SignedAngle(
                centerPos - leftPos,
                centerPos - targetLeftPos,
                Vector3.up
            );

            Debug.Log("angle: " + angle);
            // Rotate the VRtrackingspace around the y axis
            VRtrackingspace.RotateAround(VRtrackingspace.position, Vector3.up, angle);

            leftPos = leftController.transform.position;
            rightPos = rightController.transform.position;
            centerPos2 = (leftPos + rightPos) / 2f;
            diff = centerPos - centerPos2;
            VRtrackingspace.position += diff;

            recenter = false;
        }
    }
}
