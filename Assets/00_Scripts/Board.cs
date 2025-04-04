using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;
    void Start()
    {
        // 배열 크기는 초기화할 때 정해짐
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        // Linq를 사용한 셔플
        // OrderBy(조건): 정렬
        // x => Random.Range(0f, 7f) : 배열을 순서대로 순회하면서 Random.Range(0f, 7f)의 값에 따라서 얼만큼 작고 크냐에 따라서 해당 요소의 우선순위가 결정되고 그 우선순위에 따라서 정렬됨
        // ToArray(): orderBy를 쓰면 자료형이 Array가 아니기때문에 
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            // 두 번째 transform 매개변수는 부모이고 그 부모 아래 생성하라는 의미
            GameObject go = Instantiate(card, this.transform);

            // 카드사이즈가 x: 1.3, y: 1.3 이라서 1.4 만큼 띄워서 카드 배치
            // idea: 몫과 나머지에 1.4를 곱해서 시작 위치를 구한다!
            // 카드 전체 위치 조정을 위해서 x, y 각각 -2.1f, -3.0f 
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.Instance.cardCount = arr.Length;
    }
}
