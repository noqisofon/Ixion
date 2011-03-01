/* -*- encoding: utf-8; -*- */
using System;
using System.Text;


namespace Ixion.Logging.Helper {


    using Ixion.Logging.Spi;


    /// <summary>
    /// 派生したクラスが必要とするフォーマット機能を提供する抽象クラスです。
    /// </summary>
    public abstract class PatternConvertor {
        /// <summary>
        /// 
        /// </summary>
        protected PatternConvertor() {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fi"></param>
        protected PatternConvertor(FormattingInfo fi) {
        }


        /// <summary>
        /// 指定された変換方法でフォーマットするためのテンプレートメソッドです。
        /// </summary>
        /// <param name="sbuilder"></param>
        /// <param name="log_event"></param>
        public void Format(StringBuilder sbuilder, LoggingEvent log_event) {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sbuilder"></param>
        /// <param name="k"></param>
        public void SpacePad(StringBuilder sbuilder, int k) {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        /// <returns></returns>
        internal abstract string convert(LoggingEvent log_event);


        /// <summary>
        /// 
        /// </summary>
        private PatternConvertor next_;
    }


}
