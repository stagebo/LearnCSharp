using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet与csharp
{
    /*
 * 1···c#代码的工作流程：c#代码--->CIL(Common Intermediate Language)-->本机代码
 *                         IDE    CLR                                   JIT
 * 2···托管代码：为.NET框架编写的代码成为托管代码，需要CLR
 * 3···非托管代码：不在CLR控制下运行的代码，比如Win32  c/c++  DLL，成为非托管代码
 * 4···using(int x=0)用于回收非托管资源。因为非托管代码不在clr控制之下，所以资源不
 * 会被gc回收，所以需要用using来回收
 * 5···重写是覆盖父类方法，必须用new或者override关键字修饰，重载是本类里面。
 * 6···Http协议：HyperText Transfer Protocol（超文本传输协议）
 *              是一个工作在应用层的规定了客户端和服务器端请求以及应答的标准。
 *              http协议是无连接协议。
 *              
 * 7···计算机的现代架构：包含三个部分，储存单元（硬盘），控制单元+计算单元（cpu），IO设备
 * 8···域名：所谓域，就是区域，可以是空间区域，也可以是按一定规则划分的区域，顾名思义，
 *      域名，
 *          就是区域名。域的级次用.来划分，最后一个字段为顶级域名，一次向前为二级、
 *          三级域名。
 * 9···DNS：Domain Name System，域名系统。Internet上作为域名和ip地址相互映射的分
 *      布式数据库，
 *          用于代替ip地址方便记忆。
 * 10···is和as的区别：is用于判断某实例是否属于某类，as用于将a类实例转化为b类实例，
 *      如果成功就返回该实例的b类型，如果失败就返回null
 * ···
 * 11···今天学习的内容---2016.8.30
 * 12···html的标签基本学完，大都试用过一遍，记住了大半。html5还没有看
 * 13···了解了css的工作原理，选择器的种类（元素选择器，类选择器，id选择器#，属性选
 *      择器[],选择器的派生，选择器的分组等）
 * 14···理解了css的定位机制,绝对定位（absolute）和相对定位（ relative）.
 * 15···理解了css的模型，边框等所代表的位置，
 * 16···了解了JQuery的工作原理，熟悉了一些基本库函数，会查文档写一些自己需要的内容。
 * 17···json，JavaScript对象表示法，用于传输数据。类似于xml但是比xml更灵活
 * 18···c#的内存分配
 *       在c#中，内存分为5个区，堆、栈、自由储存区、全局/静态储存区和常量储存区
 *       堆：储存变量，内存由gc自动清理
 *       栈：储存new分配的内存块儿，内存由编译器释放
 *       自由储存区：
 *       全局/静态储存区：全局变量，静态变量
 *       常量储存区：储存常量，不允许修改
 *       
 * 19···string驻留：string内容相同的会有相同的引用，但不是全部，动态创建的不会驻留
 * 
 * ···总结起来自我感觉就是：
 *     1、html解决了
 *     2、css也解决了
 *     3、JQuery还没解决，还有一些语法不太懂。
 *     
 * 然后基础也差不多学完了，陶哥指导我写一些小项目吧~
 *     
 * 
 */
    class 总结
    {
        static void Run()
        {
            Method();
            Console.ReadKey();
        }
        static void Method()
        {
            string str1 = "ABCD1234";
            string str2 = "ABCD1234";
            string str3 = "ABCD";
            string str4 = "1234";
            string str5 = "ABCD" + "1234";
            string str6 = "ABCD" + str4;
            string str7 = str3 + str4;

            Console.WriteLine("string str1 = \"ABCD1234\";");
            Console.WriteLine("string str2 = \"ABCD1234\";");
            Console.WriteLine("string str3 = \"ABCD\";");
            Console.WriteLine("string str4 = \"1234\";");
            Console.WriteLine("string str5 = \"ABCD\" + \"1234\";");
            Console.WriteLine("string str6 = \"ABCD\" + str4;");
            Console.WriteLine("string str7 = str3 + str4;");

            Console.WriteLine("\nobject.ReferenceEquals(str1, str2) = {0}", object.ReferenceEquals(str1, str2));
            Console.WriteLine("object.ReferenceEquals(str1,  \"ABCD1234\") = {0}", object.ReferenceEquals(str1, "ABCD1234"));

            Console.WriteLine("\nobject.ReferenceEquals(str1, str5) = {0}", object.ReferenceEquals(str1, str5));
            Console.WriteLine("object.ReferenceEquals(str1, str6) = {0}", object.ReferenceEquals(str1, str6));
            Console.WriteLine("object.ReferenceEquals(str1, str7) = {0}", object.ReferenceEquals(str1, str7));

            Console.WriteLine("\nobject.ReferenceEquals(str1, string.Intern(str6)) = {0}", object.ReferenceEquals(str1, string.Intern(str6)));
            Console.WriteLine("object.ReferenceEquals(str1, string.Intern(str7)) = {0}", object.ReferenceEquals(str1, string.Intern(str7)));
        }
    }
}
