using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet与csharp
{
    class 重载操作符
    {
        public static void Run()
        {
         
            Node root = getList(9, 5, 6, 7, 2, 8, 1, 4, 3);
            while (true)
            {
                if (root == null) break;
                Console.WriteLine(root.data);
                ++root;
            }
            Console.ReadKey();
        }
        static Node getList(params int[] a)
        {
            Node root = new Node(a[0]);
            Node temp = root;
            for (int i = 1; i < a.Length; i++)
            {
                temp.next = new Node(a[i]);
                Node t = temp;
                ++temp;
                temp.pre = t;
            }
            return root;
        }
        class Node
        {
            public int data;
            public Node next;
            public Node pre;
            public Node(int data)
            {
                this.data = data;
                this.next = null;
            }
            public static Node operator ++(Node root)
            {
                return root.next;
            }
            public static Node operator --(Node root)
            {
                return root.pre;
            }
        }
    }
   
}
