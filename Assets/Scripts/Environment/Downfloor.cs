using UnityEngine;

public class Downfloor : MonoBehaviour
{

    bool isOpen = false;
    bool stopWatchingButton = false;

    void Start()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void Open()
    {
        isOpen = true;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DownfloorHole");
        GameObject g = new GameObject();
        g.transform.parent = transform;
        g.AddComponent<SpriteRenderer>().sortingOrder = -1; 
        g.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DownfloorOppened");
        g.transform.localScale = new Vector3(1f, 1f);
        g.transform.position = transform.position;

        GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ButtonClick b = FindObjectOfType<ButtonClick>();
        if (!stopWatchingButton && collision.CompareTag("Player") &&
            ((Application.isMobilePlatform && b.IsCliked && b.Key == KeyCode.Space) ||
            (!Application.isMobilePlatform) && Input.GetKey(KeyCode.Space)))
        {
            stopWatchingButton = true;
            FindObjectOfType<LevelController>().NewLevelPart1();
            GameController.Score =
                (int)(GameController.Score * 1.2) +
                300 * FindObjectOfType<GameController>().currentLevel;
        }
    }
}
