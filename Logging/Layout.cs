/* -*- encoding: utf-8; -*- */
using System;


namespace Ixion.Logging {


    using Spi;


    /// <summary>
    ///
    /// </summary>
    public abstract class Layout {
        /// <summary>
        /// 
        /// </summary>
        public Layout() {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logging_event"></param>
        /// <returns></returns>
        public abstract string Format(LoggingEvent logging_event);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetContentType() {
            return string.Empty;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetFooter() {
            return string.Empty;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetHeader() {
            return string.Empty;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool IgnoreThrowable();
    }


}
