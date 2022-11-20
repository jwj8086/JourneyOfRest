using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject option;
    public GameObject select_chapter;

    public void Option_clicked()
    {
        mainMenu.SetActive(false);
        option.SetActive(true);
    }

    public void Option_exit()
    {
        mainMenu.SetActive(true);
        option.SetActive(false);
    }

    public void Select_Chapter()
    {
        mainMenu.SetActive(false);
        select_chapter.SetActive(true);
    }

    public void Select_Chapter_exit()
    {
        mainMenu.SetActive(true);
        select_chapter.SetActive(false);
    }
}
