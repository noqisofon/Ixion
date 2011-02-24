using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging {


    /// <summary>
    /// 
    /// </summary>
    public interface ILoggerAdapter {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Debug(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        void Debug(object message, Exception e);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Error(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        void Error(object message, Exception e);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Fatal(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        void Fatal(object message, Exception e);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Warn(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        void Warn(object message, Exception e);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Info(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        void Info(object message, Exception e);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Trace(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        void Trace(object message, Exception e);
    }


}
