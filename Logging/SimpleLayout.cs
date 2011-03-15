using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging {


    using Ixion.Logging.Spi;


    /// <summary>
    ///
    /// </summary>
    public class SimpleLayout : Layout {
        /// <summary>
        /// 
        /// </summary>
        public SimpleLayout() {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        /// <returns></returns>
        public override string Format(LoggingEvent log_event) {
            return string.Format( "{0} - {1}", log_event.Level, log_event.Message );
        }


        /// <summary>
        /// SimpleLayout は LoggingEvent オブジェクト中に含まれる Exception オブジェクトを処理しません。
        /// ですので、真を返します。
        /// </summary>
        public override bool IgnoreException {
            get { return true; }
        }
    }


}
