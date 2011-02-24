/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging {


    using Ixion.Logging.Spi;


    /// <summary>
    ///
    /// </summary>
    public interface IAppender {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_filter"></param>
        void AddFilter(Filter new_filter);


        /// <summary>
        /// 
        /// </summary>
        void ClearFilters();


        /// <summary>
        /// 
        /// </summary>
        void Close();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        void DoAppend(LoggingEvent log_event);


        /// <summary>
        /// 
        /// </summary>
        Filter Filter { get; }


        /// <summary>
        /// 
        /// </summary>
        Layout Layout { get; set; }


        /// <summary>
        /// 
        /// </summary>
        string Name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        IErrorHandler ErrorHandler { get; set; }


        /// <summary>
        /// 
        /// </summary>
        bool RequiresLayout();
    }


}
