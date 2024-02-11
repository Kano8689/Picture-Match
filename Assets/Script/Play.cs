using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Play : MonoBehaviour
{
    public Transform Image;
    public GameObject Prefab;
    public Sprite[] AllImage;
    public Sprite Box;
    List<int> list1 = new List<int>();
    List<int> list2 = new List<int>();
    List<int> AllCards = new List<int>();
    int totalCard = 3, totalPairs = 0;
    int cnt = 0, wincnt = 0;
    int lvlno=0, maxlvl = 0;
    Sprite cnt1, cnt2;
    GameObject g1, g2;
    // Start is called before the first frame update
    void Start()
    {
        lvlno = PlayerPrefs.GetInt("LevelNo", 1);

        if (lvlno <= 4)
        {
            Image.GetComponentInChildren<GridLayoutGroup>().cellSize = new Vector2(200, 200);
            Image.GetComponentInChildren<GridLayoutGroup>().spacing = new Vector2(12, 12);
        }
        else if (lvlno == 5)
        {
            Image.GetComponentInChildren<GridLayoutGroup>().cellSize = new Vector2(160, 160);
            Image.GetComponentInChildren<GridLayoutGroup>().spacing = new Vector2(12, 12);
        }
        else if (lvlno == 6)
        {
            Image.GetComponentInChildren<GridLayoutGroup>().cellSize = new Vector2(140, 140);
            Image.GetComponentInChildren<GridLayoutGroup>().spacing = new Vector2(10, 10);
        }
        else if(lvlno ==7)
        {
            Image.GetComponentInChildren<GridLayoutGroup>().cellSize = new Vector2(110, 110);
            Image.GetComponentInChildren<GridLayoutGroup>().spacing = new Vector2(15, 15);
        }
        else if (lvlno == 8)
        {
            Image.GetComponentInChildren<GridLayoutGroup>().cellSize = new Vector2(100, 100);
            Image.GetComponentInChildren<GridLayoutGroup>().spacing = new Vector2(10, 10);
        }
        else
        {
            Image.GetComponentInChildren<GridLayoutGroup>().cellSize = new Vector2(83, 95);
            Image.GetComponentInChildren<GridLayoutGroup>().spacing = new Vector2(6, 10);
        }

        if (lvlno == 1)
        {
            Image.GetComponentInChildren<GridLayoutGroup>().constraintCount = 2;
            totalCard = 2 * 2;
        }
        else if (lvlno % 2 == 0)
        {
            Image.GetComponentInChildren<GridLayoutGroup>().constraintCount = lvlno;
            totalCard = lvlno * lvlno;
        }
        else
        {
            Image.GetComponentInChildren<GridLayoutGroup>().constraintCount = lvlno;
            totalCard = lvlno * (lvlno-1);
        }


        totalPairs = totalCard / 2;

        for(int i=0;i<totalPairs;i++)
        {
            int rn = Random.Range(0, AllImage.Length);
            while(list1.Contains(rn))
            {
                rn = Random.Range(0, AllImage.Length);
            }
            list1.Add(rn);
            list2.Add(rn);
        }
        AllCards.AddRange(list1);
        AllCards.AddRange(list2);
        Shuffle();
        for (int i=0;i<AllCards.Count;i++)
        {
            //print(AllCards[i]);
            int temp = AllCards[i];
            GameObject g =  Instantiate(Prefab, Image.transform);
            g.GetComponent<Button>().onClick.AddListener(() => onClickBtn(temp,g));
        }

    }

    void Shuffle()
    {
        for(int i=0;i<AllCards.Count;i++)
        {
            int rn = Random.Range(0, AllCards.Count);
            int temp = AllCards[rn];
            AllCards[rn] = AllCards[i];
            AllCards[i] = temp;
        }
    }

    public void onClickBtn(int temp, GameObject g)
    {
        lvlno = PlayerPrefs.GetInt("LevelNo", 1);
        maxlvl = PlayerPrefs.GetInt("MaxLevel", 1);

        //print(temp);
        g.GetComponent<Image>().sprite = AllImage[temp];
        g.GetComponent<Button>().interactable = false;
        cnt++;

        if (cnt == 1)
        {
            cnt1 = AllImage[temp];
            g1 = g;
        }
        else if (cnt == 2)
        {
            cnt2 = AllImage[temp];
            g2 = g;
        }

        //StartCoroutine(holdOn());
        if (cnt == 2)
        {
            if (cnt1 == cnt2)
            {
                Debug.Log("Winner") ;
                wincnt++;
            }
            else
            {
                Debug.Log("Loser");
                StartCoroutine(holdOn());
            }

            if(wincnt == totalPairs)
            {
                if (maxlvl <= lvlno)
                {
                    PlayerPrefs.SetInt("MaxLevel", lvlno);
                }

                lvlno++;
                PlayerPrefs.SetInt("LevelNo", lvlno);

                SceneManager.LoadScene("Level");
            }

            cnt = 0;
        }
    }

    IEnumerator holdOn()
    {
        yield return new WaitForSeconds(1f);
        g1.GetComponent<Image>().sprite = Box;
        g2.GetComponent<Image>().sprite = Box;

        g1.GetComponent<Button>().interactable = false;
        g2.GetComponent<Button>().interactable = false;
        g1.GetComponent<Button>().interactable = true;
        g2.GetComponent<Button>().interactable = true;
    }

}
