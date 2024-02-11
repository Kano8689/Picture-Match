using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public Button[] btn;
    int lvlno = 0,maxlvl = 0;
    
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void OnEnable()
    {
        lvlno = PlayerPrefs.GetInt("LevelNo", 1);
        maxlvl = PlayerPrefs.GetInt("MaxLevel", 1);

        for(int i=0;i<=maxlvl;i++)
        {
            btn[i].interactable = true;
            btn[i].GetComponentInChildren<Text>().text = "LEVEL " + (i + 1);
        }
    }

    public void OnClickLvlNo(int n)
    {
        PlayerPrefs.SetInt("LevelNo", n);
        SceneManager.LoadScene("Play");
    }
}
