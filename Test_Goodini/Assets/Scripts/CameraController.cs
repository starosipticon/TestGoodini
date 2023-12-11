using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour

{
    [SerializeField] private Joystick _joystick;
    [SerializeField] TouchScreen _touchScreen;
    [SerializeField] private CinemachineFreeLook[] Cameras;
    [SerializeField] [Range(0.1f, 1f)] private float _horizontalInputScale = 1f;
    [SerializeField] [Range(0.1f, 1f)] private float _verticalInputScale = 0.5f;
    [SerializeField] float minFov = 15;
    [SerializeField] float maxFov = 90;
    private int _activeCameraIndex = 0;
    private float currentFov;

    private void Start()
    {
        currentFov = Cameras[_activeCameraIndex].m_Lens.FieldOfView;
    }
    private void Update()
    {

        if (_touchScreen.isUsed == true)
        {
            ChangeFov(_touchScreen.deltaDistance * 0.1f);
            Cameras[_activeCameraIndex].m_YAxis.m_InputAxisValue = _touchScreen.verticalRotation * _verticalInputScale;
            Cameras[_activeCameraIndex].m_XAxis.m_InputAxisValue = _touchScreen.horizontalRotation * 0.1f;
        }
        
        else
        {
            Cameras[_activeCameraIndex].m_YAxis.m_InputAxisValue = _joystick.Vertical * _verticalInputScale;
            Cameras[_activeCameraIndex].m_XAxis.m_InputAxisValue = _joystick.Horizontal * _horizontalInputScale;
        }
    }

    public void ChangeCamera(int index)
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].gameObject.SetActive(i == index);
        }
        _activeCameraIndex = index;
    }

    private void ChangeFov(float scale)
    {
        currentFov = Cameras[_activeCameraIndex].m_Lens.FieldOfView;
        if ((currentFov <= maxFov && scale > 0) || (currentFov >= minFov && scale < 0))
        {
            Cameras[_activeCameraIndex].m_Lens.FieldOfView -= scale;
            Cameras[_activeCameraIndex].m_Lens.FieldOfView = Mathf.Clamp(Cameras[_activeCameraIndex].m_Lens.FieldOfView,15,90);
        }

    }
}
