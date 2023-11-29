using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBox : MonoBehaviour
{
    public GameObject success;
    public GameObject fail;
    public GameObject penalty;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Success()
    {
        transform.Find("Success").gameObject.SetActive(true);
    }

    public void Fail()
    {
        transform.Find("Fail").gameObject.SetActive(true);
        transform.Find("Penalty").gameObject.SetActive(true);
    }

    public void SuccessClear()
    {
        transform.Find("Success").gameObject.SetActive(false);
    }

    public void FailClear()
    {
        transform.Find("Fail").gameObject.SetActive(false);
        transform.Find("Penalty").gameObject.SetActive(false);
    }
}
