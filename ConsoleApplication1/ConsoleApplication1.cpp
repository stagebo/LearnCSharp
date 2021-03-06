// ConsoleApplication1.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>

#include<stdio.h>
#include<math.h>
#include<time.h>
#include<stdlib.h>

#define P_num 20 //粒子数目 
#define dim 2
#define low -100 //搜索域范围
#define high 100
#define iter_num 600
#define V_max 20 //速度范围
#define c1 2
#define c2 2
#define w 0.5
#define alp 1
double particle[P_num][dim]; //个体集合
double particle_loc_best[P_num][dim]; //每个个体局部最优向量
double particle_loc_fit[P_num]; //个体的局部最优适应度,有局部最优向量计算而来
double particle_glo_best[dim]; //全局最优向量
double gfit;                           //全局最优适应度,有全局最优向量计算而来
double particle_v[P_num][dim]; //记录每个个体的当前代速度向量
double particle_fit[P_num]; //记录每个粒子的当前代适应度

double fitness(double a[])
{
	//	int i;
	double res = 0.0;
	//	for(i=0; i<dim; i++)
	//		sum += a[i]*a[i];
		//sum += 100*(a[i+1]-a[i]*a[i])*(a[i+1]-a[i]*a[i])
		//+(a[i]-1)*(a[i]-1);

	res = (sin(a[0] * a[0] + a[1] * a[1])*sin(a[0] * a[0] + a[1] * a[1]) - 0.5) / ((1 + 0.001*(a[0] * a[0] + a[1] * a[1]))*(1 + 0.001*(a[0] * a[0] + a[1] * a[1])));

	return res;
}
void initial()
{
	int i, j;
	for (i = 0; i < P_num; i++) //随机生成粒子
		for (j = 0; j < dim; j++)
		{
			particle[i][j] = low + (high - low)*1.0*rand() / RAND_MAX; //初始化群体
			particle_loc_best[i][j] = particle[i][j]; //将当前最优结果写入局部最优集合
			particle_v[i][j] = -V_max + 2 * V_max*1.0*rand() / RAND_MAX;  //速度
		}
	for (i = 0; i < P_num; i++)  //计算每个粒子的适应度
	{
		particle_fit[i] = fitness(particle[i]);
		particle_loc_fit[i] = particle_fit[i];
	}
	gfit = particle_loc_fit[0];  //找出全局最优
	j = 0;
	for (i = 1; i < P_num; i++)
	{
		if (particle_loc_fit[i] < gfit)
		{
			gfit = particle_loc_fit[i];
			j = i;
		}
	}
	for (i = 0; i < dim; i++) //更新全局最优向量 
		particle_glo_best[i] = particle_loc_best[j][i];
}
void renew_particle()
{
	int i, j;
	for (i = 0; i < P_num; i++) //更新个体位置生成位置
		for (j = 0; j < dim; j++)
		{
			particle[i][j] += alp * particle_v[i][j];
			if (particle[i][j] > high)
				particle[i][j] = high;
			if (particle[i][j] < low)
				particle[i][j] = low;
		}
}

void renew_var()
{
	int i, j;
	for (i = 0; i < P_num; i++)  //计算每个粒子的适应度
	{
		particle_fit[i] = fitness(particle[i]);
		if (particle_fit[i] < particle_loc_fit[i])  //更新个体局部最优值
		{
			particle_loc_fit[i] = particle_fit[i];
			for (j = 0; j < dim; j++) // 更新局部最优向量
				particle_loc_best[i][j] = particle[i][j];
		}
	}
	for (i = 0, j = -1; i < P_num; i++)  //更新全局变量
	{
		if (particle_loc_fit[i] < gfit)
		{
			gfit = particle_loc_fit[i];
			j = i;
		}
	}
	if (j != -1)
		for (i = 0; i < dim; i++)  //更新全局最优向量  
			particle_glo_best[i] = particle_loc_best[j][i];
	for (i = 0; i < P_num; i++) //更新个体速度
		for (j = 0; j < dim; j++)
		{
			particle_v[i][j] = w * particle_v[i][j] + c1 * 1.0*rand() / RAND_MAX * (particle_loc_best[i][j] - particle[i][j])
				+ c2 * 1.0*rand() / RAND_MAX * (particle_glo_best[j] - particle[i][j]);
			if (particle_v[i][j] > V_max)
				particle_v[i][j] = V_max;
			if (particle_v[i][j] < -V_max)
				particle_v[i][j] = -V_max;
		}
}

int main()
{
	int i = 0;
	srand((unsigned)time(NULL));
	initial();
	while (i < iter_num)
	{
		renew_particle();
		renew_var();
		printf("x1=%lf,x2=%lf,minf(x)=%lf\n", particle_glo_best[0], particle_glo_best[1], gfit);
		i++;
	}
	//	fflush(stdin);
    std::cout << "Hello World!\n"; 
	return 0;
}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门提示: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件
