using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportController : MonoBehaviour
{

    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshHold = 0.1f;

    public bool EnableLeftTeleport { get; set; } = true;
    public bool EnableRightTeleport { get; set; } = true;

    void Start()
    {
        
    }

   
    void Update()
    {

        if (leftTeleportRay)
        {
            leftTeleportRay.gameObject.SetActive(EnableLeftTeleport && CheckifActivated(leftTeleportRay));
        }

        if (rightTeleportRay)
        {
            rightTeleportRay.gameObject.SetActive(EnableRightTeleport && CheckifActivated(rightTeleportRay));
        }

    }


    public bool CheckifActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshHold);
        return isActivated;
    }


}
