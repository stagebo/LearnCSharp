﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.Helper
{
    class MyException : ApplicationException
    {

        //
        // 摘要:
        //     Initializes a new instance of the System.Exception class.
        public MyException() : base() { }
        //
        // 摘要:
        //     Initializes a new instance of the System.Exception class with a specified error
        //     message.
        //
        // 参数:
        //   message:
        //     The message that describes the error.
        public MyException(string message) : base(message) { }
        //
        // 摘要:
        //     Initializes a new instance of the System.Exception class with a specified error
        //     message and a reference to the inner exception that is the cause of this exception.
        //
        // 参数:
        //   message:
        //     The error message that explains the reason for the exception.
        //
        //   innerException:
        //     The exception that is the cause of the current exception, or a null reference
        //     (Nothing in Visual Basic) if no inner exception is specified.
        public MyException(string message, Exception innerException) : base(message, innerException) { }


    }
}
