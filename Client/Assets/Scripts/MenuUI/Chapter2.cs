using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter2 : MonoBehaviour
{
    public GameObject select_chapter;
    public GameObject chapter2;

    public void Chapter2_clicked()
    {
        select_chapter.SetActive(false);
        chapter2.SetActive(true);
    }

    public void Chapter2_no()
    {
        select_chapter.SetActive(true);
        chapter2.SetActive(false);
    }

    public void Chapter2_yes()
    {
        SceneManager.LoadScene("Map02");
    }
}
