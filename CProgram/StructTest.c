/*
���򣺵�����Ĳ���
����Ա�������� 1330090110 13�����4��
ʱ�䣺20141016   */
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
	//����һ��ֻ��ͷ������������
	*L = (LinkList)malloc(sizeof(LNode)); //Ϊͷ������洢�ռ�
	if (*L == NULL) return ERROR;
	(*L)->next = NULL;
	return OK;
}//InitList_L

void CreateList_L(LinkList L, int n) {
	LinkList p, q;
	int i;
	p = L;
	printf("��������Ҫ��������ݣ�\n");
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
	printf("�����������Ԫ��:\n");
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
	if (!L) return ERROR;	// L������
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
	LinkList p;//����ָ��p
	p = L->next;//��pָ������L�ĵ�һ�����
	j = 1;//jΪ������
	while (p&&j<i) {//*��Ϊ���Ҽ�����j��û�е���iʱ��ѭ������
		p = p->next;//��pָ����һ�����
		++j;
	}
	if (!p || j>i)
		return ERROR;//��i����㲻����
	*e = p->data;//ȡ��i����������
	printf("���%d����Ԫ��:\n", i);
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
	printf("%d\n", InitList_L(&L));//����������
	CreateList_L(L, 5);
	printf("%d\n", PrintLink_L(L));
	printf("����������ڵ�������Ԫ��λ�ü�Ԫ��:\n");
	scanf_s("%d%d", &i, &n);
	printf("%d\n", ListInsert_L(L, i, n));
	printf("%d\n", PrintLink_L(L));
	printf("������ɾ���ڵ�������Ԫ��λ��:\n");
	scanf_s("%d", &x);
	printf("%d\n", ListDelete_L(L, x, &e));
	printf("%d\n", PrintLink_L(L));
	printf("��������Ҫ��ȡ�ڵ�������Ԫ��λ��:\n");
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

