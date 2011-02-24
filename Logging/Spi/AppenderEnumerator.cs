using System;
using System.Collections;
using System.Collections.Generic;


namespace Ixion.Logging.Spi {


    /// <summary>
    ///
    /// </summary>
    internal class AppenderEnumerator : IDisposable, IEnumerator<Appender> {
        /// <summary>
        /// 
        /// </summary>
        public AppenderEnumerator(Appender[] appenders) {
            this.appenders_ = appenders;
            this.current_ = default( Appender );
            this.index_ = 0;
        }


        #region IDisposable ƒƒ“ƒo
        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
        }
        #endregion





        #region IEnumerator ƒƒ“ƒo
        /// <summary>
        /// 
        /// </summary>
        object IEnumerator.Current {
            get { return this.current_; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool MoveNext() {
            if ( this.appenders_.Length > this.index_ ) {
                ++this.index_;
                this.current_ = this.appenders_[this.index_];
            } else
                return false;
            
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        public void Reset() {
            this.index_ = 0;
            this.current_ = this.appenders_[this.index_];
        }
        #endregion


        #region IEnumerator<Appender> ƒƒ“ƒo
        /// <summary>
        /// 
        /// </summary>
        public Appender Current {
            get { return this.current_; }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        private int index_;
        /// <summary>
        /// 
        /// </summary>
        private Appender[] appenders_;
        /// <summary>
        /// 
        /// </summary>
        private Appender current_;
    }


}
