using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadCreator : MonoBehaviour
{
    public GameObject QuadPrefab;
    public GameObject RingPrefab;
    public GameObject PlanePrefab;

    public int SumLength = 30;

    private int m_quadCount = 12;
    private float m_radius = 5.0f;
    public float Width => QuadPrefab.transform.localScale.x;

    private List<GameObject> m_rings = new List<GameObject>();

    private void Awake()
    {
        CalculateRadius();

        for(int i = 0; i < SumLength; i++)
        {
            GenerateRing(new Vector3(transform.position.x, transform.position.y, transform.position.z + i * Width));
        }
        
    }

    private void CalculateRadius()
    {
        // ���ݱ߳�����뾶
        float angle = 360.0f / m_quadCount; // ÿ���ǵĶ���
        float halfAngle = angle * 0.5f; // ÿ����ǵĶ���
        float halfWidth = Width * 0.5f; // Quad�İ���߳�
        // ���к�������뾶
        float radius = halfWidth / Mathf.Tan(Mathf.Deg2Rad * halfAngle);
        m_radius = radius;
    }

    private void GenerateRing(Vector3 centerPos)
    {
        GameObject ring = Instantiate(PlanePrefab, centerPos, Quaternion.identity);
        ring.transform.SetParent(this.transform);
        m_rings.Add(ring);

        #region 12����Ƭ�ɻ�
        //float angleStep = 360.0f / m_quadCount;
        //for (int i = 0; i < m_quadCount; i++)
        //{
        //    float angle = i * angleStep;
        //    Vector3 position = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle) * m_radius,
        //                                    Mathf.Cos(Mathf.Deg2Rad * angle) * m_radius,
        //                                    centerPos.z);
        //    Quaternion rotation = Quaternion.LookRotation(position - centerPos, Vector3.up);
        //    GameObject quad = Instantiate(RingPrefab, position, rotation);
        //    quad.transform.parent = transform;
        //    m_quads.Add(quad);
        //}
        #endregion

        #region 12����Ƭ����
        //float startX = centerPos.x - m_quadCount * Width / 2f;
        //for (int i = 0; i < m_quadCount; i++)
        //{
        //    Vector3 position = new Vector3(startX + i * Width, centerPos.y - 2.5f, centerPos.z);
        //    Quaternion rotation = Quaternion.LookRotation(Vector3.down);
        //    GameObject quad = Instantiate(QuadPrefab, position, rotation);
        //}
        #endregion
    }

    public float MoveSpeed = 5f;
    private Queue<GameObject> m_waitDelete = new Queue<GameObject>();
    private Queue<float> m_waitGeneratePos = new Queue<float>();
    private void Update()
    {
        m_rings.ForEach((ring) =>
        {
            //ȫ��λ��
            ring.transform.position -= new Vector3(0, 0, MoveSpeed * Time.deltaTime);

            //ĩβ��Ƭ��ɾ��
            if(ring.transform.position.z < -1)
            {
                m_waitDelete.Enqueue(ring);
                m_waitGeneratePos.Enqueue(ring.transform.position.z + (SumLength - 1) * Width);
            }
        });

        //ɾ����ɾ����Ƭ
        while (m_waitDelete.Count > 0)
        {
            GameObject ring = m_waitDelete.Dequeue();
            m_rings.Remove(ring);
            Destroy(ring);
        }

        //���ɶ�Ӧ��Ƭ
        while (m_waitGeneratePos.Count > 0)
        {
            float z = m_waitGeneratePos.Dequeue();
            GenerateRing(new Vector3(transform.position.x, transform.position.y, z));
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("��ʼ�任");


        }
    }

    private void ChangeTubeShape()
    {



    }

}