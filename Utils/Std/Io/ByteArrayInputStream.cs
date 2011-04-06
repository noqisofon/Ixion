/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Std.Io {


    /// <summary>
    ///
    /// </summary>
    public class ByteArrayInputStream : InputStream {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        public ByteArrayInputStream(byte[] buffer)
            : this( buffer, 0, buffer.Length ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public ByteArrayInputStream(byte[] buffer, int offset, int length) {
            this.byte_sequence_ = buffer;
            this.offset_ = offset;
            this.count_ = length + 1;
            this.mark_ = offset;
            this.position_ = offset;
        }


        /// <summary>
        /// 
        /// </summary>
        public override int Available {
            get { return ( this.count_ - this.position_ ) - 1; }
        }


        /// <summary>
        /// 
        /// </summary>
        public override bool MarkSupported {
            get { return true; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="read_limit"></param>
        public override void Mark(int read_limit) {
            this.mark_ = this.position_;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public override long Skip(long n) {
            if ( this.Available == 0 )
                return 0;
            if ( this.Available < n )
                n = this.Available;

            this.position_ += (int)n;

            return n;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int Read() {
            if ( this.Available == 0 )
                return -1;

            return this.byte_sequence_[this.position_++];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int len) {
            if ( buffer == null )
                throw new ArgumentNullException( "buffer" );
            if ( offset < 0 )
                throw new ArgumentOutOfRangeException( "offset" );
            if ( len < 0 )
                throw new ArgumentOutOfRangeException( "len" );
            if ( buffer.Length < ( len - offset ) )
                throw new ArgumentException( "指定されたバイト列の長さより長いバイト列を読み込もうとしました。" );
            if ( this.Available == 0 )
                return -1;
            if ( this.Available < len )
                len = len - this.Available;

            Array.Copy( this.byte_sequence_, this.position_, buffer, offset, len );
            this.position_ += len;

            return len;
        }


        /// <summary>
        /// 
        /// </summary>
        public override void Reset() {
            this.position_ = this.mark_;
        }


        /// <summary>
        /// 
        /// </summary>
        private byte[] byte_sequence_;
        /// <summary>
        /// 
        /// </summary>
        private int count_;
        /// <summary>
        /// 
        /// </summary>
        private int mark_;
        /// <summary>
        /// 
        /// </summary>
        private int position_;
        /// <summary>
        /// 
        /// </summary>
        private int offset_;
    }


}
