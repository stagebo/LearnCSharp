using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数学问题
{
    public class 二叉树
    {
        public static void Run()
        {
            HashSet<int> hs = new HashSet<int>();
            NodeB root = new NodeB();
            Random r = new Random();
            //insert(root, 1); insert(root, 2); insert(root, 3);
            for (int i = 0; i < 3; i++)
            {
                int x = r.Next(100);
                hs.Add(x);
                if (insert(root, x))
                {
                }
            }
            Console.WriteLine("root深度----" + deepth(root));
            Console.WriteLine("root长度----" + hs.Count);

            Console.WriteLine("树的内容");
            List<int> l = hs.ToList<int>();
            l.Sort();
            foreach (int ii in l)
            {
                Console.Write(ii + "---");
            }

            Console.WriteLine("\r\n前序遍历");
            preOrder(root);
            Console.WriteLine();
            Console.WriteLine("中序遍历");
            midOrder(root);
            Console.WriteLine();
            Console.WriteLine("后序遍历");
            postOrder(root);
            Console.WriteLine();
            Console.ReadKey();
        }

        #region   /*二叉树的各种操作方法*/
        /*删除节点*/
        public static bool deleteNode(NodeB root, int? data)
        {
            if (root == null)
            {
                return false;
            }
            /*先找到要删除的节点和他的父节点*/
            NodeB parent = null;
            NodeB child = root;
            while (true)
            {
                if (child == null)
                {
                    return false;
                }
                if (data == child.Data)
                {
                    break;
                }
                else if (data > child.Data)
                {
                    parent = child;
                    child = child.RightChild;
                }
                else if (data < child.Data)
                {
                    parent = child;
                    child = child.LeftChild;
                }
            }
            /*判断没找到的结果，return*/
            if (child == null)
            {
                return false;
            }
            /*判断是否为根节点*/
            bool isRoot = root.Data == child.Data;
            if (isRoot)
            {
                if (child.LeftChild == null & child.RightChild == null)
                {
                    root.Data = null ;
                }
                else if (child.LeftChild == null)
                {
                    root = root.RightChild;
                }
                else if (child.RightChild == null)
                {
                    root = root.LeftChild;
                }
                /*孩子左右子树都不为空，选择右子树种最大节点来代替该节点*/
                else
                { 
                
                }
            }
            /*不是根节点*/
            else
            {
                /*判断是否为左子树*/
                bool isLeft = parent.LeftChild != null && parent.LeftChild.Data == data;
                /*目标左右都为空*/
                if (child.LeftChild == null && child.RightChild == null)
                {
                    if (isLeft)
                    {
                        parent.LeftChild = null;
                    }
                    else 
                    {
                        parent.RightChild = null;
                    }
                }
                else if (child.LeftChild == null)
                {
                    if (isLeft)
                    {
                        parent.LeftChild = child.RightChild;
                    }
                    else
                    {
                        parent.RightChild = child.RightChild;
                    }
                }
                else if (child.RightChild == null)
                {
                    if (isLeft)
                    {
                        parent.LeftChild = child.LeftChild;
                    }
                    else
                    {
                        parent.RightChild = child.LeftChild;
                    }
                }
                /*孩子的左右字数都不为空，找出孩子的右树种最小树来代替*/
                else
                { 
                
                }
            }
            return true;
        }
        /*求树的深度*/
        public static int deepth(NodeB root)
        {
            if (root == null) return 0;
            else return deepth(root.LeftChild) > deepth(root.RightChild) ? deepth(root.LeftChild) + 1 : deepth(root.RightChild) + 1;
        }
        /*求树的节点数量*/
        public static int size(NodeB root)
        {
            if (root == null) return 0;
            return 1 + size(root.LeftChild) + size(root.RightChild);
        }

        /*查找二叉树节点*/
        public static NodeB findNode(NodeB root, int? data)
        {
            if (root == null)
            {
                return null;
            }
            if (root.Data == data)
            {
                return root;
            }
            else if (data > root.Data)
            {
                if (root.RightChild == null)
                {
                    return null;
                }
                return findNode(root.RightChild, data);
            }
            else
            {
                if (root.LeftChild == null)
                {
                    return null;
                }
                return findNode(root.LeftChild, data);
            } 
        }

        /*前序遍历二叉树*/
        public static void preOrder(NodeB root)
        {
            if (root == null) return;
            Console.Write(root.Data + "---");
            preOrder(root.LeftChild);
            preOrder(root.RightChild);

        }

        /*中序遍历二叉树*/
        public static void midOrder(NodeB root)
        {
            if (root == null) return;
            midOrder(root.LeftChild);
            Console.Write(root.Data + "---");
            midOrder(root.RightChild);

        }

        /*后序遍历二叉树*/
        public static void postOrder(NodeB root)
        {
            if (root == null) return;
            postOrder(root.LeftChild);
            postOrder(root.RightChild);
            Console.Write(root.Data + "---");

        }

        /* 二叉树的插入操作*/
        public static bool insert(NodeB root, int? data)
        {
            if (root == null)
            {
                return false;
            }
            if (root.Data == null)
            {
                root.Data = data;
                Console.WriteLine("插入成功：" + data);
                return true;
            }
            else if (data == root.Data) return false;
            else if (data > root.Data)
            {
                if (root.RightChild == null)
                {
                    root.RightChild = new NodeB(data);
                    Console.WriteLine("插入成功：" + data);
                }
                else
                {
                    return insert(root.RightChild, data);
                }
            }
            else
            {
                if (root.LeftChild == null)
                {
                    root.LeftChild = new NodeB(data);
                    Console.WriteLine("插入成功：" + data);
                }
                else
                {
                    return insert(root.LeftChild, data);
                }
            }
            return true;
        }
        /*创建一颗二叉树，输入参数是一个可变数组*/
        public static NodeB createNode(params int[] array)
        {
            NodeB root = new NodeB();
            foreach (int a in array)
            {
                insert(root, a);
            }
            return root;
        }
        #endregion

        #region /* 二叉树节点*/
        public class NodeB
        {
            public NodeB LeftChild;
            public NodeB RightChild;
            public int? Data;
            public NodeB()
            {
                LeftChild = null;
                RightChild = null;
                Data = null;
            }
            public NodeB(int? data)
            {
                LeftChild = null;
                RightChild = null;
                this.Data = data;
            }
            
        }
        /*泛型节点*/
        public class TNode<T>
        {
            public TNode<T> leftChild;
            public TNode<T> rightChild;
            public T data;
            public TNode (T t)
            {
                this.data = t;
                leftChild = null;
                rightChild = null;
            }
            public TNode()
            {
                data=default(T);
                leftChild = rightChild  = null;
            }

        }
        #endregion
    }


   
}
