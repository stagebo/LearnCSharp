using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet与csharp
{
    class 事件
    {
        /*
        * 事件就是被简化的针对特殊用途的委托，注册到事件上的方法会在事件触发时被调用。事件包含一个委托
        *
        * 事件的属性：
        *          触发事件：调用或触发事件的术语，当事件触发时，所有注册到他的方法都会被调用
        *          发布者：让时间被其他类或者结构可见并使用的类或结构。 
        *          订阅者：吧事件和发布者关联注册的类或结构
        *          事件处理程序：注册到时间的方法，可以在事件所在的类或者机构中，或者在不同的类或结构中
        * 
        * 
        * 
        */

        static void Rum()
        {
        }
        //事件的声明
        //public event EventHandler e1, e2, e3;//当然，也可以是静态的
        //事件的声明需要一个委托。上面用的是。net预定义的标准事件委托EventHandler。
    }
}
