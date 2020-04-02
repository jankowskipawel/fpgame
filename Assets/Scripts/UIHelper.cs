using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelper : MonoBehaviour
{
    public GameObject inventoryUI;    
    public GameObject menuUI;

    private bool isActiveInventoryUI = false;
    private bool isActiveMenuUI = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isActiveInventoryUI)
            {
                inventoryUI.SetActive(false);
                isActiveInventoryUI = false;
            }
            else
            {
                inventoryUI.SetActive(true);
                isActiveInventoryUI = true;
            } 
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActiveMenuUI)
            {
                menuUI.SetActive(false);
                isActiveMenuUI = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                menuUI.SetActive(true);
                isActiveMenuUI = true;
                Cursor.lockState = CursorLockMode.Confined;

            } 
        }
    }
}
