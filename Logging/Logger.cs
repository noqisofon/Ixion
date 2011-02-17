using System;
using System.Collections.Generic;
using System.Text;


namespace Vanila.Logging {


    /// <summary>
    ///
    /// </summary>
    public class Logger {


        /// <summary>
        /// 
        /// </summary>
        private Level loglevel_;
        /// <summary>
        /// 
        /// </summary>
        private Layout layout_;
        /// <summary>
        /// 
        /// </summary>
        private List<Appender> appenders_ = new List<Appender>();
    }


}
