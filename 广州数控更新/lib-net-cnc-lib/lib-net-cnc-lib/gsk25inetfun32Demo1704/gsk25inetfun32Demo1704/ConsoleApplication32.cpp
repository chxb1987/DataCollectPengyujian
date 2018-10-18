// Test25iLib.cpp : 定义控制台应用程序的入口点。
//

//#include <stdio.h>

#include <time.h>
#include "gsk25inetfun.h"

#include <windows.h>

static void gsk25i_offline( HINSGSKRM hInst)
{
	std::cout<<(int)hInst<<"disconnected"<<std::endl;
}

static void gsk25i_verInfo(HINSGSKRM hInst)
{
	struct VERS_INFO *verInfo = GSKRM_GetVersionInfo(hInst); 
	if(verInfo != NULL)
	{
		std::cout<<"sysVersion:"<<verInfo->sysVersion<<std::endl;
		std::cout<<"armVersion:"<<verInfo->armVersion<<std::endl;
		std::cout<<"dspVersion:"<<verInfo->dspVersion<<std::endl;
		std::cout<<"FPGAVersion:"<<verInfo->FPGAVersion<<std::endl;
		std::cout<<"plcfileName:"<<verInfo->plcfileName<<std::endl;
		std::cout<<"hardVersion:"<<verInfo->hardVersion<<std::endl;
		std::cout<<"softWareNumber:"<<verInfo->softWareNumber<<std::endl;
		std::cout<<"hardWareNumber:"<<verInfo->hardWareNumber<<std::endl;
	}

}

static void gsk25i_runInfo(HINSGSKRM hInst)
{

	struct RUNSTAT_INFO *runInfo = GSKRM_GetRunInfo(hInst); 
	if(runInfo != NULL)
	{
		std::cout<<"运行时间:"<<runInfo->run_time/3600<<"H"<<(runInfo->run_time/60)%60<<"M"<<runInfo->run_time%60<<std::endl;
		std::cout<<"加工时间:"<<runInfo->process_time/3600<<"H"<<(runInfo->process_time/60)%60<<"M"<<runInfo->process_time%60<<std::endl;
		std::cout<<"系统时间:"<<runInfo->CNC_time/3600<<"H"<<(runInfo->CNC_time/60)%60<<"M"<<runInfo->CNC_time%60<<std::endl;
		std::cout<<"累计运行时间:"<<runInfo->run_time_cnt/3600<<"H"<<(runInfo->run_time_cnt/60)%60<<"M"<<runInfo->run_time_cnt%60<<std::endl;
		std::cout<<"累计加工时间:"<<runInfo->process_time_cnt/3600<<"H"<<(runInfo->process_time_cnt/60)%60<<"M"<<runInfo->process_time_cnt%60<<std::endl;
		std::cout<<"累计总次数:"<<runInfo->powerup_times_all<<std::endl;
		std::cout<<"累计本月次数:"<<runInfo->powerup_times_month<<std::endl;
	}

}


static void gsk25i_axisInfo(HINSGSKRM hInst)
{
	int x=0;
	struct AXIS_INFO *axisInfo = GSKRM_GetAxisInfo(hInst); 
	if(axisInfo != NULL)
	{
		for(x=0; x<NET_AXIS_NUM; x++)
			std::cout<<"abs["<<x<<"]="<<axisInfo->Abs_Coord[x]<<std::endl;
		for(x=0; x<NET_AXIS_NUM; x++)
			std::cout<<"rel["<<x<<"]="<<axisInfo->Rel_Coord[x]<<std::endl;
		for(x=0; x<NET_AXIS_NUM; x++)
			std::cout<<"mac["<<x<<"]="<<axisInfo->Mac_Coord[x]<<std::endl;
		for(x=0; x<NET_AXIS_NUM; x++)
			std::cout<<"addi["<<x<<"]="<<axisInfo->Addi_Coord[x]<<std::endl;
	}
}

static void gsk25i_almInfo(HINSGSKRM hInst)
{
	int cnt=0, x=0;
	struct ALARM_INFO *almInfo = GSKRM_GetAlarmInfo(hInst,&cnt);
	std::cout<<"GSK25I_GetAlarmInfo="<<cnt<<std::endl;
	for(x=0; x<cnt; x++)
	{
		if(almInfo == NULL)
			break;
		std::cout<<"almInfo["<<almInfo->index<<"]="<< \
			almInfo->ErrorNoStr<<"="<<almInfo->axisNo<< \
			"="<<almInfo->ErrorMessage<<"="<<almInfo->ErrorTime<<std::endl;
		almInfo ++;
	}

}

static void gsk25i_prgInfo(HINSGSKRM hInst)
{
	int cnt=0, x=0;
	struct PROGRAM_INFO *prgInfo = GSKRM_GetProgramInfo(hInst,&cnt);
	std::cout<<"GSK25I_GetProgramInfo="<<cnt<<std::endl;
	for(x=0; x<cnt; x++)
	{
		if(prgInfo == NULL)
			break;
		std::cout<<"prgInfo["<<prgInfo->index<<"]="<< \
			prgInfo->programName<<"="<<prgInfo->programTime<<std::endl;
		prgInfo ++;
	}
}

static void gsk25i_nameInfo(HINSGSKRM hInst)
{

	char name[32];
	memset(name, 0, 32);
	GSKRM_GetCncTypeName(hInst, name);
	std::cout<<"name: "<<name<<std::endl;

}

static void gsk25i_ParamInfo(HINSGSKRM hInst)
{
	int errno = 1;
	double pval[8];//8个轴型参数
	errno = GSKRM_GetParamInfo(hInst, 1862, 3, pval);
	if(errno == 0)
	{
		std::cout<<"Param get yes:"<<pval[0]<<std::endl;
		std::cout<<"Param get yes:"<<pval[1]<<std::endl;
		std::cout<<"Param get yes:"<<pval[2]<<std::endl;
	}
	else
		std::cout<<"Param false;"<<errno<<std::endl;
}

static void gsk25i_SetParamInfo(HINSGSKRM hInst)
{
	int errno = 1;
	double pval[8];//8个轴型参数

	pval[0] = 1;
	pval[1] = 0;
	pval[2] = 0;
	pval[3] = 1;
	pval[4] = 0;
	pval[5] = 0;
	pval[6] = 1;
	pval[7] = 0;
	errno = GSKRM_SetParamOpt(hInst, 1800, 1, pval);
	if(errno == 0)
	{
		std::cout<<"Param set yes:"<<pval[0]<<std::endl;
	}
	else
		std::cout<<"Param false;"<<errno<<std::endl;

}

static void gsk25i_SetmacroInfo(HINSGSKRM hInst)
{
	int macroNo[4];
	double macroVal[4];
	macroNo[0] = 100;
	macroNo[1] = 102;
	macroNo[2] = 111;
	macroNo[3] = 108;
	macroVal[0] = 3.3;
	macroVal[1] = 3.3;
	macroVal[2] = 3.3;
	macroVal[3] = 3.3;
	errno = GSKRM_SetMacroInfo(hInst, 4, macroNo, macroVal);
	if(errno == 0)
	{
		std::cout<<"macro get yes:"<<macroVal[0]<<std::endl;
	}
	else
		std::cout<<"macro false;"<<errno<<std::endl;
}

static void gsk25i_macroInfo(HINSGSKRM hInst)
{
	int macroNo[4];
	double macroVal[4];

	macroNo[0] = 500;
	macroNo[1] = 502;
	macroNo[2] = 511;
	macroNo[3] = 2008;
	errno = GSKRM_GetMacroInfo(hInst, 4, macroNo, macroVal);
	if(errno == 0)
	{
		std::cout<<"macro get yes:"<<macroVal[0]<<std::endl;
		std::cout<<"macro get yes:"<<macroVal[1]<<std::endl;
		std::cout<<"macro get yes:"<<macroVal[2]<<std::endl;
		std::cout<<"macro get yes:"<<macroVal[3]<<std::endl;
	
	}
	else
		std::cout<<"macro false;"<<std::endl;
}

static void gsk25i_setPitchInfo(HINSGSKRM hInst)
{
	int errno = 1;
	errno = GSKRM_SetPitchInfo(hInst, 6, 1, 1, 6);
	if(errno == 0)
		std::cout<<"pitch set yes:"<<6<<std::endl;
	else
		std::cout<<"req pitch false;"<<std::endl;
	GSKRM_SetPitchInfo(hInst, 6, 1, 0, -6);
	if(errno == 0)
		std::cout<<"pitch set yes:"<<-6<<std::endl;
	else
		std::cout<<"req pitch false;"<<std::endl;
}

static void gsk25i_PitchInfo(HINSGSKRM hInst)
{
	int pparamNum[3];
	pparamNum[0] = 1;
	pparamNum[1] = 3;
	pparamNum[2] = 6;
	struct PITCH_ARRAY *pitchinfo = GSKRM_GetPitchInfo(hInst, 1, 3, pparamNum);
	if(pitchinfo != NULL)
	{
		std::cout<<"pitchinfo:"<<pitchinfo->val[0]<<std::endl;
		std::cout<<"pitchinfo:"<<pitchinfo->val[1]<<std::endl;
		std::cout<<"pitchinfo:"<<pitchinfo->val[2]<<std::endl;
	}
	else
		std::cout<<"get pitch false;"<<std::endl;
	//读取螺补
	pitchinfo = GSKRM_GetPitchInfo(hInst, 0, 3, pparamNum);
	if(pitchinfo != NULL)
	{
		std::cout<<"pitchinfo:"<<pitchinfo->val[0]<<std::endl;
		std::cout<<"pitchinfo:"<<pitchinfo->val[1]<<std::endl;
		std::cout<<"pitchinfo:"<<pitchinfo->val[2]<<std::endl;
	}
	else
		std::cout<<"get pitch false;"<<std::endl;

}

static void gsk25i_setcoordInfo(HINSGSKRM hInst)
{
	int errno = 1;
	char coordType = 0;
	char coordNo = 1;
	char axisNo[4];
	axisNo[0] = 0;
	axisNo[1] = 1;
	axisNo[2] = 2;
	axisNo[3] = 3;
	double value[4];
	value[0] = 12;
	value[1] = 13;
	value[2] = 14;
	value[3] = 15;
	
	errno = GSKRM_SetCoordSync(hInst, coordType, coordNo, axisNo, value);
	if(errno == 0)
	{
		std::cout<<"W coord Yes"<<std::endl;
	}
	else
		std::cout<<"W coord false;"<<std::endl;

}

static void gsk25i_setofstInfo(HINSGSKRM hInst)
{
	int errno = 1;
	int toolNo = 3;
	int Ttype = 10;
	int optFlag = 0;
	double toolvalue = 23.6;

	errno = GSKRM_SetOfstSync(hInst, toolNo, Ttype, optFlag, toolvalue);
	if(errno == 0)
	{
		std::cout<<"W tool Yes"<<errno<<std::endl;
	}
	else
		std::cout<<"W tool false;"<<errno<<std::endl;	

}

static void gsk25i_setPLCInfo(HINSGSKRM hInst)
{

	clock_t start, finish;

	double elapsed_time;
	unsigned char pVal[16];
	pVal[0] = 0x00;
	pVal[1] = 0x00;
	pVal[2] = 2;
	pVal[3] = 3;
	pVal[4] = 4;
	pVal[5] = 5;
	pVal[6] = 6;
	pVal[7] = 7;
	pVal[8] = 1;
	pVal[9] = 2;
	pVal[10] = 3;
	pVal[11] = 4;
	pVal[12] = 5;
	pVal[13] = 6;
	pVal[14] = 7;
	pVal[15] = 8;
	start=clock();//测试时间
	/*
	errno = GSKRM_SetPlcData(hInst, 'R', 960, 15, pVal);//R
	if(errno == 0)
	{
		std::cout<<"R Yes"<<std::endl;
	}
	else
		std::cout<<"req false;"<<std::endl;
		*/
	finish=clock();
	elapsed_time = finish-start;
	std::cout<<"R time="<<elapsed_time<<std::endl;

	start=clock();
	/*
	errno = GSKRM_SetPlcData(hInst, 'X', 50, 8, pVal);//X
	if(errno == 0)
	{
		std::cout<<"X Yes"<<std::endl;
	}
	else
		std::cout<<"req false;"<<std::endl;
	*/
	finish=clock();
	elapsed_time = finish-start;
	std::cout<<"X time="<<elapsed_time<<std::endl;			
	
	start=clock();
	/*
	errno = GSKRM_SetPlcData(hInst, 'Y', 50, 4, pVal);//Y
	if(errno == 0)
	{
		std::cout<<"Y Yes"<<std::endl;
	}
	else
		std::cout<<"req false;"<<std::endl;
		*/	
	finish=clock();
	elapsed_time = finish-start;
	/*
	std::cout<<"Y time="<<elapsed_time<<std::endl;			
	errno = GSKRM_SetPlcData(hInst, 'F', 50, 4, pVal);//F
	if(errno == 0)
	{
		std::cout<<"F Yes"<<std::endl;
	}
	else
		std::cout<<"req false;"<<std::endl;
	*/

//int GSKRM_SetPlcDatabit(HINSGSKRM hInst, int type, int index, int bit, unsigned char Value);
	

//	pVal[0] = 0x20;
//	errno = GSKRM_SetPlcData(hInst, 'X', 6, 1, pVal);//G
	errno = GSKRM_SetPlcDatabit(hInst, 'X', 15, 7, 1);
	if(errno == 0)
	{
		std::cout<<"X Yes"<<std::endl;
	}
	else
		std::cout<<"req false;"<<std::endl;

	/*
	errno = GSKRM_SetPlcData(hInst, 'A', 20, 4, pVal);//A
	if(errno == 1)
	{
		std::cout<<"A Yes"<<std::endl;
	}
	else
		std::cout<<"req false;"<<std::endl;
		*/
}
		

static void gsk25i_loadNCfile(HINSGSKRM hInst)
{
	errno = GSKRM_SetFolderOpt(hInst, 1, 1, "O7002.NC");			
	if(errno == 0)
	{
		Sleep(100);//延时,等待加载完成
		std::cout<<"file load: yes"<<std::endl;
	}
	else
		std::cout<<"file load false;"<<std::endl;	

}


int workmain(int argc, char* argv[])
{
	int port = 5000;
	unsigned char ipaddr[4];
	ipaddr[0] = 192;
	ipaddr[1] = 168;
	ipaddr[2] = 188;
	ipaddr[3] = 123;
	char filePath[256] = "D:/R25.NC";
	HINSGSKRM hInst = GSKRM_CreateInstance(ipaddr, 1);
	if(hInst == NULL)
		std::cout<<"creator hinst err!"<<std::endl;
	else
		std::cout<<"hinst Connnect succeed!"<<std::endl;

	double axisval[8];
	//struct RUNSTAT_INFO run_info;
	int errno;
	int cnt;
	int Poscnt = 0;
	struct PosUnit PosU[1024];
	int j=0;

	FILE *fp1 = fopen("D:/gc.txt","at");
	if(fp1 == NULL)
	{
		printf("open file err \n");
	}
	fclose(fp1);
	while(true)
	{
		int  x;
		GSKRM_SetOvertime(hInst, 800);
		std::cout<<"input attach times : ";
		std::cin >> x ;
		Sleep(100);
		while(x--)
		{
			//start=clock();
			
			//读取参数
			//gsk25i_ParamInfo(hInst);

			//修改参数
			//gsk25i_SetParamInfo(hInst);
			
			//版本信息
			//gsk25i_verInfo(hInst);
			//Sleep(10);
			//运行信息等
			//gsk25i_runInfo(hInst);	
			//Sleep(10);
			//运行机床坐标
			//gsk25i_axisInfo(hInst);
			//Sleep(10);			
			//报警履历
			//gsk25i_almInfo(hInst);
			//Sleep(10);
			//加工履历
			//gsk25i_prgInfo(hInst);
			//Sleep(10);						
			//设置宏变量
			//gsk25i_SetmacroInfo(hInst);

			//读取宏变量
			//gsk25i_macroInfo(hInst);
			
				
			//设置螺补
			//gsk25i_setPitchInfo(hInst);
			
			//读取螺补
			//gsk25i_PitchInfo(hInst);

			//写入工件系
			//gsk25i_setcoordInfo(hInst);
			
			//写入刀偏
			//gsk25i_setofstInfo(hInst);
			
			

			//Sleep(100);
			//写入PLC资源
			//gsk25i_setPLCInfo(hInst);


			//上传G代码文件
/*
			errno = GSKRM_SetPutFile(hInst, filePath, 0);
			if(errno == 0)
			{
				std::cout<<"file set yes:"<<std::endl;
				Sleep(100);
				std::cout<<"connect state: "<<errno<<std::endl;
			}
			else
				std::cout<<"file false;"<<errno<<std::endl;
*/
			Sleep(200);
			//文件加载
		//	gsk25i_loadNCfile(hInst);

		    Sleep(100);
			//获得25i的系统名
			gsk25i_nameInfo(hInst);
		    Sleep(100);
			//Sleep(100);
			//写入PLC资源
			gsk25i_setPLCInfo(hInst);

			//数据采集
			/*
			memset(PosU, 0, sizeof(struct PosUnit)*1024);
			errno = reqRunMonitor(hInst, 2, 0, 0, 0, 0, &Poscnt, PosU);
			if(errno == 0)
			{
				std::cout<<"reqRunMonitor set yes:"<<Poscnt<<std::endl;
				//插补位置
				
				for(j=0; j<Poscnt; j++)
				{
				std::cout<<"X="<<PosU[j].DspData.CmdPos[0]<<"Y="<<PosU[j].DspData.CmdPos[1]<<std::endl;
				}
				//伺服位置
				for(j=0; j<Poscnt; j++)
				{
					 
					//fprintf(fp1," X=%f \n", PosU[j].ServoData.CmdPos[0]);

				std::cout<<"X="<<PosU[j].ServoData.CmdPos[0]<<"Y="<<PosU[j].ServoData.CmdPos[1]<<" "<<PosU[j].ServoData.PidCmdPos[0]<<" "<<PosU[j].ServoData.PidCmdPos[1]<<std::endl;
				
				}
	
				std::cout<<"reqRunMonitor state: "<<errno<<std::endl;
			}
			else
			{
				std::cout<<"reqRunMonitor false;"<<errno<<std::endl;
				break;
			}
			*/
			errno = GSKRM_GetConnectState(hInst);
			std::cout<<"connect state: "<<errno<<std::endl;
			Sleep(1000);				
		}
		break;
	}
	//fclose(fp1);
	//reqStopMonitor(hInst);
	Sleep(100);

	std::cout<<"ddd state: "<<errno<<std::endl;
	GSKRM_CloseInstance(hInst);
	errno = GSKRM_GetConnectState(hInst);
	std::cout<<"end state: "<<errno<<std::endl;
	return 1;
}

DWORD WINAPI ThreadFun(LPVOID pM)
{
	int port = 5000;
	unsigned char ipaddr[4];
	ipaddr[0] = 192;
	ipaddr[1] = 168;
	ipaddr[2] = 188;
	ipaddr[3] = 123;

	HINSGSKRM hInst = GSKRM_CreateInstance(ipaddr, 1);
	if(hInst == NULL)
		std::cout<<"creator hinst err!"<<std::endl;
	else
		std::cout<<"hinst Connnect succeed!"<<std::endl;

	int errno;
	int Poscnt = 0;
	int j=0;

	while(true)
	{
		int  x;
		GSKRM_SetOvertime(hInst, 800);
		std::cout<<"input attach times : ";
		std::cin >> x ;
		Sleep(100);
		while(x--)
		{
			//start=clock();
			
	
			//版本信息
			gsk25i_verInfo(hInst);
			Sleep(100);
			//运行信息等
			gsk25i_runInfo(hInst);	
			Sleep(10);
			//运行机床坐标
			gsk25i_axisInfo(hInst);
			Sleep(10);			
		//	报警履历
			gsk25i_almInfo(hInst);
			Sleep(10);
			//获得25i的系统名
			gsk25i_nameInfo(hInst);
		    Sleep(100);
			//Sleep(100);
			//写入PLC资源
			gsk25i_setPLCInfo(hInst);

			errno = GSKRM_GetConnectState(hInst);
			std::cout<<"connect state: "<<errno<<std::endl;
			Sleep(1000);				
		}
		break;
	}
	//reqStopMonitor(hInst);
	Sleep(100);

	std::cout<<"ddd state: "<<errno<<std::endl;
	GSKRM_CloseInstance(hInst);
	errno = GSKRM_GetConnectState(hInst);
	std::cout<<"end state: "<<errno<<std::endl;
	return 1;
}



DWORD WINAPI ThreadFun11(LPVOID pM)
{
	int port = 5000;
	unsigned char ipaddr[4];
	ipaddr[0] = 192;
	ipaddr[1] = 168;
	ipaddr[2] = 188;
	ipaddr[3] = 123;

	HINSGSKRM hInst = GSKRM_CreateInstance(ipaddr, 1);
	if(hInst == NULL)
		std::cout<<"creator hinst err!"<<std::endl;
	else
		std::cout<<"hinst Connnect succeed!"<<std::endl;

	int errno;
	int Poscnt = 0;
	int j=0;

	while(true)
	{
		int  x;
		GSKRM_SetOvertime(hInst, 800);
		std::cout<<"input attach times : ";
		std::cin >> x ;
		Sleep(100);
		while(x--)
		{
			//start=clock();
			
	
			//版本信息
			gsk25i_verInfo(hInst);
			Sleep(100);
			//运行信息等
			gsk25i_runInfo(hInst);	
			Sleep(10);
			//运行机床坐标
			gsk25i_axisInfo(hInst);
			Sleep(10);			
		//	报警履历
			gsk25i_almInfo(hInst);
			Sleep(10);
			//获得25i的系统名
			gsk25i_nameInfo(hInst);
		    Sleep(100);
			//Sleep(100);
			//写入PLC资源
			gsk25i_setPLCInfo(hInst);

			errno = GSKRM_GetConnectState(hInst);
			std::cout<<"connect state: "<<errno<<std::endl;
			Sleep(1000);				
		}
		break;
	}
	//reqStopMonitor(hInst);
	Sleep(100);

	std::cout<<"ddd state: "<<errno<<std::endl;
	GSKRM_CloseInstance(hInst);
	errno = GSKRM_GetConnectState(hInst);
	std::cout<<"end state: "<<errno<<std::endl;
	return 1;
}


int main(int argc, char* argv[])
{
	printf("main thread\n");
    HANDLE handle1 = CreateThread(NULL, 0, ThreadFun, "Thread one", 0, NULL);
    HANDLE handle2 = CreateThread(NULL, 0, ThreadFun11, "Thread two", 0, NULL);

    DWORD exitCode1 = 0, exitCode2 = 0;
    
	while(1)
	{
          GetExitCodeThread(handle1, &exitCode1);
          GetExitCodeThread(handle2, &exitCode2);
          if(exitCode1 != STILL_ACTIVE && exitCode2 != STILL_ACTIVE)
               break;
     }

     CloseHandle(handle1);
     CloseHandle(handle2);
     system("PAUSE");
     return 0;
	
}
