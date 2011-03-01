/* -*- encoding: utf-8; -*- */
using System;


namespace Ixion.Logging {


    using Spi;


    /// <summary>
    ///
    /// </summary>
    public abstract class Layout : ILayout {
        /// <summary>
        /// 
        /// </summary>
        protected Layout() {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        /// <returns></returns>
        public abstract string Format(LoggingEvent log_event);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string ContentType {
            get { return string.Empty; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string  Footer {
            get { return string.Empty; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string Header {
            get { return string.Empty; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool IgnoreException { get; }
    }


}
