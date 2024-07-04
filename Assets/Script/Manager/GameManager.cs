using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    int skinIndex;
    public bool isFinishLevel;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetSkinIndex()
    {
        return skinIndex;
    }

    public void SetSkinIndex(int index)
    {
        skinIndex = index;
    }

    public bool GetUnlockedLevels()
    {
        return isFinishLevel;
    }
}
