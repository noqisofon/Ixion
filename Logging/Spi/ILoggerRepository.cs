/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;


namespace Ixion.Logging.Spi {


    /// <summary>
    ///
    /// </summary>
    public interface ILoggerRepository {
        /// <summary>
        /// 
        /// </summary>
        Level Threshold { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        void EmitNoAppenderWarning(Logger category);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Logger Exists(string name);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="appender"></param>
        void FireAddAppenderEvent(Logger logger, IAppender appender);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator<Logger> GetCurrentLoggers();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Logger GetLogger(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Logger GetLogger(string name, ILoggerFactory factory);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Logger RootLogger();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        bool IsDisabled(int level);


        /// <summary>
        /// 
        /// </summary>
        void ResetConfiguration();
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        void SetThresholdFromString(string level);


        /// <summary>
        /// 
        /// </summary>
        void Shotdown();
    }


}
