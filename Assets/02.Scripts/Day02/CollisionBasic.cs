using UnityEngine;
/*
[Collider]
-�������� �浹�̳� Ʈ���� �̺�Ʈ�� �߻����� �� ȣ��
-���� ���� ��� �״�ΰ� �ƴ϶� �ܼ�ȭ��, �ڽ�, ��, ĸ�� ������ ó��

[���� �浹 �̺�Ʈ]
-private void OnCollisionEnter(Collision collision){} //ó�� �浹�� �߻��Ǿ��� ��
-private void OnCollisionStay(Collision collision){} //�浹�� �����Ǵ� ���� �� ������ ���� ȣ��
-private void OnCollisionExit(Collision collision){} //�浹�� ������ ��

[� ��쿡 ���ɱ�?]
-������ ������ �ʿ��� ���
-ĳ���Ͱ� ���� �ε��� ��, �̵��� ���߰��� Ư�� ������ �ؾ��� ��
-����ü�� ���̳� ���� �浹�ϸ� �ı��Ǵ� �Ͽ�
-�÷��̾ Ư�� ������Ʈ�� ������ �浹�� �Ͼ�� ���
-�����ؼ� �ٴڿ� ã���ϴ� ���

[Ʈ���� �̺�Ʈ]
-private void OnTriggerEnter(Collider other){} //Ʈ���� ������ ó�� �������� ��
-private void OnTriggerStay(Collider other){} //Ʈ���� ������ �ӹ����� ����
-private void OnTriggerExit(Collider other){} //Ʈ���� �������� ����� ��

[� ��쿡 ���ɱ�?]
-������ ������ �ʿ���� ���(�浹���� ������ �ʿ��� ���)
-�ڷ���Ʈ ���
-������ ����
*/

