using UnityEngine;

public class BackgroundImage : MonoBehaviour
{
    #region field
    [SerializeField] private float MoveSpeed;
    #endregion
    private void Update()
    {
        ImageMove();
    }

    #region method
    private void ImageMove()
    {
        //ImageMove
        transform.position += Vector3.left * MoveSpeed * Time.deltaTime;

        //좌표값이 -x축으로 무한정 늘어나는걸 방지
        if(transform.position.x < -18.0f)
        {
            transform.position = new Vector2(0, transform.position.y);
        }
    }
    #endregion
}
