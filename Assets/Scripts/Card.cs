using UnityEngine;

public class Card : MonoBehaviour
{
    private Animator _anim;
    private AudioClip _flip;

    public void OpenCard()
    {
        GameManager.Instance.audioSource.PlayOneShot(_flip);

        _anim.SetBool("isOpen", true);
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);

        if (GameManager.Instance.FirstCard == null)
        {
            GameManager.Instance.flipCnt = GameManager.Instance._elapsedTime;
            GameManager.Instance.FirstCard = gameObject;
        }
        else
        {
            GameManager.Instance.flipCnt = 0;
            GameManager.Instance.SecondCard = gameObject;
            GameManager.Instance.IsMatched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    private void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    private void CloseCardInvoke()
    {
        _anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
    }
}
