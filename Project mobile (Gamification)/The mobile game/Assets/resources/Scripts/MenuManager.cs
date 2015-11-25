using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [Header("lists")]
    public Button[] creationButtons;
    public GameObject[] panelActivate;
    public GameObject[] storeLists;

    [Header("panels")]
    public GameObject prefabCharacterCreation;
    public GameObject menuPanel;
    public GameObject Store;

    [Header("Pause buttons")]
    public GameObject pause_Button;
    public GameObject unPause_Button;

   
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
        Application.Quit();
    }


    #region character Creation section

    public void GoToEquipementMenu()
    {
        menuPanel.SetActive(false);
        prefabCharacterCreation.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        prefabCharacterCreation.SetActive(false);
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// when choose a body part you can select different items
    /// </summary>
    /// <param name="bodyPart"></param>
    public void CharacterCreationMenu(int bodyPart)
    {
        panelActivate[0].SetActive(false);
        for (int i = 0; i < creationButtons.Length; i++)
        {
            if (bodyPart == i)
            {
                panelActivate[i + 1].SetActive(true);
            }
        }
    }
    /// <summary>
    /// Go back to the main character creation menu
    /// </summary>
    /// <param name="goBack"></param>
    public void GoBackCharacterCreation(bool goBack)
    {
        if (goBack)
        {
            for (int i = 0; i < panelActivate.Length; i++)
            {
                panelActivate[i].SetActive(false);
            }
            panelActivate[0].SetActive(true);
           
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
