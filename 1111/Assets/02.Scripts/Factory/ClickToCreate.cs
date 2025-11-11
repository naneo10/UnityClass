using System.Collections.Generic;
using UnityEngine;

public class ClickToCreate : MonoBehaviour
{
    [SerializeField] private LayerMask m_layerToClick;
    [SerializeField] private Vector3 m_Offset; //생성 위치에 더해줄 오프셋

    [SerializeField] private Factory[] m_Factories;
    private List<GameObject> m_createdProducts = new List<GameObject>();

    void Update()
    {
        GetProductAtClick();
    }

    private void GetProductAtClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_Factories == null || m_Factories.Length == 0) return;
            if (Camera.main == null) return;

            Factory selectedFactory = m_Factories[Random.Range(0, m_Factories.Length)];

            //화면에서 마우스 위치를 3D 공간좌표로 선 하나를 생성
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //레이캐스트: 
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_layerToClick) 
                && selectedFactory != null)
            {
                IProduct product = selectedFactory.GetProduct(hit.point + m_Offset);

                if (product is Component component)
                {
                    m_createdProducts.Add(component.gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject product in m_createdProducts)
        {
            if (product != null)
            {
                Destroy(product);
            }
        }
        m_createdProducts.Clear();
    }
}
