/* -*- encoding: utf-8; -*- */
using System;
using System.IO;


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
        /// <param name="writer"></param>
        /// <param name="log_event"></param>
        public void Format(TextWriter writer, LoggingEvent log_event) {
            writer.Write( convert( log_event ) );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="n"></param>
        public virtual void SpacePad(TextWriter writer, int n) {
            writer.Write( "    " );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other_next"></param>
        /// <returns></returns>
        public PatternConvertor SetNext(PatternConvertor other_next) {
            this.next_ = other_next;

            return this.next_;
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
