using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCam : MonoBehaviour
{
    #region field
    [Header("¼³Á¤")]
    [SerializeField] private Transform target;

    private float followSpeed = 5.0f;
    #endregion

    void LateUpdate()
    {
        if (!InteractionManager.Instance.changeScene)
        {
            Follow();
        }
    }

    #region method
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        GameObject target = GameObject.Find("Player");

        if (target != null) this.target = target.GetComponent<Transform>();
    }

    private void Follow()
    {
        if (target == null) return;

        Vector3 targetPos = transform.position;

        targetPos.x = target.position.x;
        targetPos.y = target.position.y;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime
            );
    }
    #endregion
}
