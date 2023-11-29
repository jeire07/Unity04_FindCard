using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Text timeCnt;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endTxt;

    public Text tryCnt;

    public static GameManager I;
    // 싱글톤 공부 -> 설명 가능한 수준까지 

    public AudioClip matchAudio;
    public AudioClip missAudio;
    public AudioClip clearAudio;
    public AudioClip failAudio;
    public AudioSource audioSource;

    int count = 0;
    public float elapsedTime;
    float remainTime = 50.0f;
    public float flipCnt;

    string[] cardName = { "rtan0", "rtan1", "rtan2", "rtan3", "rtan4", "rtan5", "rtan6", "rtan7" };

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
            = new() { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
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
        /*
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                float x = (i / 4) * 1.4f - 2.1f;
                float y = (j % 4) * 1.4f - 3.0f;
                newCard.transform.position = new Vector3(x, y, 0);
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if ( (remainTime - elapsedTime) <= 0.0f)
        {
            audioSource.PlayOneShot(failAudio);
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
        timeCnt.text = (remainTime - elapsedTime).ToString("N2");

        if (firstCard == null)
        {
            flipCnt = elapsedTime;
        }
        else if ((flipCnt - elapsedTime) > 5.0f)
        {
            firstCard.GetComponent<Card>().CloseCard();
            firstCard = null;
        }
    }

    public void IsMatched()
    {
        count += 1;
        tryCnt.text = count.ToString();
        string firstCardImage = firstCard.transform.Find("front")
            .GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front")
            .GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(matchAudio);
            //Debug.Log("equal");
            firstCard.GetComponent<Card>().DestroyCard();
            secondCard.GetComponent<Card>().DestroyCard();

            int cardsLeft = GameObject.Find("Cards")
                .transform.childCount;
            //Debug.Log(cardsLeft);
            if (cardsLeft == 2)
            {
                audioSource.PlayOneShot(clearAudio);
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            elapsedTime += 0.5f;
            audioSource.PlayOneShot(missAudio);

            //Debug.Log("not equal");
            firstCard.GetComponent<Card>().CloseCard();
            secondCard.GetComponent<Card>().CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }
}
