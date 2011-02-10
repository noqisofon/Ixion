using System;
using System.Collections.Generic;
using System.Text;


namespace Vanila.Logging {


    using Vanila.Logging.Spi;


    /// <summary>
    ///
    /// </summary>
    public interface Appender {
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
    }


}
