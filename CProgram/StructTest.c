/*
程序：单链表的插入
程序员：刘晓钰 1330090110 13计算机4班
时间：20141016   */
#include <stdio.h>
#include <stdlib.h>

#define ERROR 0
#define OK 1
#define OVERFLOW -1


typedef int Status;

typedef int ElemType;


typedef struct LNode {
	ElemType data;
	struct LNode *next;
}LNode, *LinkList;

Status InitList_L(LinkList *L) {
	//构造一个只有头结点的线性链表
	*L = (LinkList)malloc(sizeof(LNode)); //为头结点分配存储空间
	if (*L == NULL) return ERROR;
	(*L)->next = NULL;
	return OK;
}//InitList_L

void CreateList_L(LinkList L, int n) {
	LinkList p, q;
	int i;
	p = L;
	printf("请输入你要输入的数据：\n");
	for (i = n; i>0; --i) {
		q = (LinkList)malloc(sizeof(LNode));
		scanf_s("%d", &q->data);
		//q->next=p->next; 
		p->next = q;
		p = q;
		//q=q->next;
	}
	q->next = NULL;
}//CreateList_q

Status PrintLink_L(LinkList L) {
	LinkList p;
	p = L->next;
	printf("输出单链表中元素:\n");
	while (p != NULL) {
		printf("%d ", p->data);
		p = p->next;
	}
	printf("\n");
	return OK;
}//PrintLink_L



Status ListInsert_L(LinkList L, int i, ElemType e) {
	int j = 0;
	LinkList p, s;
	if (!L) return ERROR;	// L不存在
	p = L;
	while (p&&j<i - 1) {
		p = p->next;
		++j;
	}
	if (!p || j>i - 1)
		return ERROR;
	s = (LinkList)malloc(sizeof(LNode));
	if (!s) return OVERFLOW;
	s->data = e;
	s->next = p->next;
	p->next = s;
	return OK;
}//ListInsert_L

Status GetElem_L(LinkList L, int i, ElemType *e) {
	int j;
	LinkList p;//声明指针p
	p = L->next;//让p指向链表L的第一个结点
	j = 1;//j为计数器
	while (p&&j<i) {//*不为空且计数器j还没有等于i时，循环继续
		p = p->next;//让p指向下一个结点
		++j;
	}
	if (!p || j>i)
		return ERROR;//第i个结点不存在
	*e = p->data;//取第i个结点的数据
	printf("输出%d处的元素:\n", i);
	printf("%d\n", *e);
	return OK;
}//GetElem_L


Status ListDelete_L(LinkList L, int i, ElemType *e) {
	int j = 0;
	LinkList p, q;
	p = L;
	while (p->next&&j<i - 1) {
		p = p->next;
		++j;
	}
	if (!(p->next) || j>i - 1)
		return ERROR;
	q = p->next;
	p->next = q->next;
	e = q->data;
	free(q);
	return OK;
}//ListDelete_L





int mains() {
	LinkList L;
	ElemType e;
	int i, n, x, y;
	printf("%d\n", InitList_L(&L));//构建单链表
	CreateList_L(L, 5);
	printf("%d\n", PrintLink_L(L));
	printf("请输入插入在单链表处的元素位置及元素:\n");
	scanf_s("%d%d", &i, &n);
	printf("%d\n", ListInsert_L(L, i, n));
	printf("%d\n", PrintLink_L(L));
	printf("请输入删除在单链表处的元素位置:\n");
	scanf_s("%d", &x);
	printf("%d\n", ListDelete_L(L, x, &e));
	printf("%d\n", PrintLink_L(L));
	printf("请输入想要获取在单链表处的元素位置:\n");
	scanf_s("%d", &y);
	printf("%d\n", GetElem_L(L, y, &e));

	return 0;

}
void main() {
	LinkList l;
	l = (LinkList)malloc(sizeof(LNode));
	int i = 0;
	LinkList p, q;
	p = l;
	while (i< 6) {
		q = (LinkList)malloc(sizeof(LNode));
		p->next = q;
		p->data = i;
		p = p->next;
		i++;
	}
	p = l;
	while (p->next  != NULL)
	{
		printf("%d\n",p->data);
		p = p->next;
	}

	while (1);
}

