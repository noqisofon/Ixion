/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Reflection;


namespace Ixion.Logging {


    using Ixion.Logging.Spi;


    /// <summary>
    /// 
    /// </summary>
    public class Category : IAppenderAttachable, ILogger {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        protected Category(string name) {
            this.name_ = name;
            this.appenders_ = new List<IAppender>();
        }


        /// <summary>
        /// 
        /// </summary>
        public Category Parent {
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
            Log( level, message, null );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Log(Level level, object message, Exception e) {
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


        #region Ithis.Logger メンバ
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
        public bool IsEnableFor(Level level) {
            return this.Level.GetHashCode() >= level.GetHashCode();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        public void Log(LoggingEvent log_event) {
            if ( log_event == null )
                return;

            if ( this.IsEnableFor( log_event.Level ) )
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
            if ( IsEnableFor( level ) )
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


        /// <summary>
        /// 
        /// </summary>
        private bool additive_ = false;
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
        private Category parent_ = null;
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
