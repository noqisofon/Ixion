using System;


namespace Ixion.Logging.Spi {


    /// <summary>
    ///
    /// </summary>
    public interface ILoggerFactory {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Logger MakeNewLoggerInstance(string name);
    }


}
