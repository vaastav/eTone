using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour {

    public GameObject Panel;


    public void ClickedPanel()
    {
        Panel.SetActive(true);
    }
}
