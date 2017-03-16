using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法
{
    class 数据结构_图
    {
        static const int o = int.MaxValue;
        int[][] racs;
        String[] v;
        //racs[o][]=new int[]{};
        public void init()
        {
            racs = new int[8][];
            racs[0] = new int[] { o, 1, 1, o, o, o, o, o };
            racs[1] = new int[] { 1, o, o, 1, o, o, o, o };
            racs[2] = new int[] { 1, o, o, 1, 1, o, o, o };
            racs[3] = new int[] { o, 1, 1, o, o, 1, 1, o };
            racs[4] = new int[] { o, o, 1, o, o, o, o, 1 };
            racs[5] = new int[] { o, o, o, 1, o, o, 1, o };
            racs[6] = new int[] { o, o, o, 1, o, 1, o, o };
            racs[7] = new int[] { o, o, o, o, 1, o, o, o };
            v = new String[] { "V0", "V1", "V2", "V3", "V4", "V5", "V6", "V7" };
        }
        public void Run()
        {

        }


    }
    class Graph<T>
    {
        private int[][] racs;//邻接矩阵
        private T[] v;      //各点所带信息

        private int vNum;   //节点数目
        private int[] visitedCount; //记录访问
        private int[] currDist;     //最短路径算法中用来记录每个节点的当前路径长度.

        public Graph(int[][] racs, T[] v)
        {
            if (racs.Length != racs[0].Length)
            {
                throw new Exception("racs is not a adjacency matrix!");
            }
            if (racs.Length != v.Length)
            {
                throw new Exception("Argument of 2 verticeInfo's length is error!");
            }
            this.racs = racs;
            this.v = v;
            vNum = racs.Length;
            visitedCount = new int[vNum];
        }
    }
}
