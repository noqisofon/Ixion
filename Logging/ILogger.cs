/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging {


    using Ixion.Logging.Spi;


    /// <summary>
    /// 
    /// </summary>
    public interface ILogger {
        /// <summary>
        /// 
        /// </summary>
        ILoggerRepository Repository { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        bool IsEnabledFor(Level level);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="declare_type"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        void Log(Type declare_type, Level level, object message, Exception e);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        void Log(LoggingEvent log_event);
    }


}
