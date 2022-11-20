using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter1 : MonoBehaviour
{
    public GameObject select_chapter;
    public GameObject chapter1;

    public void Chapter1_clicked()
    {
        select_chapter.SetActive(false);
        chapter1.SetActive(true);
    }

    public void Chapter1_no()
    {
        select_chapter.SetActive(true);
        chapter1.SetActive(false);
    }

    public void Chapter1_yes()
    {
        SceneManager.LoadScene("Map01");
    }
}
