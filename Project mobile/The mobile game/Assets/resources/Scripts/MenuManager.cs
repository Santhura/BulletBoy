using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [Header("lists")]
    public Button[] inventoryButtons;               // list of item buttons of the inventory
    public GameObject[] inventoryPanelsActivate;    // list of the panels of the inventory
    public GameObject[] storeLists;                 // list of options to buy equipement in the store

    [Header("panels")]
    public GameObject prefabInventoryMenu;          // Prefab of the inventory menu
    public GameObject menuPanel;                    // Begin pause menu panel
    public GameObject Store;                        // Store panel

    [Header("Pause buttons")]
    public GameObject pause_Button;                 //pause button
    public GameObject unPause_Button;               //Unpause button

   

    /// <summary>
    /// Switches the game between pause and unpause
    /// </summary>
    /// <param name="isPause"></param>
    public void Pause(bool isPause)
    {
        if (!isPause)
        {
            pause_Button.SetActive(false);
            unPause_Button.SetActive(true);
            Time.timeScale = 0;
            menuPanel.SetActive(true);
            isPause = true;
        }
        else
        {
            pause_Button.SetActive(true);
            unPause_Button.SetActive(false);
            Time.timeScale = 1;
            menuPanel.SetActive(false);
            isPause = false; 
        }
    }

    public void QuitGame()
    {
        Application.Quit(); // Quits game
    }


    #region Inventory section

    /// <summary>
    /// go to Inventory menu screen
    /// </summary>
    public void GoToInventoryMenu()
    {
        menuPanel.SetActive(false);
        prefabInventoryMenu.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        prefabInventoryMenu.SetActive(false);
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// Inventory menu
    /// </summary>
    /// <param name="bodyPart"></param>
    public void InventoryMenu(int itemNumber)
    {
        inventoryPanelsActivate[0].SetActive(false);
        for (int i = 0; i < inventoryButtons.Length; i++)
        {
            if (itemNumber == i)
            {
                inventoryPanelsActivate[i].SetActive(true);
            }
        }
    }
    /// <summary>
    /// Go back to Inventory menu
    /// </summary>
    /// <param name="goBack"></param>
    public void GoBackToInventoryMenu(bool goBack)
    {
        if (goBack)
        {
            for (int i = 0; i < inventoryPanelsActivate.Length; i++)
            {
                inventoryPanelsActivate[i].SetActive(false);
            }
            inventoryPanelsActivate[0].SetActive(true);
           
        }
    }
    #endregion

    #region store methods

    public void GoToStore(bool isOnStore)
    {
        if (!isOnStore)
        {
            menuPanel.SetActive(false);
            Store.SetActive(true);
        }
        else
        {
            menuPanel.SetActive(true);
            Store.SetActive(false);
        }

    }
    /// <summary>
    /// Select a part to buy
    /// </summary>
    /// <param name="bodyPart"></param>
    public void SelectStoreGatagory(int bodyPart)
    {
        for (int i = 0; i < storeLists.Length; i++)
        {
            storeLists[i].SetActive(false);
            if (bodyPart == i)
            {
                storeLists[i].SetActive(true);
            }
        }
    }

    #endregion
}
