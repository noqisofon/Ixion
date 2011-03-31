/* -*- encoding: utf-8 -*- */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Ixion.Logging {


    using Ixion.Logging.Helper;
    using Ixion.Logging.Spi;


    /// <summary>
    ///
    /// </summary>
    public class TextWriterAppender : AppenderSkeleton {
        /// <summary>
        /// 
        /// </summary>
        public TextWriterAppender()
            : base() {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        protected TextWriterAppender(Layout layout)
            : base() {
            this.layout_ = layout;
            this.writer_ = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="stream"></param>
        public TextWriterAppender(Layout layout, Stream stream)
            : this( layout, new StreamWriter( stream, Encoding.Default ) ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="writer"></param>
        public TextWriterAppender(Layout layout, TextWriter writer)
            : base() {
            base.Layout = layout;
            this.SetWriter( writer );
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
            get { return this.encoding_; }
            set { this.encoding_ = value; }
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
        public override bool RequiresLayout {
            get { return true; }
        }


        #region protected-methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        protected override void Append(LoggingEvent log_event) {
            this.WriteHeader();
            this.writer_.Write( base.Layout.Format( log_event ) );
            this.WriteFooter();

            if ( this.ImmediateFlush )
                this.writer_.Flush();
        }


        /// <summary>
        /// 
        /// </summary>
        protected void WriteHeader() {
            this.writer_.Write( base.Layout.Header );
        }


        /// <summary>
        /// 
        /// </summary>
        protected void WriteFooter() {
            this.writer_.Write( base.Layout.Footer );
        }


        /// <summary>
        /// 
        /// </summary>
        protected virtual void Reset() {
            this.writer_ = null;

            if ( !base.closed )
                base.closed = true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected void SetWriter(TextWriter writer) {
            this.Reset();

            if ( writer is QuietWriter )
                this.writer_ = writer;
            else
                this.writer_ = new QuietWriter( writer, base.ErrorHandler );
        }


        /// <summary>
        /// 
        /// </summary>
        protected virtual void CloseWriter() {
            if ( !base.closed ) {
                this.writer_.Close();
                base.closed = true;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dispoing"></param>
        protected override void Dispose(bool dispoing) {
            if ( dispoing )
                this.CloseWriter();

            this.Reset();

        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        private Encoding encoding_;
        /// <summary>
        /// 毎回 Append() 後にフラッシュするかどうかを表します。
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
