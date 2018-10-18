/**********************************************************
���������豸���޹�˾��Ȩ����2016
���ߣ���־�� 
ʱ�䣺2017.03
˵����
	25i�����汾˵��
	gsk25inetfun.dll
	gsk25inetfun.h

	Ŀǰ�ṩͨ��TCP/IP��ȡϵͳ�Ļ������꣬�������꣬�������

	������������PMC��X Y F G R A K����Դ

	�����Լ���������ع���
	�趨PLCλ����
***********************************************************/


#ifndef GSK25INETFUN_H
#define GSK25INETFUN_H


#include <iostream>
//#include <WS2tcpip.h>
#include <WinSock2.h>
#include <MMSystem.h>

#pragma comment(lib, "ws2_32.lib")


#define GSK25IAPI_EXPORTS
#ifdef GSK25IAPI_EXPORTS
#define GSK25I_API __declspec(dllexport)
#else
#define GSK25I_API __declspec(dllimport)
#endif


//CNC����ͨ��ʵ������
typedef  void*  HINSGSKRM;


/* �����붨��, ���ڵ��� 0x8000 �Ĵ������� PC �˲��������� */
#define GSK25I_OK					0x0000	// �ɹ���û�д���

#define GSK25I_ERR_HINST			0x8000	//ʵ��������
#define GSK25I_ERR_REQ				0x8001	//����ͻ���ճ���
#define GSK25I_ERR_AXIS				0x8002	//����������Χ
#define GSK25I_ERR_TYPE				0x8003	//�����ڵ�����

/***********************************************************************************

								���ӹ���ӿ�
***********************************************************************************/

/*
��  �ܣ�����GSK����ͨ��ʵ����Զ��ͨ������
��  ����cncIPAddr : CNC ��IP ��ַ
����	type : ͨѶģʽ  0ΪUDP,1ΪTCP/IP
����ֵ���ɹ�����ͨ��ʵ�������������NULL
��ע�� 988TD֧��UDP �� TCP/IP 2�ַ�ʽ
       25i Ŀǰ֧�� TCP/IP 1�ַ�ʽ*/
extern "C" GSK25I_API HINSGSKRM GSKRM_CreateInstance(unsigned char cncIPAddr[4], int type);


/* 
��  �ܣ���ѯʵ��������״̬
��  ����hInst: CNCԶ�����ͨ��ʵ�����
����ֵ������״̬��-1:��Чʵ���� 0:δ����; 1:�Ѿ����� */
extern "C" GSK25I_API int GSKRM_GetConnectState(HINSGSKRM hInst);


/*
��  �ܣ�Զ�̶Ͽ����Ӳ��ر�GSK����ͨ��ʵ��
��  ����hInst: GSK����ͨ��ʵ�����
����ֵ����*/
extern "C" GSK25I_API int GSKRM_CloseInstance(HINSGSKRM hInst);


/*
��  �ܣ� ����ͨ�ų�ʱʱ��
��  ���� hInst:  GSK����ͨ��ʵ�����
		overtime: ͨ�ų�ʱʱ�䣬��λms
����ֵ���ɹ�����0, ���򷵻ش�����*/
extern "C" GSK25I_API int GSKRM_SetOvertime(HINSGSKRM hInst, unsigned int overtime);

/*
��  �ܣ� ��ȡCNC�ͺ�����
��  ���� hInst:  GSK����ͨ��ʵ�����
		 typeName: ���ڴ�Ż�õ�CNC�ͺ����ƣ������СӦ���ڵ���32
                   �磺��25i������988TD��
����ֵ���ɹ�����0, ���򷵻ش�����*/
extern "C" GSK25I_API int GSKRM_GetCncTypeName(HINSGSKRM hInst, char typeName[32]);


/***********************************************************************************

								ҵ����ӿ�

***********************************************************************************/
#define NET_AXIS_NUM		8
#define NET_GGROUP_NUM		22

/*
��  �ܣ� ��ȡ��ǰϵͳ�İ汾��Ϣ
��  ���� hInst:  GSK����ͨ��ʵ�����
����ֵ���ɹ�����struct VERS_INFOָ��, ���򷵻�NULL*/
struct VERS_INFO
{
	char sysVersion[32];//ϵͳ�汾
	char armVersion[32];//Ӧ�ð汾
	char dspVersion[32];//�岹�汾
	char FPGAVersion[32];//λ�ذ汾
	char plcfileName[32];//��ͼ�ļ���
	char hardVersion[32];//Ӳ���汾  ����
	char softWareNumber[32];//������  ����
	char hardWareNumber[32];//Ӳ�����  ����
};
extern "C" GSK25I_API struct VERS_INFO* GSKRM_GetVersionInfo(HINSGSKRM hInst);

/*
��  �ܣ� ��ȡ��ǰϵͳ��������Ϣ
��  ���� hInst:  GSK����ͨ��ʵ�����
����ֵ���ɹ�����struct RUNSTAT_INFOָ��, ���򷵻�NULL*/
struct RUNSTAT_INFO
{
	char OpMode;							//����ģʽ
	char RunMode;							//����ģʽ
	char EmergFlag;							//��ͣ�ź�����
	char currentalarmflag;					//��ǰ�Ƿ��ڱ���״̬����������
	unsigned short currentAlarmNum;			//��ǰ������
	unsigned short PartCount;				//�ӹ������
	unsigned char Gmode[NET_GGROUP_NUM];	// ��ǰG����ģ̬
	unsigned char GmodeNext[NET_GGROUP_NUM];//�¶�G����ģ̬
	unsigned char SvCurrent[NET_AXIS_NUM];	//�����Ḻ��
    unsigned char SpdCurrent[2];			//���Ḻ��
	char progfileName[32];					//��ǰ�ļ�
	unsigned int run_time;					// ����ʱ��
	unsigned int process_time;				// �ӹ�ʱ��
	unsigned int CNC_time;					//ϵͳʱ��
	unsigned int BlockNumber;				//�к�
	int run_time_cnt;						// �ۼ�����ʱ��
	int process_time_cnt;					// �ۼƼӹ�ʱ��
	int powerup_times_all;					// �ۼ��ܴ���
	int powerup_times_month;				// �ۼƱ��´���
	unsigned char loginLevel;		//��ǰ��½�ȼ�
	unsigned char opt_error;
	unsigned char custom_error;
	char reserver[64];		//����

};
extern "C" GSK25I_API struct RUNSTAT_INFO* GSKRM_GetRunInfo(HINSGSKRM hInst);

/*
��  �ܣ� ��ȡ��ǰϵͳ�ĸ������ꡢ���ʡ�ת����Ϣ
��  ���� hInst:  GSK����ͨ��ʵ�����
����ֵ���ɹ�����struct AXIS_INFOָ��, ���򷵻�NULL*/
struct AXIS_INFO
{
	double Abs_Coord[NET_AXIS_NUM];//��������
	double Rel_Coord[NET_AXIS_NUM];//�������
	double Mac_Coord[NET_AXIS_NUM];//��������
	double Addi_Coord[NET_AXIS_NUM];//ʣ�����
	double Frd;
	int Spd;
	int F;//����
	int S;//ת��!
	int J;//���ٱ���
	
	double ActiveAxisSpeed[NET_AXIS_NUM];	//����ʵ�ʷ��ٶ�
};
extern "C" GSK25I_API struct AXIS_INFO* GSKRM_GetAxisInfo(HINSGSKRM hInst);


/*
��  �ܣ� ��ȡ��ǰϵͳ�ı�������
��  ���� hInst:  GSK����ͨ��ʵ�����
����ֵ���ɹ�����struct ALARM_INFOָ��,���160��
		���򷵻�NULL*/
struct ALARM_INFO
{
	int index;//������
	int axisNo;	//������Ż��վ��
	char ErrorNoStr[8];//������
	char ErrorTime[16];//����ʱ��
	char ErrorMessage[64];//������Ϣ
};
extern "C" GSK25I_API struct ALARM_INFO* GSKRM_GetAlarmInfo(HINSGSKRM hInst,int *retcnt);


/*
��  �ܣ� ��ȡ��ǰϵͳ�ļӹ�����
��  ���� hInst:  GSK����ͨ��ʵ�����
����ֵ���ɹ�����struct PROGRAM_INFOָ��,���160��
		���򷵻�NULL*/
struct PROGRAM_INFO
{
	int	index;//������
	char programName[32];//�ӹ�������
	char programTime[32];//�ӹ�ʱ��(����ʱ��+�ӹ���ʱ)
};
extern "C" GSK25I_API struct PROGRAM_INFO* GSKRM_GetProgramInfo(HINSGSKRM hInst,int *retcnt);


/*
��  �ܣ� ��ȡ��ǰϵͳ�Ĳ�������
��  ���� hInst:  GSK����ͨ��ʵ�����
����ֵ���ɹ�����struct OPERATE_INFOָ��,���160��
		���򷵻�NULL*/
struct OPERATE_INFO
{
	int	index;//������
	char optstr[5][32];
	//0//��������
	//1//�����Ż����λ
	//2//����ǰ��ֵ
	//3//��������ֵ
	//4//����ʱ��
};
extern "C" GSK25I_API struct OPERATE_INFO* GSKRM_GetOperateInfo(HINSGSKRM hInst,int *retcnt);




/*
д��CNC���ݽӿ�
��  �ܣ� ����PLC�Ĵ�������һ���͵�ַ��
��  ���� hInst:  GSK����ͨ��ʵ�����
		type:	��ַ����
	   index:	��ַ���� ���, ƫ�Ƶ�ַ
	   bit:   0-7 ��ȡ�Ĵ��������� (����㿪ʼ�������������)
	   pValue:	0/1	��ǰ���ݻ�����ָ��
����ֵ���ɹ�����0, ���򷵻ش�����*/


extern "C" GSK25I_API int GSKRM_SetPlcDatabit(HINSGSKRM hInst, int type, int index, int bit, unsigned char Value);


/*
д��CNC���ݽӿ�
��  �ܣ� ����PLC�Ĵ�������һ���͵�ַ���ӵ�ַ��㿪ʼ������������ݵ�д��
��  ���� hInst:  GSK����ͨ��ʵ�����
		type:	��ַ����
	   count:	��ַ���� ���
	   num:    ��ȡ�Ĵ��������� (����㿪ʼ�������������)
	   pValue: ��ǰ���ݻ�����ָ��
����ֵ���ɹ�����0, ���򷵻ش�����*/

extern "C" GSK25I_API int GSKRM_SetPlcData(HINSGSKRM hInst, int type, int count, int num, unsigned char dValue[]);


/*
д��CNC���ݽӿ�
��  �ܣ� ���ù���ϵ
��  ���� hInst:  GSK����ͨ��ʵ�����
     char coordType;   //����ϵ����, 0 : EXT   G54 ~ G59;   1 : G54.1
	 char coordNo;		//����ϵ���
     short reserver;	//����
     char axisNo[4];	//���0-8
     double value[4];	//ֵ
����ֵ���ɹ�����0, ���򷵻ش�����
*/

extern "C" GSK25I_API int GSKRM_SetCoordSync(HINSGSKRM hInst, char coordType, char coordNo, char axisNo[], double value[]);

/*
д��CNC���ݽӿ�
��  �ܣ� ���ù���ϵ
��  ���� hInst:  GSK����ͨ��ʵ�����
	int toolNo;//���ߺ�
	int type;//������ÿһ�еı�ž��忴��Ķ���
	double val;	//��λmm
	int optFlag;  //0 ����д�� 1 ����
����ֵ���ɹ�����0, ���򷵻ش�����
*/

extern "C" GSK25I_API int GSKRM_SetOfstSync(HINSGSKRM hInst, int toolNo, int type, int optFlag, double value);


/*
д��CNC���ݽӿ� 
��  �ܣ� �����ݲ�
��  ���� hInst:  GSK����ͨ��ʵ�����
	int pitchNo;//�ݲ���
	int pitchCnt;//����
	int direction;	//1:����/0:����
	int pitchVal;  //�ݲ�ֵ
����ֵ���ɹ�����0, ���򷵻ش�����
*/

extern "C" GSK25I_API int GSKRM_SetPitchInfo(HINSGSKRM hInst, int pitchNo, int pitchCnt, int direction, int pitchVal);


struct PITCH_ARRAY{
    unsigned short cnt;		//С�ڵ���8
    short direction;	//0 ����  1  ����
    short num[8];
    short val[8];
};	

/*
��CNC���ݽӿ� 
��  �ܣ� ��ȡ�ݲ�
��  ���� hInst:  GSK����ͨ��ʵ�����
	int flag;//1:����/0:����
	int cnt;//����
	int pparamNum;	//�ݲ���
����ֵ���ɹ�����0, ���򷵻�NULL
*/
extern "C" GSK25I_API struct PITCH_ARRAY* GSKRM_GetPitchInfo(HINSGSKRM hInst, int flag, int cnt, int *pparamNum);

/*
��CNC������ӿ� 
��  �ܣ� ��ȡ�����ֵ
��  ���� hInst:  GSK����ͨ��ʵ�����
	int macroCnt;//���������
	int *macroNo;//��������
	int *macroVal;	//�����ֵ
����ֵ���ɹ�����0, ���򷵻ش����
*/

extern "C" GSK25I_API int GSKRM_GetMacroInfo(HINSGSKRM hInst, int macroCnt, int *macroNo, double *macroVal);

/*
����CNC����� 
��  �ܣ� ���ú����
��  ���� hInst:  GSK����ͨ��ʵ�����
	int macroCnt;//���������
	int *macroNo;//��������
	int *macroVal;	//�����ֵ
����ֵ���ɹ�����0, ���򷵻ش����
*/

extern "C" GSK25I_API int GSKRM_SetMacroInfo(HINSGSKRM hInst, int macroCnt, int *macroNo, double *macroVal);


/*
����CNC����
��  �ܣ� ���ò���
��  ���� hInst:  GSK����ͨ��ʵ�����
	int number;//������
	int valcnt;//����
	int *pval;	//��Ҫ�޸ĵĲ���ֵ
����ֵ���ɹ�����0, ���򷵻ش����
*/

extern "C" GSK25I_API int GSKRM_SetParamOpt(HINSGSKRM hInst, int number, int valcnt, double *pval);

/*
��CNC�����ӿ�
��  �ܣ� ��ȡ����,λ�����ֽڵ�ʮ����ȡֵ
��  ���� hInst:  GSK����ͨ��ʵ�����
	int number;//������
	int valcnt;//����
	int *pval;	//��Ų���ֵ
����ֵ���ɹ�����0, ���򷵻ش����
*/

extern "C" GSK25I_API int GSKRM_GetParamInfo(HINSGSKRM hInst, int number, int valcnt, double *pval);


/*
�ϴ�G�����ļ�,�ļ�����׺�����Ǵ�дNC, ��: 55.NC
��  �ܣ� �ϴ��ļ�
��  ���� hInst:  GSK����ͨ��ʵ�����
	char *filePath;//�ļ�·�����ļ���
	bool isDNC; 1: DNC���� 0: �ϴ��ļ�
����ֵ���ɹ�����0, ���򷵻ش����
*/

extern "C" GSK25I_API int GSKRM_SetPutFile(HINSGSKRM hInst, char *filePath, bool isDNC);



extern "C" GSK25I_API int GSKRM_SetFolderOpt(HINSGSKRM hInst, int optType, int fileType, char *path);



/*
 *�ŷ����ݼ�أ�DSP���ݽṹ
 */
struct DspData
{
    double ServoFeedBack[5];	//�ŷ�������λ��������
    double CmdPos[5];

    float cmd_speed;		// ָ���ٶ�
    int  block_num;			//�к�
};

/*
 *�ŷ����ݼ�أ��ŷ����ݽṹ
 */
struct ServoData
{
    float ServoFeedBack[2];	//�ŷ�������λ��������
    float CmdPos[2];
    float PidCmdPos[2];         //λ��PID�����õ�ָ��ֵ
    float CodPos[2];           //��դ����λ��ֵ
    short ServoCmdVel[2];     //�ŷ�PIDָ���ٶȣ�0.1rmp
    short ServoSpeed[2];       //�ŷ�ʵ���ٶ�
    short ServoCmdCurrent[2];  //�ŷ�ָ�����, 0.1A
    short ServoCurrent[2];     //�ŷ�ʵ�ʵ���

    int SpindleCmdPulse;   //pulse
    int SpindleFdbPulse;
    int SpindleCmdSpeed;   //0.1rpm
    int SpindleFdbSpeed;
    short SpindleCmdCurrent; //0.1A
    short SpindleFdbCurrent;
    short SpindleTapPosErr;  //pulse
    short resved;		//����
};                          //�˽ṹ���ڱ���ϵͳ�д��͹������ŷ�������Ϣ
/*
 *�ŷ����ݼ��
 */
struct PosUnit
{
    union
    {
        struct DspData DspData;
        struct ServoData ServoData;
    };
};             //�������紫�͵Ĳ���ѡ����ʵ�����


extern "C" GSK25I_API int reqRunMonitor(HINSGSKRM hInst, int monitorType, int path, int spindle, int axis1, int axis2, int *cnt, struct PosUnit Pos[]);

        //================================================================
        //��������

        //��	��������������������
        //��	�����ֱ��ǣ���������������0 �رռ���1�ŷ�����2DSP�㷨����
        //				ͨ����0/1		�����0/1		�������0-7
        //����	ֵ��0 : �ɹ�����0 ʧ��
        //int reqRunMonitor(int monitorType, int path, int spindle, int axis1, int axis2);

        //��	��������ֹͣ��������
        //��	����
        //����	ֵ��0 : �ɹ�����0 ʧ��
        //void ();
extern "C" GSK25I_API int reqStopMonitor(HINSGSKRM hInst);

#endif