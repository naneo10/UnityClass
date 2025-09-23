using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
���Ϸ� ��(Euler Angle): 3���� �������� ȸ���� ǥ���ϴ� ���
���� ȸ���� ǥ���ϴ� ���. x,y,z �࿡ ���� �������� ȸ�� ���� ����Ͽ� ��ü�� ȸ���� ����
������������, ������ ������ �߻��� �� ����

���ʹϾ�(Quaternion): ȸ���� ǥ���ϴ� �� �ٸ� ���. ������ ������ ���� �� �ִ�
4���� ���Ҽ��� ǥ�� ��. (x, y, z, w) 
���������� ����
*/

public class Rotatecube : MonoBehaviour
{
    /*
    [transform.Rotate]
    :Rotate(x, y, z, space.self)[default ��]�Ǵ� Rotate(x, y, z, space.world)�� ���� ������ �� ����
    -���� ȸ�� ������ �Էµ� Vector3��ŭ �� ȸ��(����)

    [fransform.rotation]
    :Quaternion.Euler(x, y, z)�� ����� Ư�������� ���� ����
    -���� ȸ���� �������� �ʰ�, ���� ����� ���

    [transform.localRotation]
    :rotation�� ���������� �θ� ������Ʈ�� ������ �޴� ���� ȸ�� ���� ����
    -�θ� ������Ʈ�� ȸ���ϸ� ������� ȸ�� ���� ����� �� ����

    [transform.LookAt]
    :Ư�� Ÿ���� �ٶ󺸰� ����
    -3D ���ӿ��� ���� �÷��̾ ���ϵ��� ���� �� ���

    [transform.RotateAround]
    :Ư�� ��ġ�� �߽����� ������Ʈ�� ȸ��
    -ī�޶� Ư�� ������Ʈ�� ���� �����̳� ���� �˵� ȸ�� � Ȱ���� ����

    [Quaternion.LookRotation
    :LookAt�� ���������� �� ������ ���
    -Ư�� ������ ���ϰ� �� �� ���
    */

    public Transform target;

    void Update()
    {
        transform.RotateAround(target.position, Vector3.down, 30.0f * Time.deltaTime);

        transform.Rotate(transform.up * -30.0f * Time.deltaTime); //������Ʈ�� ���� y�� //���� ����� ���� ����
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 50.0f, 0.0f)); //������Ʈ ���� ������� y������ ȸ��
    }
}
