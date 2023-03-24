using System.Collections.Generic;
using UnityEngine;

using System;
using Newtonsoft.Json;

public class DataManagement : MonoBehaviour
{
    public static DataManagement instance;

    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);


    }

    public class VectorData
    {
        public float x;
        public float y;
        public float z;
    }

    public class QuaternionData
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }

    public class PanelData
    {
        public VectorData position;
        public QuaternionData rotation;
        public VectorData scale;
    }

    public class GameObjectData
    {
        public VectorData position;
        public QuaternionData rotation;
        public VectorData scale;
    }

    public class RoofData
    {
        public GameObjectData gameObject;
        public int degrees;
    }

    public class SlidersData
    {
        public float x;
        public float y;
        public float xOffset;
        public float yOffset;
        public float degrees;
        public float panelDegrees;
    }

    public class SaveData
    {
#nullable enable
        public string? _id;
#nullable disable
        public string name;
        public VectorData size;
        public RoofData roof;
        public SlidersData sliders;
    }

    public class LoadData
    {
        public SaveData[] data;
    }

    public static SaveData Save(List<Transform> panels, Transform roof, string name)
    {
        if (name == null)
        {
            name = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }
        List<PanelData> panelData = new List<PanelData>();
        foreach (Transform panel in panels)
        {
            panelData.Add(new PanelData
            {
                position = new VectorData
                {
                    x = panel.position.x,
                    y = panel.position.y,
                    z = panel.position.z
                },
                rotation = new QuaternionData
                {
                    x = panel.rotation.x,
                    y = panel.rotation.y,
                    z = panel.rotation.z,
                    w = panel.rotation.w
                },
                scale = new VectorData
                {
                    x = panel.localScale.x,
                    y = panel.localScale.y,
                    z = panel.localScale.z,
                }
            });
        }

        RoofData roofData = new RoofData
        {
            gameObject = new GameObjectData
            {
                position = new VectorData
                {
                    x = roof.position.x,
                    y = roof.position.y,
                    z = roof.position.z
                },
                rotation = new QuaternionData
                {
                    x = roof.rotation.x,
                    y = roof.rotation.y,
                    z = roof.rotation.z,
                    w = roof.rotation.w
                },
                scale = new VectorData
                {
                    x = roof.localScale.x,
                    y = roof.localScale.y,
                    z = roof.localScale.z,
                }
            },
            degrees = RoofManager.instance.GetDegrees()
        };

        SlidersData slidersData = new SlidersData
        {
            x = RoofManager.instance.GetInputX(),
            y = RoofManager.instance.GetInputY(),
            xOffset = RoofManager.instance.GetInputXOffset(),
            yOffset = RoofManager.instance.GetInputYOffset(),
            degrees = RoofManager.instance.GetDegrees(),
            panelDegrees = RoofManager.instance.GetInputPanelDegrees()
        };

        Vector2 size = RoofManager.instance.GetSize();
        VectorData sizeData = new VectorData
        {
            x = size.x,
            y = size.y
        };

        SaveData data = new SaveData
        {
            name = name,
            size = sizeData,
            roof = roofData,
            sliders = slidersData
        };


        return data;
    }

    // public void LoadConfigurations(Action<LoadData> callback)
    // {
    //     StartCoroutine(Request.get<LoadData>("/zonnepanelen", (response) => callback(response)));
    // }

    // public void LoadConfiguration(string _id, Transform parent, GameObject prefab, Action callback)
    // {
    //     StartCoroutine(Request.get<LoadData>($"/zonnepanelen/{_id}", (LoadData response) =>
    //            {
    //       :/ int degrees = response.data[0].roof.degrees;
    //                RoofManager.instance.SetDegrees(degrees);

    //                RoofManager.instance.configurationName = response.data[0].name;
    //                RoofManager.instance.configurationId = response.data[0]._id;

    //                SlidersData sliders = response.data[0].sliders;
    //                RoofManager.instance.SetInputX(sliders.x);
    //                RoofManager.instance.SetInputY(sliders.y);
    //                RoofManager.instance.SetInputXOffset(sliders.xOffset);
    //                RoofManager.instance.SetInputYOffset(sliders.yOffset);
    //                RoofManager.instance.SetInputPanelDegrees(sliders.panelDegrees);

    //                foreach (Transform child in parent) Destroy(child.gameObject);

    //                GameObject panelParent = GameObject.Find("PanelParent");
    //                Vector2 size = new Vector2(response.data[0].size.x, response.data[0].size.y);
    //                RoofManager.instance.SetObject(panelParent, size.x, size.y);

    //                callback();
    //            }));
    // }

    // public void SaveConfiguration(string name, Transform parent, Action callback)
    // {
    //     List<Transform> children = new List<Transform>();
    //     foreach (Transform child in parent) children.Add(child);

    //     SaveData data = DataManagement.Save(children, transform, name);
    //     string json = JsonConvert.SerializeObject(data);
    //     StartCoroutine(Request.post<SaveData>("/zonnepanelen", json, (SaveData response) =>
    //     {
    //         callback();
    //     }));
    // }

    // public void UpdateConfiguration(string _id, string name, Transform parent, Action callback)
    // {
    //     List<Transform> children = new List<Transform>();
    //     foreach (Transform child in parent) children.Add(child);

    //     SaveData data = DataManagement.Save(children, transform, name);
    //     string json = JsonConvert.SerializeObject(data);
    //     StartCoroutine(Request.put<SaveData>($"/zonnepanelen/{_id}", json, (SaveData response) =>
    //     {
    //         callback();
    //     }));
    // }
}
