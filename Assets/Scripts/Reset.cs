using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    [SerializeField] Button resetButton;
    // Start is called before the first frame update
    void Start()
    {
        resetButton.onClick.AddListener(Reset1);
    }

    // Update is called once per frame
    void Reset1()
    {
        SceneManager.LoadScene("InsideRange");
    }
}
