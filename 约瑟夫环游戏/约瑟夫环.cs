using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数学问题
{
    public class 约瑟夫环
    {
        /*
        * 约瑟夫环（约瑟夫问题）是一个数学的应用问题：已知n个人（以编号1，2，3...n分别表示）围坐在一张圆桌周围。
        * 从编号为k的人开始报数，数到m的那个人出列；他的下一个人又从1开始报数，数到m的那个人又出列；依此规律重
        * 复下去，直到圆桌周围的人全部出列。通常解决这类问题时我们把编号从0~n-1，最后结果+1即为原问题的解。
        * 约瑟夫环运作如下：
             1、一群人围在一起坐成环状（如：N）
             2、从某个编号开始报数（如：K）
             3、数到某个数（如：M）的时候，此人出列，下一个人重新报数
             4、一直循环，直到所有人出列，约瑟夫环结束
        * 
        * 实现思路：
        *      1，创建节点类，创建环（JosephCir）类，在JosephCir环内实现环的连接，提供游戏开始方法
        *      2，游戏开始，丢……
        *      3，当环当前长度LengthNow为1时，游戏结束，返回最后胜出同学的节点。
        */
        static void Run()
        {
            JosephCir j = new JosephCir(3, 1, 4);

            Console.WriteLine("游戏开始！");
            NodeJ n = j.GameStart();

            if (n != null)
                Console.WriteLine("游戏胜出同学的编号为：" + n.Data);
            else
                Console.WriteLine("游戏失败");

            Console.ReadKey();
        }
    }
    /*约瑟夫环*/
    class JosephCir
    {
        public int LengthNow { get; set; }/*环当前长度，如果环长为1则表示游戏结束*/
        public NodeJ head;/*1号同学节点*/
        public NodeJ now;/*即手绢所在同学节点*/
        public int Total { get; set; }/*总共玩游戏的人数*/
        public int Start { get; set; }/*从该编号开始游戏*/
        public int PlayNum { get; set; }/*每次游戏丢PlayNum次手绢*/

        /*创建约瑟夫环*/
        public JosephCir(int total, int start, int playNum)
        {
            this.Total = total;
            this.Start = start;
            this.PlayNum = playNum;
            this.LengthNow = total;
            head = new NodeJ(1);
            now = head;
            for (int i = 1; i < total; i++)
            {
                NodeJ temp = new NodeJ(i + 1);
                now.next = temp;
                now = temp;
            }
            now.next = head;
            /*now 和head指向1号编号同学*/
            now = head;
        }

        /*游戏开始，结束时返回游戏胜出的NodeJ*/
        public NodeJ GameStart()
        {
            if (head == null) return null;

            /*将手绢发给开始的那个人*/
            for (int i = 0; i < Start - 1; i++)
            {
                now = now.next;
            }

            /*开始游戏*/
            while (LengthNow > 1)
            {
                for (int i = 0; i < PlayNum - 1; i++)
                    now = now.next;

                now.next = now.next.next;
                now = now.next;
                LengthNow -= 1;
            }
            if (LengthNow == 1 && now != null)
                return now;
            return null;
        }
    }
    /*节点类*/
    class NodeJ
    {
        public NodeJ next=null;
        public int Data { get; set; }
        public NodeJ(int data)
        {
            this.Data = data;
        }
    }
}
