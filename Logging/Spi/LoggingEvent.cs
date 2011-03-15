/* -*- encoding: utf-8 -*- */
using System;


namespace Ixion.Logging.Spi {


    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LoggingEvent {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fqn_of_category_class"></param>
        /// <param name="logger"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public LoggingEvent(Type fqn_of_category_class,
                             Logger logger,
                             Level level,
                             object message,
                             Exception e)
            : this( fqn_of_category_class, logger, DateTime.Now, level, message, e ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fqn_of_category_class"></param>
        /// <param name="logger"></param>
        /// <param name="time_stamp"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public LoggingEvent(Type fqn_of_category_class,
                             Logger logger,
                             DateTime time_stamp,
                             Level level,
                             object message,
                             Exception e) {
            this.fqn_of_category_class_ = fqn_of_category_class;
            this.level_ = level;
            this.message_ = message;
            this.logger_name_ = logger.Name;
            this.logger_ = logger;
            this.location_infomation_ = new LocationInfo( /*e, */fqn_of_category_class );
            this.time_stamp_ = time_stamp;
        }


        /// <summary>
        /// 
        /// </summary>
        public Type FqnOfLoggerClass {
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
        public object Message {
            get { return this.message_; }
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
        public Logger Logger {
            get { return this.logger_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public DateTime TimeStamp {
            get { return this.time_stamp_; }
        }


        /// <summary>
        /// 
        /// </summary>
        private Type fqn_of_category_class_;
        /// <summary>
        /// 
        /// </summary>
        private Level level_;
        /// <summary>
        /// 
        /// </summary>
        private object message_;
        /// <summary>
        /// 
        /// </summary>
        private LocationInfo location_infomation_ = null;
        /// <summary>
        /// 
        /// </summary>
        private string logger_name_;
        /// <summary>
        /// 
        /// </summary>
        private Logger logger_;
        /// <summary>
        /// 
        /// </summary>
        private DateTime time_stamp_;

    }


}
