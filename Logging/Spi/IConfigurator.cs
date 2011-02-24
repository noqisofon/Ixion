/* -*- encoding: utf-8; -*- */
using System;


namespace Ixion.Logging.Spi {


    /// <summary>
    ///
    /// </summary>
    public interface IConfigurator {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="repository"></param>
        void DoConfigure(Uri url, ILoggerRepository repository);
    }


}
