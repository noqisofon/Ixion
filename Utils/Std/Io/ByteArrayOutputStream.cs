/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Std.Io {


    /// <summary>
    ///
    /// </summary>
    public class ByteArrayOutputStream : OutputStream {
        /// <summary>
        /// 
        /// </summary>
        public ByteArrayOutputStream()
            : this( DEFAULT_BUFFER_SIZE ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        public ByteArrayOutputStream(int size) {
            if ( size < DEFAULT_BUFFER_SIZE )
                size = DEFAULT_BUFFER_SIZE;

            this.byte_sequence_ = new byte[size];
            this.count_ = 0;
            this.capacity_ = size;
        }


        /// <summary>
        /// 
        /// </summary>
        public int Size {
            get { return this.capacity_; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        public override void Write(int b) {
            this.byte_sequence_[this.count_++] = (byte)( 0xff & b );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        public override void Write(byte[] buffer, int offset, int len) {
            if ( buffer == null )
                throw new ArgumentNullException( "buffer" );
            if ( offset < 0 )
                throw new ArgumentOutOfRangeException( "offset" );
            if ( len < 0 )
                throw new ArgumentOutOfRangeException( "len" );
            if ( buffer.Length < ( len - offset ) )
                throw new ArgumentException( "指定されたバイト列の長さより長いバイト列を書き込もうとしました。" );
            if ( ( this.capacity_ - this.count_ ) < len )
                len = this.capacity_ - this.count_;

            Array.Copy( buffer, offset, this.byte_sequence_, this.count_, len );
            this.count_ += len;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        public void WriteTo(OutputStream output) {
            output.Write( this.byte_sequence_, 0, this.byte_sequence_.Length );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray() {
            byte[] result = new byte[this.capacity_];
            Array.Copy( this.byte_sequence_, 0, result, 0, result.Length );

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            StringBuilder builder = new StringBuilder();

            foreach ( byte b in this.byte_sequence_ ) {
                builder.AppendFormat( "%{0:x}", b );
            }
            return builder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding_name"></param>
        /// <returns></returns>
        public string ToString(string encoding_name) {
            if ( encoding_name == null )
                throw new ArgumentNullException( "encoding_name" );

            Encoding encoding = null;
            try {
                encoding = Encoding.GetEncoding( encoding_name );
            } catch ( ArgumentException ) {
                encoding = Encoding.Default;
            }
            return this.ToString( encoding );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public string ToString(Encoding encoding) {
            return encoding.GetString( this.byte_sequence_ );
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
        private int capacity_;


        /// <summary>
        /// 
        /// </summary>
        private const int DEFAULT_BUFFER_SIZE = 128;
    }


}
