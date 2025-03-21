using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(XROrigin))]
public class RoomScaleFix : MonoBehaviour
{
    CharacterController character;
    XROrigin xrOrigin;

    void Start()
    {
        character = GetComponent<CharacterController>();
        xrOrigin = GetComponent<XROrigin>();
    }

    void Update()
    {
        character.height = xrOrigin.CameraInOriginSpaceHeight + 0.15f;

        var centerPoint = transform.InverseTransformPoint(xrOrigin.Camera.transform.position);
        character.center = new Vector3(centerPoint.x, character.height/2 + character.skinWidth, centerPoint.z);

        character.Move(new Vector3(0.001f, -0.001f, 0.001f));
        character.Move(new Vector3(-0.001f, -0.001f, -0.001f));
    }
}
