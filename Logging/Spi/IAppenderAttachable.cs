/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging.Spi {


    /// <summary>
    /// 
    /// </summary>
    public interface IAppenderAttachable {
        /// <summary>
        /// 
        /// </summary>
        bool IsAttached { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_appender"></param>
        void AddAppender(IAppender new_appender);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator<IAppender> GetAllAppenders();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IAppender GetAppender(string name);


        /// <summary>
        /// 
        /// </summary>
        void RemoveAllAppenders();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appender"></param>
        void RemoveAppender(IAppender appender);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void RemoveAppender(string name);
    }


}
