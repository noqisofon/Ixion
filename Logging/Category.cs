/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;


namespace Ixion.Logging {


    using Ixion.Logging.Spi;


    /// <summary>
    /// 
    /// </summary>
    public class Category : IAppenderAttachable {
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
        public ILoggerRepository LoggerRepository {
            get { return this.repository_; }
        }


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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Category GetRoot() {
            if ( Root == null ) {
                return null;
            }
            return Root;
        }


        #region protected methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetResourceBundleString(string key) {
            return string.Empty;
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


        /// <summary>
        /// 
        /// </summary>
        private static Category Root = null;
    }


}
