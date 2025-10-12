using Unity.VisualScripting;
using UnityEngine;

public class ClearBox : MonoBehaviour
{
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager.coin != 20)
        {
            sr.color = Color.red;
        }
        else if (gameManager.coin == 20)
        {
            sr.color = Color.green;
        }
    }
}
