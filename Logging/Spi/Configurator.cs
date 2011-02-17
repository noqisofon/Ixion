/* -*- encoding: utf-8; -*- */
using System;


namespace Vanila.Logging.Spi {


    /// <summary>
    ///
    /// </summary>
    public interface Configurator {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="repository"></param>
        void DoConfigure(Uri url, LoggerRepository repository);
    }


}
