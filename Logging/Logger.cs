/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging {


    using Ixion.Logging.Spi;


    /// <summary>
    ///
    /// </summary>
    public class Logger : Category, ILoggerAdapter {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Logger(string name)
            : base( name ) {
        }


        /// <summary>
        /// 
        /// </summary>
        public bool IsDebugEnabled {
            get { return base.IsEnableFor( Level.Debug ); }
        }


        /// <summary>
        /// 
        /// </summary>
        public bool IsInfoEnabled {
            get { return base.IsEnableFor( Level.Info ); }
        }


        /// <summary>
        /// 
        /// </summary>
        public bool IsTraceEnable {
            get {
                return base.IsEnableFor( Level.Trace );
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


        #region ILoggerAdapter ÉÅÉìÉoÅ[
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Debug(object message) {
            base.Log( Level.Debug, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Debug(object message, Exception e) {
            base.Log( Level.Debug, message, e );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Error(object message) {
            base.Log( Level.Error, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Error(object message, Exception e) {
            base.Log( Level.Error, message, e );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(object message) {
            base.Log( Level.Fatal, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Fatal(object message, Exception e) {
            base.Log( Level.Fatal, message, e );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Warn(object message) {
            base.Log( Level.Warn, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Warn(object message, Exception e) {
            base.Log( Level.Warn, message, e );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Info(object message) {
            base.Log( Level.Info, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Info(object message, Exception e) {
            base.Log( Level.Info, message, e );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Trace(object message) {
            base.Log( Level.Trace, message );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public void Trace(object message, Exception e) {
            base.Log(Level.Trace,message,e);
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
            return null;
        }
    }


}
