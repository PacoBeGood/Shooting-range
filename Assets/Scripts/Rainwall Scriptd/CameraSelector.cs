using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSelector : MonoBehaviour
{
    [SerializeField] private Button Viewmode;
    [SerializeField] private Button FrontButton;
    [SerializeField] private Button BackButton;
    [SerializeField] private Button LeftButton;
    [SerializeField] private Button RightButton;
    [SerializeField] private Button TopButton;
    [SerializeField] private Button ExitButton;

    [SerializeField] private Camera FrontCam;
    [SerializeField] private Camera BackCam;
    [SerializeField] private Camera LeftCam;
    [SerializeField] private Camera RightCam;
    [SerializeField] private Camera TopCam;
    
    List <Camera> cameras = new List<Camera> ();
    

    
    // Start is called before the first frame update
    void Start()
    {
        Viewmode.onClick.AddListener(EnableViewMode);
        FrontButton.onClick.AddListener(() => ToggleCamera(CameraPosition.Front));
        BackButton.onClick.AddListener(() => ToggleCamera(CameraPosition.Back));
        LeftButton.onClick.AddListener(() => ToggleCamera(CameraPosition.Left));
        RightButton.onClick.AddListener(() => ToggleCamera(CameraPosition.Right));
        TopButton.onClick.AddListener(() => ToggleCamera(CameraPosition.Top));
        ExitButton.onClick.AddListener(() => ExitButtonClicked(CameraPosition.Top));
        
        cameras.Add(FrontCam);
        cameras.Add(BackCam);
        cameras.Add(LeftCam);
        cameras.Add(RightCam);
        cameras.Add(TopCam);

        foreach (Camera c in cameras) c.enabled = false;
        
        TopCam.enabled = true;

        FrontButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        LeftButton.gameObject.SetActive(false);
        RightButton.gameObject.SetActive(false);
        TopButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        
    }
    enum CameraPosition {
        Front,
        Back,
        Left,
        Right,
        Top
    }

    void EnableViewMode()
    {
        FrontButton.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
        LeftButton.gameObject.SetActive(true);
        RightButton.gameObject.SetActive(true);
        TopButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
    } 

    void ToggleCamera(CameraPosition index)
    {
        CameraPosition i = 0;
        
        foreach (Camera camera in cameras)
        {
            if (index == i)
            {
                camera.enabled = true;
            }
            else 
            {
                camera.enabled = false;
            }
            i++;
        }
    }

    void ExitButtonClicked(CameraPosition index)
    {
        FrontButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        LeftButton.gameObject.SetActive(false);
        RightButton.gameObject.SetActive(false);
        TopButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        CameraPosition i = 0;
        
        foreach (Camera camera in cameras)
        {
            if (index == i)
            {
                camera.enabled = true;
            }
            else 
            {
                camera.enabled = false;
            }
            i++;
        }
    }


}
