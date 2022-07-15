using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFlux : MonoBehaviour
{
    public Transform player;
    public float maxDis = 5;

    private SmoothFollowerObj posFollow;//����λ��ƽ���ƶ�
    private SmoothFollowerObj lookFollow;//���Ƴ���ƽ��ת��


    public Vector3 positionVector;//��ɫλ���ƶ���ʱ�򣬷�������
    public Vector3 lookVector;//��ɫ����仯��ʱ�򣬳�������

    private Vector3 lastVelocityDir;//��һ���ƶ��ķ���
    private Vector3 lastPos;//֮ǰ�ƶ���Ŀ���λ��

    // Use this for initialization
    void Start()
    {
        posFollow = new SmoothFollowerObj(0.5f, 0.5f);
        lookFollow = new SmoothFollowerObj(0.1f, 0.0f);
        posFollow.Update(transform.position, 0, true);//��ʼ����ֵ
        lookFollow.Update(player.transform.position, 0, true);

        //positionVector = new Vector3(0, 0.5f, 1.7f);
        lookVector = new Vector3(0, 0, 1.5f);

        lastVelocityDir = player.transform.forward;
        lastPos = player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(transform.position, player.position);
        if (dis > maxDis)//�����Һͳ���֮��ľ����������������룬���Ƴ���������ƶ�
        {
            PetMoveFlow();//�����ƶ����߼�
        }
        else
        {

        }

        transform.LookAt(player.position, Vector3.up);

    }

    private void PetMoveFlow()
    {
        lastVelocityDir += (player.transform.position - lastPos) * 5;
        lastPos = player.transform.position;
        lastVelocityDir += player.transform.forward * Time.deltaTime;
        lastVelocityDir = lastVelocityDir.normalized;
        Vector3 horizontal = transform.position - player.transform.position;
        Vector3 horizontal2 = horizontal;
        Vector3 vertical = player.transform.up;
        Vector3.OrthoNormalize(ref vertical, ref horizontal2);
        if (horizontal.sqrMagnitude > horizontal2.sqrMagnitude) horizontal = horizontal2;
        transform.position = posFollow.Update(
            player.transform.position + horizontal * Mathf.Abs(positionVector.z) + vertical * positionVector.y,
            Time.deltaTime
        );

        horizontal = lastVelocityDir;
        Vector3 look = lookFollow.Update(player.transform.position + horizontal * lookVector.z - vertical * lookVector.y, Time.deltaTime);
        transform.rotation = Quaternion.FromToRotation(transform.forward, look - transform.position) * transform.rotation;
    }

    class SmoothFollowerObj
    {

        private Vector3 targetPosition;
        private Vector3 position;
        private Vector3 velocity;
        private float smoothingTime;
        private float prediction;

        public SmoothFollowerObj(float smoothingTime)
        {
            targetPosition = Vector3.zero;
            position = Vector3.zero;
            velocity = Vector3.zero;
            this.smoothingTime = smoothingTime;
            prediction = 1;
        }

        public SmoothFollowerObj(float smoothingTime, float prediction)
        {
            targetPosition = Vector3.zero;
            position = Vector3.zero;
            velocity = Vector3.zero;
            this.smoothingTime = smoothingTime;
            this.prediction = prediction;
        }

        // ����λ����Ϣ
        public Vector3 Update(Vector3 targetPositionNew, float deltaTime)
        {
            Vector3 targetVelocity = (targetPositionNew - targetPosition) / deltaTime;//��ȡĿ���ƶ��ķ�������
            targetPosition = targetPositionNew;

            float d = Mathf.Min(1, deltaTime / smoothingTime);
            velocity = velocity * (1 - d) + (targetPosition + targetVelocity * prediction - position) * d;

            position += velocity * Time.deltaTime;
            return position;
        }

        //���ݴ��ݽ��������ݣ����ñ��ز���
        public Vector3 Update(Vector3 targetPositionNew, float deltaTime, bool reset)
        {
            if (reset)
            {
                targetPosition = targetPositionNew;
                position = targetPositionNew;
                velocity = Vector3.zero;
                return position;
            }
            return Update(targetPositionNew, deltaTime);
        }

        public Vector3 GetPosition() { return position; }
        public Vector3 GetVelocity() { return velocity; }
    }
}
