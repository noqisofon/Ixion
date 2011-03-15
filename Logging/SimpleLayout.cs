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
        /// SimpleLayout �� LoggingEvent �I�u�W�F�N�g���Ɋ܂܂�� Exception �I�u�W�F�N�g���������܂���B
        /// �ł��̂ŁA�^��Ԃ��܂��B
        /// </summary>
        public override bool IgnoreException {
            get { return true; }
        }
    }


}
