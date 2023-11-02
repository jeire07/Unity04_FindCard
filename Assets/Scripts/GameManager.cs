using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Text timeTxt;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endTxt;
    public Text result;
    public Text tryCount;

    public static GameManager I;

    public AudioClip match;
    public AudioClip miss;
    public AudioClip clear;
    public AudioClip fail;
    public AudioSource audioSource;

    int count = 0;
    public float time;
    public float timecount;

    string[] cardName = {"rtan0", "rtan1", "rtan2", "rtan3", "rtan4", "rtan5", "rtan6", "rtan7"};

    void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        //강의 자료 코드
        //int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        //rtans = rtans.OrderBy(itme => Random.Range(-1.0f, 1.0f)).ToArray();

        //대체 코드
        List<int> initRtans 
            = new List<int>{ 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        int[] rtans = new int[16];

        for (int i = 0; i < 16; i++)
        {
            int randomNum = initRtans[Random.Range(0, initRtans.Count)];
            rtans[i] = randomNum;
            Debug.Log(randomNum);
            initRtans.Remove(randomNum);
        }

        for (int i = 0; i < 16; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent
                = GameObject.Find("Cards").transform;

            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if (time >= 50.0f)
        {
            audioSource.PlayOneShot(fail);
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
        timeTxt.text = time.ToString("N2");

        if (firstCard == null)
        {
            timecount = time;
        }
        else if ( (time - timecount) > 5.0f)
        {
            firstCard.GetComponent<Card>().CloseCard();
            GameManager.I.firstCard = null;
        }
    }

    public void IsMatched()
    {
        count += 1;
        tryCount.text = count.ToString();
        string firstCardImage = firstCard.transform.Find("front")
            .GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front")
            .GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            result.text = "성공!";
            audioSource.PlayOneShot(match);

            //Debug.Log("equal");
            firstCard.GetComponent<Card>().DestroyCard();
            secondCard.GetComponent<Card>().DestroyCard();

            int cardsLeft = GameObject.Find("Cards")
                .transform.childCount;
            //Debug.Log(cardsLeft);
            if (cardsLeft == 2)
            {
                audioSource.PlayOneShot(clear);
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            result.text = "실패!, 0.5초 패널티!";
            time += 0.5f;
            audioSource.PlayOneShot(miss);

            //Debug.Log("not equal");
            firstCard.GetComponent<Card>().CloseCard();
            secondCard.GetComponent<Card>().CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }
}
