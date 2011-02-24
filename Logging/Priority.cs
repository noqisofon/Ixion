/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;


namespace Ixion.Logging {


    /// <summary>
    /// 
    /// </summary>
    public enum Priority : uint {
        /// <summary>
        /// 
        /// </summary>
        All = 1000,
        /// <summary>
        /// 
        /// </summary>
        Trace = 900,
        /// <summary>
        /// 
        /// </summary>
        Debug = 600,
        /// <summary>
        /// 
        /// </summary>
        Info = 500,
        /// <summary>
        /// 
        /// </summary>
        Warn = 400,
        /// <summary>
        /// 
        /// </summary>
        Error = 300,
        /// <summary>
        /// 
        /// </summary>
        Fatal = 100,
        /// <summary>
        /// 
        /// </summary>
        None = 0
    }


}
