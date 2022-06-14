using UnityEngine;
using UnityEngine.UI;

public class Entrance : MonoBehaviour
{
    [SerializeField] bool active;
    [SerializeField] int xmod, ymod;
    [SerializeField] int id;

    public void Deactivate()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        active = false;
    }

    public void Activate()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        active = true;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        ButtonClick b = FindObjectOfType<ButtonClick>();
        if (active && collision.CompareTag("Player") &&
            ((Application.isMobilePlatform && b.IsCliked && b.Key == KeyCode.Space) ||
            (!Application.isMobilePlatform) && Input.GetKey(KeyCode.Space)))
        {
            Deactivate();
            FindObjectOfType<LevelController>().WalkToPart1(xmod, ymod, id);
        }
    }
}
