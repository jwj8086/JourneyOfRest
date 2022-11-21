using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject merchant;

    public void Store_clicked()
    {
        merchant.SetActive(true);
        Time.timeScale = 0;
    }
}
