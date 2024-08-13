using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class LeftControllerMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Vector3 _menuOffset;

    [SerializeField] private float _angleThreshold = 35f;

    private XRNode inputSource = XRNode.LeftHand;

    void Start()
    {
        
    }
    
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftHandPosition) && 
            device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion leftHandRotation))
        {

            float angle = Vector3.Angle(leftHandRotation * Vector3.up, Vector3.up);

            _menuCanvas.SetActive(angle < _angleThreshold);
            if (_menuCanvas.activeSelf)
            {
                Vector3 offsetPosition = leftHandPosition + leftHandRotation * _menuOffset;
                _menuCanvas.transform.position = offsetPosition;
                _menuCanvas.transform.rotation = leftHandRotation;
            }

            
        }
    }
}
