using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelController : MonoBehaviour
{
    [SerializeField]private GameObject infoPanel;
    [SerializeField] private GameObject UserPanel;
    void Start()
    {
        infoPanel.SetActive(false);
        UserPanel.SetActive(false);
    }

    public void PanelOn()
    {
        infoPanel.SetActive(true);
    }
    public void PanelOff()
    {
        infoPanel.SetActive(false);
    }

}
