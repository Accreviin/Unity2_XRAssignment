using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CastRay : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] LineRenderer _rayRenderer;

    private RaycastHit _hitInfo;
    private XRNode _inputSource = XRNode.RightHand;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(_inputSource);
        if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightHandPosition) &&
            device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightHandRotation))
        {
            Vector3 forward = rightHandRotation * Vector3.forward;

            if (Physics.Raycast(rightHandPosition, forward, out _hitInfo))
            {
                Debug.Log("Ray Hit");

                _rayRenderer.SetPosition(0, rightHandPosition);
                _rayRenderer.SetPosition(1, _hitInfo.point);
                ResetButton _resetbutton;

                if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    _resetbutton = _hitInfo.collider.gameObject.GetComponent<ResetButton>();
                    if (_resetbutton != null )
                    {
                        _resetbutton.ButtonPressed();
                    }
                    
                    Debug.Log("Trigger Pressed");
                    var spawnPosition = _hitInfo.point + Vector3.up;
                    Instantiate(_object, spawnPosition, Quaternion.identity);
                }
            }

            else
            {
                _rayRenderer.SetPosition(0, rightHandPosition);
                _rayRenderer.SetPosition(1, rightHandPosition + forward * 100);
            }
        }
    }
}
