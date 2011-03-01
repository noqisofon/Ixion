/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging {


    using Ixion.Logging.Spi;


    /// <summary>
    /// 
    /// </summary>
    public interface ILayout {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ContentType { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string Header { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string Footer { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IgnoreException { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        /// <returns></returns>
        string Format(LoggingEvent log_event);
    }


}
