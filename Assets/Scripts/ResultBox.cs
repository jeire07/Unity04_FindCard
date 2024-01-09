using UnityEngine;

public class ResultBox : MonoBehaviour
{
    public GameObject Success {  get; private set; }
    public GameObject Fail { get; private set; }
    public GameObject Penalty { get; private set; }

    public void MatchSuccess()
    {
        transform.Find("Success").gameObject.SetActive(true);
    }

    public void MatchFail()
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
