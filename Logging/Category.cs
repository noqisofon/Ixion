/* -*- encoding: utf-8; -*- */
using System;


namespace Vanila.Logging {


    using Vanila.Logging.Spi;


    /// <summary>
    /// 
    /// </summary>
    public class Category : AppenderAttachable {
        /// <summary>
        /// 
        /// </summary>
        protected bool additive_;
        /// <summary>
        /// 
        /// </summary>
        protected Level level_;
        /// <summary>
        /// 
        /// </summary>
        protected Category parent_;
    }


}
