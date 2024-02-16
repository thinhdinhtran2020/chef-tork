using UnityEngine;
using UnityEngine.UI;

public class AppleCollector : MonoBehaviour
{
    [SerializeField] private Text applesCount;

    private void Start()
    {
        UpdateAppleCountUI(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.Apples++; // Use GameManager object/script in every scene to track apples

            UpdateAppleCountUI();
        }
    }

    private void UpdateAppleCountUI()
    {
        int apples = GameManager.Instance.Apples;
        if (apples < 10)
        {
            applesCount.text = "00" + apples;
        }
        else if (apples < 100)
        {
            applesCount.text = "0" + apples;
        }
        else
        {
            applesCount.text = apples.ToString();
        }
    }
}
