using UnityEngine;

public class BackGroundImage : MonoBehaviour
{
    #region field
    [SerializeField] public float moveSpeed;
    #endregion

    void Update()
    {
        ImageMove();
    }

    #region method
    private void ImageMove()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < -18.0f)
        {
            transform.position = new Vector2(0, transform.position.y);
        }
    }
    #endregion
}
