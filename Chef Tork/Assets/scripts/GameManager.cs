using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Apples { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
