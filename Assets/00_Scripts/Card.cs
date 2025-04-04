using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front;
    public GameObject back;

    public Animator anim;

    AudioSource audioSource;
    public AudioClip clip;

    public SpriteRenderer frontImage;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Setting(int number)
    {
        idx = number;
        // Rtan 이미지의 Texture Type이 Sprite
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }

    public void OpenCard()
    {
        // 두 개 이상 뒤집기 방지
        if (GameManager.Instance.secondCard != null) return;

        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        // firstCard가 비었다면 현재 카드 할당
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            // firstCard가 비어있지 않다면 secondCard에 현재 카드 할당 -> Mached 함수 호출
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1f);
    }

    public void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
