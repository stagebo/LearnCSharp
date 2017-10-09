// CConsoleApp.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include<stdio.h>  
#include<stdlib.h>  
#include<string.h>  

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

