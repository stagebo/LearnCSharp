// CConsoleApp.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include<stdio.h>  
#include<stdlib.h>  
#include<string.h> 
#ifdef  BYTE
#else
#define byte BYTE
#endif
int main()
{
	char *a = "abc";
	char *b = "def";
	printf_s("%s\r\n", a);
	printf_s("%s\r\n", b);
	char *c = (char *)malloc(strlen(a) + strlen(b) + 1); //局部变量，用malloc申请内存  
	if (c == NULL) exit(1);
	char *tempc = c; //把首地址存下来  
	while (*a != '\0') {
		*c++ = *a++;
	}
	while ((*c++ = *b++) != '\0') {
		;
	}
	printf_s("%s\r\n", tempc);
	while (true);
	return 0;
}
class CRandomWalk {
public :
	void ComputeCoff();
	void AddConstrain();
	MyImage m_image;
	A m_A;

};
class MyImage {
public:
	int Heigth;
	int Width;

};
class A {
public :
	int* arr;
	void resize(int w, int h) {
		
	}

};
//根据邻接关系，构造拉普拉斯矩阵
void CRandomWalk::ComputeCoff()
{
	int height = m_image->Height;
	int width = m_image->Width;
	m_A.resize(height*width, width*height);
	int vn = height*width;
	typedef Eigen::Triplet<double> Tri;
	std::vector<Tri> tripletList;
	for (int i = 0;i<height;i++)
	{
		for (int j = 0;j<width;j++)
		{
			int idex = i*width + j;
			Eigen::Vector2i nei[4] = { 
				Eigen::Vector2i(i - 1,j),Eigen::Vector2i(i,j - 1),Eigen::Vector2i(i + 1,j),Eigen::Vector2i(i,j + 1) 
			};
			BYTE *data = GetpData(Eigen::Vector2i(i, j), m_image);
			float sumw = 0;
			for (int k = 0;k<4;k++)
			{
				if (nei[k][0] >= 0 && nei[k][0]<height&&nei[k][1] >= 0 && nei[k][1]<width)
				{
					int idexnei = nei[k][0] * width + nei[k][1];
					BYTE *neidata = GetpData(nei[k], m_image);
					float w = -GetGrad(data, neidata);
					w = exp(w / (2 * 50 * 50));
					sumw += w;
					tripletList.push_back(Tri(idex, idexnei, w));
				}

			}
			//计算A
			tripletList.push_back(Tri(idex, idex, -sumw));

		}
	}
	m_A.setFromTriplets(tripletList.begin(), tripletList.end());
	m_B.resize(height*width);
	m_B.setZero();


}
void CRandomWalk::AddConstrain()
{
	for (int i = 0;i<m_front.size();i++)
	{
		int indexf = m_image->Width*m_front[i].y + m_front[i].x;
		float a = m_A.coeff(indexf, indexf) + 1;
		m_A.coeffRef(indexf, indexf) = a;
	}

	for (int j = 0;j<m_back.size();j++)
	{
		int indexb = m_image->Width*m_back[j].y + m_back[j].x;
		float b = m_A.coeff(indexb, indexb) + 1;
		m_A.coeffRef(indexb, indexb) = b;
	}

	m_MatricesCholesky = new Eigen::SparseLU<Eigen::SparseMatrix<double>>(m_A);

}
void CRandomWalk::Solver()
{
	ComputeCoff();
	AddConstrain();


	Eigen::VectorXd b = m_B;
	for (int i = 0;i<m_front.size();i++)
	{
		int indexf = m_image->Width*m_front[i].y + m_front[i].x;
		b(indexf) += 1;
	}
	Eigen::VectorXd x = m_MatricesCholesky->solve(b);


	Eigen::VectorXd bb = m_B;
	for (int j = 0;j<m_back.size();j++)
	{
		int indexfb = m_image->Width*m_back[j].y + m_back[j].x;
		bb(indexfb) += 1;
	}
	Eigen::VectorXd y = m_MatricesCholesky->solve(bb);

	//比较概率大小
	for (int i = 0;i<m_image->Height*m_image->Width;i++)
	{
		if (x(i)>y(i))
		{
			BYTE *data = (BYTE*)m_image->Scan0 + i * 4;
			for (int k = 0;k<3;k++)
			{
				data[k] = 255;
			}
		}
	}
	//int indexb=m_image->Width*m_front.y+m_front.x;

}
BYTE* CRandomWalk::GetpData(Eigen::Vector2i pt, BitmapData*image)
{
	return (BYTE*)image->Scan0 + image->Width * 4 * pt[0] + 4 * pt[1];

}
float CRandomWalk::GetGrad(BYTE*data1, BYTE*data2)
{
	float sum0 = 0;
	for (int i = 0;i<3;i++)
	{
		sum0 += (data1[i] - data2[i])*(data1[i] - data2[i]);

	}
	return sum0;

}

