/* -*- encoding: utf-8; -*- */
using System;


namespace Ixion.Logging.Spi {


    /// <summary>
    /// 
    /// </summary>
    public interface IErrorHandler : IOptionHandler {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        /// <param name="error_code"></param>
        void Error(string message, Exception e, int error_code);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        /// <param name="error_code"></param>
        /// <param name="logging_event"></param>
        void Error(string message, Exception e, int error_code, LoggingEvent logging_event);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appender"></param>
        void SetAppender(IAppender appender);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appender"></param>
        void SetBackupAppender(IAppender appender);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        void SetLogger(Logger logger);
    }


}
