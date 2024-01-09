using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Card { get; set; }
    public GameObject FirstCard { get; set; }
    public GameObject SecondCard { get; set; }
    public GameObject EndTxt { get; set; }
    public Text TimeCnt { get; set; }
    public Text TryCnt { get; set; }
    
    public static GameManager Instance { get; set; }
    
    public AudioClip matchAudio;
    public AudioClip missAudio;
    public AudioClip clearAudio;
    public AudioClip failAudio;
    public AudioSource audioSource;
    
    public float _elapsedTime { get; set; }
    
    public float flipCnt { get; set; }
    
    private float _remainTime = 50.0f;
    private int _count = 0;
    
    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

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
            GameObject newCard = Instantiate(Card);
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
        _elapsedTime += Time.deltaTime;

        if ( (_remainTime - _elapsedTime) <= 0.0f)
        {
            audioSource.PlayOneShot(failAudio);
            EndTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
        TimeCnt.text = (_remainTime - _elapsedTime).ToString("N2");

        if (FirstCard == null)
        {
            flipCnt = _elapsedTime;
        }
        else if ((flipCnt - _elapsedTime) > 5.0f)
        {
            FirstCard.GetComponent<Card>().CloseCard();
            FirstCard = null;
        }
    }

    public void IsMatched()
    {
        _count += 1;
        TryCnt.text = _count.ToString();
        string firstCardImage = FirstCard.transform.Find("front")
            .GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = SecondCard.transform.Find("front")
            .GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(matchAudio);
            //Debug.Log("equal");
            FirstCard.GetComponent<Card>().DestroyCard();
            SecondCard.GetComponent<Card>().DestroyCard();

            int cardsLeft = GameObject.Find("Cards")
                .transform.childCount;
            //Debug.Log(cardsLeft);
            if (cardsLeft == 2)
            {
                audioSource.PlayOneShot(clearAudio);
                EndTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            _elapsedTime += 0.5f;
            audioSource.PlayOneShot(missAudio);

            //Debug.Log("not equal");
            FirstCard.GetComponent<Card>().CloseCard();
            SecondCard.GetComponent<Card>().CloseCard();
        }

        FirstCard = null;
        SecondCard = null;
    }
}
