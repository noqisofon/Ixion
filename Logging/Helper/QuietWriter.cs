/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Ixion.Logging.Helper {


    using Ixion.Logging.Spi;


    /// <summary>
    /// 
    /// </summary>
    public class QuietWriter : TextWriter {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="error_handler"></param>
        public QuietWriter(TextWriter writer, IErrorHandler error_handler) {
            if ( error_handler == null )
                throw new ArgumentNullException( "error_handler" );

            this.writer_ = writer;
            this.error_handler_ = error_handler;
        }


        /// <summary>
        /// 
        /// </summary>
        public override Encoding Encoding {
            get { return this.writer_.Encoding; }
        }


        /// <summary>
        /// 
        /// </summary>
        public IErrorHandler ErrorHandler {
            get { return this.error_handler_; }
            set {
                if ( value == null )
                    throw new ArgumentNullException( "value" );

                this.error_handler_ = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public override void Flush() {
            try {
                this.writer_.Flush();
            } catch ( Exception e ) {
                this.error_handler_.Error( e.Message, e, ErrorCode.FlushFailure );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(object value) {
            try {
                this.writer_.Write( value );
            } catch ( Exception e ) {
                this.error_handler_.Error( e.Message, e, ErrorCode.WriteFailure );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        public override void Write(string format, object arg0) {
            try {
                this.writer_.Write( format, arg0 );
            } catch ( Exception e ) {
                this.error_handler_.Error( e.Message, e, ErrorCode.WriteFailure );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public override void Write(string format, params object[] args) {
            try {
                this.writer_.Write( format, args );
            } catch ( Exception e ) {
                this.error_handler_.Error( e.Message, e, ErrorCode.WriteFailure );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(object value) {
            try {
                this.writer_.WriteLine( value );
            } catch ( Exception e ) {

                this.error_handler_.Error( e.Message, e, ErrorCode.WriteFailure );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        public override void WriteLine(string format, object arg0) {
            try {
                this.writer_.WriteLine( format, arg0 );
            } catch ( Exception e ) {
                this.error_handler_.Error( e.Message, e, ErrorCode.WriteFailure );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public override void WriteLine(string format, params object[] args) {
            try {
                this.writer_.WriteLine( format, args );
            } catch ( Exception e ) {
                this.error_handler_.Error( e.Message, e, ErrorCode.WriteFailure );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private TextWriter writer_;
        /// <summary>
        /// 
        /// </summary>
        private IErrorHandler error_handler_;

    }


}
