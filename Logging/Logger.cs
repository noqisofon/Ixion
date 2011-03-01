/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging {


    using Ixion.Logging.Spi;


    /// <summary>
    ///
    /// </summary>
    public class Logger : IAppenderAttachable, ILogger, ILoggerAdapter {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        protected Logger(string name) {
            this.name_ = name;
            this.appenders_ = new List<IAppender>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        protected Logger(string name, Logger parent) {
            this.name_ = name;
            this.parent_ = parent;
            this.appenders_ = new List<IAppender>();
        }


        /// <summary>
        /// 
        /// </summary>
        public bool IsDebugEnabled {
            get { return this.IsEnabledFor( Level.Debug ); }
        }


        /// <summary>
        /// 
        /// </summary>
        public bool IsInfoEnabled {
            get { return this.IsEnabledFor( Level.Info ); }
        }


        /// <summary>
        /// 
        /// </summary>
        public bool IsTraceEnable {
            get {
                return this.IsEnabledFor( Level.Trace );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public Level EffectiveLevel {
            get {
                for ( Logger logger = this; logger != null; logger = logger.Parent ) {
                    Level level = logger.Level;

                    if ( (object)level != null )
                        return level;
                }
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="assertion"></param>
        /// <param name="message"></param>
        public void AssertLog(bool assertion, string message) {
            if ( !assertion )
                this.Error( message );
        }


        /// <summary>
        /// 
        /// </summary>
        public Logger Parent {
            get { return this.parent_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string Name {
            get { return this.name_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Level Level {
            get { return this.level_; }
            set { this.level_ = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public bool Additivity {
            get { return this.additive_; }
            set { this.additive_ = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public void Log(Level level, object message) {
            if ( IsEnabledFor( level ) )
                this.Log( level, message, null );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Log(Level level, object message, Exception e) {
            if ( IsEnabledFor( level ) )
                this.Log( level, message, e );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        public void CallAppenders(LoggingEvent log_event) {
            foreach ( IAppender appender in this.appenders_ ) {
                appender.DoAppend( log_event );
            }
        }


        #region ILogger メンバ
        /// <summary>
        /// 
        /// </summary>
        public ILoggerRepository Repository {
            get { return this.repository_; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public bool IsEnabledFor(Level level) {
            return this.Level.GetHashCode() >= level.GetHashCode();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        public void Log(LoggingEvent log_event) {
            if ( log_event == null )
                return;

            if ( this.IsEnabledFor( log_event.Level ) )
                ForcedLog( log_event );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="declare_type"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Log(Type declare_type, Level level, object message, Exception e) {
            if ( IsEnabledFor( level ) )
                ForcedLog( declare_type, level, message, e );
        }
        #endregion


        #region AppenderAttachable メンバ
        /// <summary>
        /// 
        /// </summary>
        public bool IsAttached {
            get { return this.appenders_.Count > 0; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_appender"></param>
        public void AddAppender(IAppender new_appender) {
            this.appenders_.Add( new_appender );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IAppender> GetAllAppenders() {
            return this.appenders_.GetEnumerator();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IAppender GetAppender(string name) {
            foreach ( IAppender appender in this.appenders_ ) {
                if ( appender.Name == name )
                    return appender;
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        public void RemoveAllAppenders() {
            this.appenders_.Clear();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appender"></param>
        public void RemoveAppender(IAppender appender) {
            this.appenders_.Remove( appender );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void RemoveAppender(string name) {
            foreach ( IAppender appender in this.appenders_ ) {
                if ( appender.Name == name ) {
                    this.appenders_.Remove( appender );

                    break;
                }
            }
        }
        #endregion


        #region protected methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetResourceBundleString(string key) {
            return string.Empty;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected void ForcedLog(LoggingEvent log_event) {
            //log_event.EnsureRepository(this.Hierarchy);
            CallAppenders( log_event );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="declare_type"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        protected void ForcedLog(Type declare_type, Level level, object message, Exception e) {
            CallAppenders( new LoggingEvent( declare_type, this, level, message, e ) );
        }
        #endregion


        #region ILoggerAdapter メンバー
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Debug(object message) {
            this.Log( Level.Debug, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Debug(object message, Exception e) {
            this.Log( Level.Debug, message, e );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Error(object message) {
            this.Log( Level.Error, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Error(object message, Exception e) {
            this.Log( Level.Error, message, e );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(object message) {
            this.Log( Level.Fatal, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Fatal(object message, Exception e) {
            this.Log( Level.Fatal, message, e );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Warn(object message) {
            this.Log( Level.Warn, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Warn(object message, Exception e) {
            this.Log( Level.Warn, message, e );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Info(object message) {
            this.Log( Level.Info, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Info(object message, Exception e) {
            this.Log( Level.Info, message, e );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Trace(object message) {
            this.Log( Level.Trace, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Trace(object message, Exception e) {
            this.Log( Level.Trace, message, e );
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Logger GetLogger(string name) {
            return new Logger( name );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Logger GetLogger(Type type) {
            return new Logger( type.Name );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static Logger GetLogger(string name, ILoggerFactory factory) {
            return factory.MakeNewLoggerInstance( name );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Logger GetRootLogger() {
            return new RootLogger();
        }


        /// <summary>
        /// 
        /// </summary>
        private bool additive_ = true;
        /// <summary>
        /// 
        /// </summary>
        private Level level_ = Level.Debug;
        /// <summary>
        /// 
        /// </summary>
        private string name_ = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private Logger parent_ = null;
        /// <summary>
        /// 
        /// </summary>
        private ILoggerRepository repository_ = null;
        ///// <summary>
        ///// 
        ///// </summary>
        //private ResourceBundle resource_bundle_;
        /// <summary>
        /// 
        /// </summary>
        private IList<IAppender> appenders_ = null;
    }


}
