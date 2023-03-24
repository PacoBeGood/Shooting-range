using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager manager;
    [SerializeField] Button toggleButton;
    bool mode;
    [SerializeField] List<Button> buttons;
    [Space]
    [SerializeField] Camera cam;
    [SerializeField] Transform selected;
    [SerializeField] Transform ghost;
    bool isPlacing;
    [Space]
    [SerializeField] LayerMask mask;
    [SerializeField] Transform zoneParent;
    [SerializeField] GameObject zonePrefab;
    [SerializeField] List<Transform> zones;
    [Space]
    [SerializeField] Material baseMat;
    [SerializeField] Material selectMat;
    [SerializeField] List<Transform> tools;

    enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
    enum ScaleDir
    {
        XDown,
        XUp,
        YUp,
        YDown
    }

    private void Awake()
    {
        manager = this;
    }
    void Start()
    {
        toggleButton.onClick.AddListener(Toggle);
        buttons[0].onClick.AddListener(Place);
        buttons[1].onClick.AddListener(Delete);
        buttons[2].onClick.AddListener(Move);
        buttons[3].onClick.AddListener(Rotate);
        buttons[4].onClick.AddListener(Scale);
    }

    void Update()
    {
        if (!mode) return;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            if(isPlacing) ghost.position = hit.point += new Vector3(0, 0.5f, 0);

            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;

            if (isPlacing && hit.transform.tag != "Zone")
            {
                Vector3 pos = hit.point;
                selected = Instantiate(zonePrefab, pos, Quaternion.identity, zoneParent).transform;
                zones.Add(selected);
                SetSelection();
            } else if(hit.transform.tag == "Zone")
            {
                selected = hit.transform;
                SetSelection();
            }
        }
    }

    public Transform GetZoneParent() { return zoneParent; }

    private void Toggle()
    {
        StopPlacing();
        if (!mode)
        {
            RoofManager.instance.SetDegrees(0);
            RoofManager.instance.EnableSliders(false);
            foreach (Button b in buttons) b.gameObject.SetActive(true);
            mode = true;
        }
        else
        {
            RoofManager.instance.EnableSliders(true);
            selected = null;
            foreach (Transform t in zones) t.GetComponent<Renderer>().material = baseMat;

            foreach (Button b in buttons) b.gameObject.SetActive(false);
            foreach (Transform t in tools) t.gameObject.SetActive(false);
            mode = false;
        }
        foreach (Button b in buttons) b.interactable = true;
    }
    void StopPlacing()
    {
        isPlacing = false;
        ghost.gameObject.SetActive(false);
    }
    private void SetActiveButton(int i)
    {
        foreach (Button b in buttons) b.interactable = true;
        foreach (Transform t in tools) t.gameObject.SetActive(false);
        buttons[i].interactable = false;
        StopPlacing();
    }
    private void SetSelection()
    {
        foreach (Transform t in zones) t.GetComponent<Renderer>().material = baseMat;
        selected.GetComponent<Renderer>().material = selectMat;
    }
    private void Place()
    {
        SetActiveButton(0);
        ghost.gameObject.SetActive(true);
        isPlacing = true;
        
    }
    private void Delete()
    {
        StopPlacing();
        if (selected != null)
        {
            zones.Remove(selected);
            Destroy(selected.gameObject);
        }
        buttons[0].interactable = true;
        foreach (Transform t in tools) t.gameObject.SetActive(false);

        
    }
    private void Move()
    {
        SetActiveButton(2);
        tools[0].gameObject.SetActive(true);
    }
    private void Rotate()
    {
        SetActiveButton(3);
        tools[1].gameObject.SetActive(true);
    }
    private void Scale()
    {
        SetActiveButton(4);
        tools[2].gameObject.SetActive(true);
    }
    public void UseMove(int use)
    {
        if (selected == null) return;
        switch (use)
        {
            case (int)Direction.Left:
                selected.Translate(new Vector3(-0.2f, 0, 0), Space.World);
                break;
            case (int)Direction.Right:
                selected.Translate(new Vector3(0.2f, 0, 0), Space.World);
                break;
            case (int)Direction.Up:
                selected.Translate(new Vector3(0, 0, 0.2f), Space.World);
                break;
            case (int)Direction.Down:
                selected.Translate(new Vector3(0, 0, -0.2f), Space.World);
                break;
        }
    }
    public void UseRotate(int use)
    {
        if (selected == null) return;
        if (use == (int)Direction.Left)
        {
            RotateSelected(Direction.Left);
        }
        else
        {
            RotateSelected(Direction.Right);
        }
    }
    void RotateSelected(Direction direction)
    {
        if(direction == Direction.Left) selected.Rotate(Vector3.up, -10f);
        else selected.Rotate(Vector3.up, 10f);
    }
    public void UseScale(int use)
    {
        if (selected == null) return;
        switch (use)
        {
            case (int)ScaleDir.XDown:
                selected.localScale += new Vector3(-0.2f, 0, 0);
                break;
            case (int)ScaleDir.XUp:
                selected.localScale += new Vector3(0.2f, 0, 0);
                break;
            case (int)ScaleDir.YUp:
                selected.localScale += new Vector3(0, 0, 0.2f);
                break;
            case (int)ScaleDir.YDown:
                selected.localScale += new Vector3(0, 0, -0.2f);
                break;
        }
    }
}
