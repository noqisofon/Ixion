/* -*- encoding: utf-8 -*- */
using System;


namespace Vanila.Logging.Spi {


    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LoggingEvent {
        /// <summary>
        /// 
        /// </summary>
        public LoggingEvent() {
        }


        /// <summary>
        /// 
        /// </summary>
        public string FqnOfLoggerClass {
            get { return this.fqn_of_category_class_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Level Level {
            get { return this.level_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public LocationInfo LocationInfomation {
            get { return this.location_infomation_; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string LoggerName {
            get { return this.logger_name_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Category Logger {
            get { return this.logger_; }
        }


        /// <summary>
        /// 
        /// </summary>
        private string fqn_of_category_class_;
        /// <summary>
        /// 
        /// </summary>
        private Level level_;
        /// <summary>
        /// 
        /// </summary>
        private LocationInfo location_infomation_;
        /// <summary>
        /// 
        /// </summary>
        private string logger_name_;
        /// <summary>
        /// 
        /// </summary>
        private Category logger_;
        /// <summary>
        /// 
        /// </summary>
        private long time_stamp_;

    }


}
