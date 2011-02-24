/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging.Spi {


    /// <summary>
    ///
    /// </summary>
    public abstract class Filter : IOptionHandler {
        /// <summary>
        /// 
        /// </summary>
        public Filter() {
        }


        /// <summary>
        /// 
        /// </summary>
        public Filter Next {
            get { return this.next_; }
            set { this.next_ = value; }
        }


        #region IOptionHandler メンバ
        /// <summary>
        /// 
        /// </summary>
        public void ActiveOptions() {
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logging_event"></param>
        /// <returns></returns>
        public abstract int Decide(LoggingEvent logging_event);


        /// <summary>
        /// 
        /// </summary>
        private Filter next_ = null;
    }


}
