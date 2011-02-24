/* -*- encoding: utf-8 -*- */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Ixion.Logging {


    using Ixion.Logging.Spi;


    /// <summary>
    ///
    /// </summary>
    public class WriterAppender : AppenderSkeleton {
        /// <summary>
        /// 
        /// </summary>
        public WriterAppender() {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="stream"></param>
        public WriterAppender(Layout layout, Stream stream)
            : this( layout, new StreamWriter( stream, Encoding.Default ) ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="writer"></param>
        public WriterAppender(Layout layout, TextWriter writer) {
            this.layout_ = layout;
            this.writer_ = writer;
        }


        /// <summary>
        /// 
        /// </summary>
        public override void Close() {
            this.Dispose( true );
        }


        /// <summary>
        /// 
        /// </summary>
        public Encoding Encoding {
            get { return this.writer_.Encoding; }
            //set { this.encoding_ = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public bool ImmediateFlush {
            get { return this.immediate_flush_; }
            set { this.immediate_flush_ = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool RequiresLayout() {
            return false;
        }


        #region protected-methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        protected override void Append(LoggingEvent log_event) {
            this.writer_.Write(this.layout_.Format(log_event));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dispoing"></param>
        protected override void Dispose(bool dispoing) {
            if ( !base.closed ) {
                if ( dispoing ) {
                    this.writer_.Close();
                }
                this.writer_ = null;
                base.closed = true;
            }
        }
        #endregion


        ///// <summary>
        ///// 
        ///// </summary>
        //private Encoding encoding_;
        /// <summary>
        /// 
        /// </summary>
        private bool immediate_flush_;
        /// <summary>
        /// 
        /// </summary>
        private TextWriter writer_;
        /// <summary>
        /// 
        /// </summary>
        private Layout layout_;
    }


}
