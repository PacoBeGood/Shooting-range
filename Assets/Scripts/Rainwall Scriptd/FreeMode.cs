using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FreeMode : MonoBehaviour
{

    [SerializeField] private Button FreemodeOn;
    [SerializeField] private Button FreemodeOff;
    private bool FreeModeActive = false;
    [SerializeField] private Camera MainCam;
    [SerializeField] private Camera movingCam;
    [SerializeField] private Button ZoomMode;
    private bool ActiveZoommode = false;
    int speed = 5;

    private Vector3 PreviousPosition;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 movementMultiplier;

    void Start()
    {
        ZoomMode.gameObject.SetActive(false);
        ZoomMode.onClick.AddListener(ZoomModeActive);
    }


    void Update()
    {
        moveDirection = new Vector3(movementMultiplier.x, movementMultiplier.y, movementMultiplier.z);
        moveDirection = movingCam.transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (FreeModeActive == false)
            {
                FreemodeOff.gameObject.SetActive(false);
                FreemodeOn.gameObject.SetActive(true);

                FreeModeActive = true;

                ZoomMode.gameObject.SetActive(true);
            }
            else
            {
                FreeModeActive = false;
                FreemodeOn.gameObject.SetActive(false);
                FreemodeOff.gameObject.SetActive(true);

                ZoomMode.gameObject.SetActive(false);
            }
        }


        if (FreeModeActive == true)
        {


            if (Input.GetMouseButtonDown(0))
            {
                PreviousPosition = movingCam.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = PreviousPosition - movingCam.ScreenToViewportPoint(Input.mousePosition);

                movingCam.transform.position = new Vector3();
                movingCam.transform.Rotate(new Vector3(1, 0, 0), -direction.y * 180);
                movingCam.transform.Rotate(new Vector3(0, 1, 0), direction.x * 180, Space.World);
                movingCam.transform.Translate(new Vector3(0, 0, -10));

                PreviousPosition = movingCam.ScreenToViewportPoint(Input.mousePosition);
            }


        }



    }

    void ZoomModeActive()
    {
        if (FreeModeActive == false) return;
        ActiveZoommode = !ActiveZoommode;

        if (ActiveZoommode) movingCam.fieldOfView = 15; 
        else movingCam.fieldOfView = 60;
    
    }


}
