using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] playerCharacterPrefabs;
    int characterIndex;
    [SerializeField] GameObject startPoints;
    public Vector2 currentPos;

    private void Awake()
    {
        currentPos = startPoints.transform.position;
    }

    public GameObject SpawnPlayer()
    {
        characterIndex = PlayerPrefs.GetInt("Selected Character", FindObjectOfType<GameManager>().GetSkinIndex());
        return Instantiate(playerCharacterPrefabs[characterIndex], lastCheckPointPos(currentPos), Quaternion.identity);
    }

    public Vector2 lastCheckPointPos(Vector2 point)
    {
        return point;
    }

    public void Respawn()
    {
        if (FindObjectOfType<Player>().isAlive == false)
        {
            FindObjectOfType<Player>().transform.position = currentPos;
        }
    }
}
