/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;


namespace Vanila.Logging.Spi {


    /// <summary>
    ///
    /// </summary>
    public interface LoggerRepository {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        void EmitNoAppenderWarning(Category category);


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
        void FireAddAppenderEvent(Category logger, Appender appender);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator<Category> GetCurrentLoggers();
    }


}
