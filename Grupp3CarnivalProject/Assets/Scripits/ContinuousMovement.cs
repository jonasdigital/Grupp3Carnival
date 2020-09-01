using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class ContinuousMovement : MonoBehaviour
{

    public LayerMask groundLayer;

    public float gravity = -9.81f;
    private float fallingSpeed;

    public float additionlHeight = 0.2f;

    public float speed = 1;

    private CharacterController character;

    public XRNode inputSource;
    private Vector2 inputAxis;

    private XRRig rig;

    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {

        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);

        //gravity
        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);

    }


    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionlHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }


    bool CheckIfGrounded()
    {

        Vector3 rayStart = transform.InverseTransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;

        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;

    }


}
