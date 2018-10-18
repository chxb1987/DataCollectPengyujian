
// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the NCCONNECTDLL_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// NCCONNECTDLL_API functions as being imported from a DLL, wheras this DLL sees symbols
// defined with this macro as being exported.
#ifdef NCCONNECTDLL430_EXPORTS
#define NCCONNECTDLL430_API __declspec(dllexport)
#else
#define NCCONNECTDLL430_API __declspec(dllimport)
#endif

#include "time.h"


//-------------------------------------------------
//				�ӷ����������ļ�
//
// ����˵��:
//
// localPathPtr ����·��,������Ŵӷ��������ص��ļ�
// fileNamePtr  Ҫ�����ļ������֣�����·����
// ipAddrPtr    ������IP��ַ
// server_port  �������˿�
// iFileType    ��Ҫ ���� ���ļ����� 
//
// ����ֵ˵��:  
//
//------------------------------------------------
 int __stdcall  download430FileFromServer(char *localPathPtr,char *fileNamePtr, char* ipAddrPtr,int server_port = 6666,int iFileType=0);

 
//-------------------------------------------------
//				��������ϴ��ļ�
//
// ����˵��:
//
// localPathPtr ����·��,������λҪ�ϴ����ļ�
// fileNamePtr  Ҫ�ϴ��ı����ļ���������·����
// ipAddrPtr    ������IP��ַ
// server_port  �������˿�
// iFileType    ��Ҫ �ϴ� ���ļ����� 
//
// ����ֵ˵��:  
//
//-------------------------------------------------
 int __stdcall  upload430FileToServer(char *localPathPtr,char *fileNamePtr, char* ipAddrPtr,int server_port = 6666,int iFileType=0);

 //------------------------------------------------
 //        ��ȡNC״̬
 //
 // ����˵��
 // index Ҫ��ȡ��״̬�ı��
 // ����ֵ˵��:
 // 
 //
 //------------------------------------------------
  typedef struct
{
  double x, y, z;                  

} PmCartesia;
 
 typedef struct _CncPose {
  PmCartesia tran;  
  double a, b, c;    
  double u, v, w;   
} CncPose;




int __stdcall get430IOStatusVal(int iNCIndex,int iIOIndex);
int __stdcall get430StatusStrVal(int iNCIndex,int index,char* retString);
int __stdcall get430StatusArrayVal(int iNCIndex,int index,int* retArray);
int __stdcall get430StatusStaticVal(int iNCIndex,int index,char* retString);
int __stdcall get430StatusVal(int iNCIndex,int index,double * retValue);
int __stdcall get430StatusTimeVal(int iNCIndex,int index,time_t * retTimeValue);
int __stdcall get430StatusAxisVal(int iNCIndex,int index,double * retAxisValue);
int __stdcall get430StatusCoordVal(int iNCIndex,int index,CncPose * retCoordValue);
int __stdcall get430ErrorVal(int iNCIndex,char* retString);
int __stdcall get430AutoMotionErrorVal(int iNCIndex,char* retString);
int __stdcall get430AutoPlcErrorVal(int iNCIndex,char* retString);

int __stdcall send430CmdToNC( int iNCIndex,int iCmdType,char * strCmdComment );
int __stdcall connect430ToNC( int *iNCIndex ,char * IPAddr = NULL, unsigned short Port = 0);
int __stdcall disconnect430ToNC( int iNCIndex );


