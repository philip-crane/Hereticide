using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button ability1;
    public Button ability2;
    
    private void Start()
    {
        ShowButtons();
    }

    private void Update()
    {
    }
    
    public void ShowButtons()
    {
        ability1.gameObject.SetActive(true);
        ability2.gameObject.SetActive(true);
    }

    public void HideButtons()
    {
        ability1.gameObject.SetActive(false);
        ability2.gameObject.SetActive(false);
    }
}