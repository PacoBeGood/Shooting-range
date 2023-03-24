using System.Collections.Generic;

using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static DataManagement;

public class RoofManager : MonoBehaviour
{
    public static RoofManager instance;

    [SerializeField]
    Transform parent;

    GameObject panelPrefab;

    [SerializeField]
    GameObject loadPanelPrefab;

    [SerializeField]
    List<Transform> panels;

    public List<Transform> panelsPrijs;

    [Space]
    [SerializeField]
    Transform sliderParent;

    [SerializeField]
    Slider inputX;

    [SerializeField]
    Slider inputY;

    [SerializeField]
    Slider inputXoffset;

    [SerializeField]
    Slider inputYoffset;

    [SerializeField]
    Slider inputDegrees;

    [SerializeField]
    Slider inputPanelDegrees;

    [Space]
    [SerializeField]
    int storedDegrees;

    [SerializeField]
    int storedPanelDegrees;

    [SerializeField]
    Vector2 storedSize;

    [SerializeField]
    Vector2 amount = new Vector2(1, 1);

    [SerializeField]
    Vector2 offset = new Vector2(0, 0);

    [Space]
    [SerializeField]
    TMP_Text textX;

    [SerializeField]
    TMP_Text textY;

    [SerializeField]
    TMP_Text textXoffset;

    [SerializeField]
    TMP_Text textYoffset;

    [SerializeField]
    TMP_Text textDegrees;

    [SerializeField]
    TMP_Text textPanelDegrees;

    public float getX;

    public float getY;

    public GameObject getPanel;

    [Space]
    [SerializeField]
    TMP_InputField inputName;

    [SerializeField]
    Button toggleSaveViewButton;

    [SerializeField]
    Button toggleLoadViewButton;

    [SerializeField]
    Button saveButton;
    [SerializeField]
    Button saveOverwriteButton;

    [SerializeField]
    Button loadButton;

    [SerializeField]
    Button exitSaveButton;

    [SerializeField]
    Button exitLoadButton;

    [SerializeField]
    GameObject LoadButtonsLocation;

    [Space]
    [SerializeField]
    Canvas defaultCanvas;

    [SerializeField]
    Canvas saveCanvas;

    [SerializeField]
    Canvas loadCanvas;

    [SerializeField] public string configurationName;

    
    [SerializeField] public string configurationId;

    private void Awake()
    {

        instance = this;
        inputX.onValueChanged.AddListener(OnEditX);
        inputY.onValueChanged.AddListener(OnEditY);
        inputXoffset.onValueChanged.AddListener(OnEditXOffset);
        inputYoffset.onValueChanged.AddListener(OnEditYOffset);
        inputDegrees.onValueChanged.AddListener((_) => OnEditDegrees(false));
        inputPanelDegrees.onValueChanged.AddListener(OnEditPanelDegrees);

        // toggleSaveViewButton.onClick.AddListener(ActivateSaveMode);
        // toggleLoadViewButton.onClick.AddListener(ActivateLoadMode);
        // saveButton.onClick.AddListener(Save);
        // saveOverwriteButton.onClick.AddListener(SaveOverwrite);
        // exitLoadButton.onClick.AddListener(ActivateDefaultMode);
        // exitSaveButton.onClick.AddListener(ActivateDefaultMode);

        getX = 1;
        getY = 1;
    }

    void Start()
    {
        // ActivateDefaultMode();
    }

    public void SetSize()
    {
        if (panelPrefab == null) return;

        panels.Clear();
        foreach (Transform child in parent) Destroy(child.gameObject);

        Vector3 baseLoc = new Vector3(
            transform.position.x - (transform.localScale.x / 2),
            0.11f,
            transform.position.z + (transform.localScale.z / 2)
        );
        baseLoc.x += (storedSize.x / 2);
        baseLoc.z -= storedSize.y;

        for (int i = 0; i < amount.x; i++)
        {
            for (int j = 0; j < amount.y; j++)
            {
                float xOffset = baseLoc.x + storedSize.x * i;
                float zOffset = baseLoc.z - storedSize.y * j;
                xOffset += offset.x * i;
                zOffset -= offset.y * j;
                Vector3 loc = new Vector3(xOffset, 0.11f, zOffset);

                GameObject panel = Instantiate(panelPrefab, loc, Quaternion.identity, parent);
                panel.transform.localEulerAngles = new Vector3(-storedPanelDegrees, 0, 0);
                panel
                    .transform
                    .GetChild(0)
                    .GetComponent<PanelObject>()
                    .SetMouse(false);
                panels.Add(panel.transform);
                panelsPrijs = panels;
            }
        }
    }

    public void OnEditX(float x)
    {
        amount.x = (int)x;
        textX.text = $"X: {amount.x}";
        if (panelPrefab != null) OnEditDegrees();
        getX = inputX.value;
        ;
    }

    public void OnEditY(float y)
    {
        amount.y = (int)y;
        textY.text = $"Y: {amount.y}";
        if (panelPrefab != null) OnEditDegrees();
        getY = inputY.value;

    }

    public void OnEditXOffset(float xOffset)
    {
        offset.x = Mathf.Round(xOffset * 100) / 100;
        textXoffset.text = $"Xo: {offset.x}";
        if (panelPrefab != null) OnEditDegrees();
    }

    public void OnEditYOffset(float yOffset)
    {
        offset.y = Mathf.Round(yOffset * 100) / 100;
        textYoffset.text = $"Yo: {offset.y}";
        if (panelPrefab != null) OnEditDegrees();
    }

    public void OnEditDegrees(bool resetGrid = true)
    {
        storedDegrees = (int)inputDegrees.value;
        textDegrees.text = $"{storedDegrees}°";
        transform.parent.localEulerAngles = new Vector3(0, 0, 0);
        if (resetGrid) SetSize();
        parent.parent = transform.parent;
        ObstacleManager.manager.GetZoneParent().parent = transform.parent;
        transform.parent.localEulerAngles = new Vector3(-storedDegrees, 0, 0);
    }

    public void OnEditPanelDegrees(float degrees)
    {
        storedPanelDegrees = (int)degrees;
        textPanelDegrees.text = $"{storedPanelDegrees}°P";
        if (panelPrefab != null) OnEditDegrees();
    }

    public void EnableSliders(bool b)
    {
        sliderParent.gameObject.SetActive(b);
    }

    public void SetObject(GameObject obj, float x, float z)
    {
        panelPrefab = obj;
        storedSize.x = x;
        storedSize.y = z;
        OnEditDegrees();
        getPanel = panelPrefab;
    }

    void ActivateSaveMode()
    {
        defaultCanvas.gameObject.SetActive(false);
        saveCanvas.gameObject.SetActive(true);
        if (configurationId != "")
        {
            inputName.text = configurationName;
            saveOverwriteButton.gameObject.SetActive(true);
        }
        else
        {
            inputName.text = "";
            saveOverwriteButton.gameObject.SetActive(false);
        }
    }

    // void ActivateLoadMode()
    // {
    //    Load();
    //     defaultCanvas.gameObject.SetActive(false);
    //     loadCanvas.gameObject.SetActive(true);
    // }

    // void ActivateDefaultMode()
    // {
    //     defaultCanvas.gameObject.SetActive(true);
    //     saveCanvas.gameObject.SetActive(false);
    //     loadCanvas.gameObject.SetActive(false);
    // }

    // void ExitSaveMode()
    // {
    //     defaultCanvas.gameObject.SetActive(true);
    //     saveCanvas.gameObject.SetActive(false);
    // }

    // void ExitLoadMode()
    // {
    //     foreach (Transform child in LoadButtonsLocation.transform)
    //         Destroy(child.gameObject);
    //     defaultCanvas.gameObject.SetActive(true);
    //     loadCanvas.gameObject.SetActive(false);
    // }

    // public void Save()
    // {
    //     DataManagement
    //         .instance
    //         .SaveConfiguration(inputName.text, parent, ExitSaveMode);
    // }

    // public void SaveOverwrite()
    // {
    //     DataManagement
    //         .instance
    //         .UpdateConfiguration(configurationId, inputName.text, parent, ExitSaveMode);
    // }

    // public void Load()
    // {
    //     DataManagement
    //         .instance
    //         .LoadConfigurations((LoadData response) =>
    //         {
    //             int i = 0;
    //             foreach (SaveData item in response.data)
    //             {
    //                 Button button = Instantiate(loadButton, loadCanvas.transform);
    //                 button.GetComponentInChildren<TMP_Text>().text = item.name;
    //                 button.onClick.AddListener(() => LoadConfiguration(item._id));
    //                 button.transform.SetParent(LoadButtonsLocation.transform);
    //                 button.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
    //                 button.transform.localPosition = new Vector3(85, -i * 30 - 30, 0);
    //                 button.gameObject.SetActive(true);
    //                 i++;
    //             }
    //         });
    // }

    // public void LoadConfiguration(string _id)
    // {
    //     DataManagement.instance.LoadConfiguration(
    //         _id,
    //         parent,
    //         loadPanelPrefab,
    //         ExitLoadMode
    //     );
    // }

    public void SetDegrees(int degrees)
    {
        inputDegrees.value = degrees;
        textDegrees.text = $"{storedDegrees}°";
    }

    public int GetDegrees()
    {
        return storedDegrees;
    }

    public void SetInputX(float x)
    {
        inputX.value = x;
    }

    public void SetInputY(float y)
    {
        inputY.value = y;
    }

    public void SetInputXOffset(float offset)
    {
        inputXoffset.value = offset;
    }

    public void SetInputYOffset(float offset)
    {
        inputYoffset.value = offset;
    }

    public void SetInputPanelDegrees(float degrees)
    {
        inputPanelDegrees.value = degrees;
    }

    public float GetInputX()
    {
        return inputX.value;
    }

    public float GetInputY()
    {
        return inputY.value;
    }

    public float GetInputXOffset()
    {
        return inputXoffset.value;
    }

    public float GetInputYOffset()
    {
        return inputYoffset.value;
    }

    public float GetInputPanelDegrees()
    {
        return inputPanelDegrees.value;
    }

    public Vector2 GetSize()
    {
        return storedSize;
    }
}
