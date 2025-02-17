using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ͼ�ӿ�
/// </summary>
public interface IBaseView
{
    bool IsInit();//�Ƿ��ʼ��
    bool IsShow();//�Ƿ���ʾ

    void InitUI();//��ʼ����ͼ

    void InitData();//��ʼ������

    void Open(params object[] args);//����ͼ

    void Close(params object[] args);//�ر���ͼ

    void DestroyView();//������ͼ

    void ApplyFunc(string eventName, params object[] args);//���������¼�

    void ApplyControllerFunc(int controllerId, string eventName, params object[] args);//�����������¼�

    void SetVisable(bool isVisable);//������ʾ����

    int ViewId { get; set ; }//��ͼID

    BaseController Controller { get ; set ; }//��ͼ����������

}
