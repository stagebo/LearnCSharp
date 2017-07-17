using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp
{
    partial class Program
    {
       

    }
    partial class 结构
    {
        /*
         * 什么是结构：结构是程序员定义的数据类型，类似于类，他又自己的数据成员和函数成员。但是和类有本质区别
         * 结构和类的区别：
         *          类是引用类型而结构是值类型
         *          结构是隐士密封的，以为着他不能被派生
         *          但是结构可以继承类，可以实现接口
         * 结构是值类型：
         *          结构类型的变量不能为null
         *          两个结构变量不能引用同一对象
         * 结构拥有一个隐式的无参数构造函数
         * 也可以不用new关键字创建结构实例，但是必须显式地为所有数据成员赋值之后才能调用他的数据成员和函数成员
         * 结构的字段是不允许在内部初始化的
         * 结构的优点：比类占用更少的资源，可以代替类来提高性能。但要注意装箱和拆箱的高代价。
         * 
         * 最后：预定义简单类型实际上在c#中被定义为结构
         * 和类一样，可以用partial声明结构。
         *  
         */ 
       
    }
    struct c
    {
        //int x;
    }
}
