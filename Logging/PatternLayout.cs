/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging {


    using Ixion.Logging.Spi;


    /// <summary>
    ///
    /// </summary>
    public class PatternLayout : Layout {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        /// <returns></returns>
        public override string Format(LoggingEvent log_event) {
            return string.Format( "[{0}] - {1}", log_event.Level, log_event.Message );
        }


        /// <summary>
        /// 
        /// </summary>
        public override bool IgnoreException {
            get { return false; }
        }
    }


}
