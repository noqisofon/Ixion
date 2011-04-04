/* -*- encoding: utf-8; -*- */
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace Ixion.Etherial {


    /// <summary>
    /// 
    /// </summary>
    public class NetworkStreamWriter : TextWriter {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public NetworkStreamWriter(NetworkStream stream)
            : this( stream, new UTF8Encoding( false, true ), STREAM_BUFFER_SIZE ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        public NetworkStreamWriter(NetworkStream stream, Encoding encoding)
            : this( stream, encoding, STREAM_BUFFER_SIZE ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <param name="buffer_size"></param>
        public NetworkStreamWriter(NetworkStream stream, Encoding encoding, int buffer_size) {
            if ( stream == null )
                throw new ArgumentNullException( "stream" );
            if ( encoding == null )
                throw new ArgumentNullException( "encoding" );
            if ( !stream.CanWrite )
                throw new ArgumentException( "指定されたストリームは書き込み処理をサポートしていません。" );
            if ( buffer_size <= 0 )
                throw new ArgumentOutOfRangeException( "buffer_size" );

            if ( buffer_size < MINIMUM_BUFFER_SIZE )
                buffer_size = MINIMUM_BUFFER_SIZE;

            this.base_stream_ = stream;
            this.encoding_ = encoding;
            this.encoder_ = encoding.GetEncoder();
            this.buffer_size_ = buffer_size;
            this.input_buffer_ = new char[buffer_size];
            this.input_buffer_length_ = 0;
            //this.output_buffer_ = new byte[encoding.GetMaxByteCount( buffer_size )];
            this.temporary_stream_ = new MemoryStream();
            this.auto_flush_ = false;
            this.stream_owner_ = false;

            WritePreamble();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public NetworkStreamWriter(TcpClient client)
            : this( client.GetStream() ) {
        }


        /// <summary>
        /// 
        /// </summary>
        ~NetworkStreamWriter() {
            this.Dispose( false );
        }


        /// <summary>
        /// 
        /// </summary>
        public override Encoding Encoding {
            get { return this.encoding_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public override void Flush() {
            if ( this.base_stream_ == null )
                throw new ObjectDisposedException( "base_stream_" );

            this.InnerEncode( false );
            byte[] output_buffer = this.temporary_stream_.ToArray();
            this.ForcedWrite( output_buffer, 0, output_buffer.Length );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(string value) {
            if ( value == null )
                return;

            int writing_size;
            int index = 0;
            int count = value.Length;

            if ( base_stream_ == null )
                throw new ObjectDisposedException( "base_stream_" );

            while ( count > 0 ) {
                writing_size = this.buffer_size_ - this.input_buffer_length_;
                if ( writing_size > count )
                    writing_size = count;

                value.CopyTo( index, this.input_buffer_, this.input_buffer_length_, writing_size );
                index += writing_size;
                count -= writing_size;
                this.input_buffer_length_ += writing_size;

                if ( this.input_buffer_length_ >= this.buffer_size_ )
                    this.InnerEncode( false );
            }

            if ( this.auto_flush_ )
                this.Flush();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing) {
            if ( this.base_stream_ != null ) {
                this.Flush();

                if ( this.stream_owner_ )
                    this.base_stream_.Close();

                this.base_stream_ = null;
            }
            this.input_buffer_ = null;
            this.input_buffer_length_ = 0;
            this.temporary_stream_.Close();
            this.buffer_size_ = 0;

            base.Dispose( disposing );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="flush"></param>
        private void InnerEncode(bool flush) {
            if ( this.input_buffer_length_ > 0 ) {
                byte[] output_buffer = new byte[this.encoding_.GetMaxByteCount( this.input_buffer_length_ )];
                int len = this.encoder_.GetBytes( this.input_buffer_,
                                                  0,
                                                  this.input_buffer_length_,
                                                  output_buffer,
                                                  0,
                                                  flush );
                if ( len > 0 )
                    this.temporary_stream_.Write( output_buffer, 0, len );

                this.input_buffer_length_ = 0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void WritePreamble() {
            byte[] preamble = this.encoding_.GetPreamble();

            if ( preamble != null )
                this.base_stream_.Write( preamble, 0, preamble.Length );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        private void ForcedWrite(byte[] buffer, int offset, int length) {
            int trial_count = 0;
            ManualResetEvent wrote = new ManualResetEvent( false );
            TimeSpan waiting_time = TimeSpan.FromMilliseconds( 100 );

            IAsyncResult byte_writing = this.base_stream_.BeginWrite( buffer, offset, length, completed, wrote );
            do {
                if ( trial_count >= MAX_TRIALS )
                    break;

                if ( wrote.WaitOne( waiting_time ) )
                    break;
                if ( byte_writing.AsyncWaitHandle.WaitOne( 0 ) )
                    continue;
            } while ( !byte_writing.IsCompleted );
            this.base_stream_.EndWrite( byte_writing );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        private static void completed(IAsyncResult ar) {
            ManualResetEvent signal = ar.AsyncState as ManualResetEvent;

            if ( ar.IsCompleted && ar.CompletedSynchronously )
                signal.Set();
        }


        /// <summary>
        /// 
        /// </summary>
        private NetworkStream base_stream_;
        /// <summary>
        /// 
        /// </summary>
        private Encoding encoding_;
        /// <summary>
        /// 
        /// </summary>
        private Encoder encoder_;
        /// <summary>
        /// 
        /// </summary>
        private int buffer_size_;
        /// <summary>
        /// 
        /// </summary>
        private char[] input_buffer_;
        /// <summary>
        /// 
        /// </summary>
        private int input_buffer_length_;
        /// <summary>
        /// 
        /// </summary>
        private MemoryStream temporary_stream_;
        /// <summary>
        /// 
        /// </summary>
        private bool auto_flush_;
        /// <summary>
        /// 
        /// </summary>
        private bool stream_owner_;


        /// <summary>
        /// 
        /// </summary>
        private const int STREAM_BUFFER_SIZE = 1024;
        /// <summary>
        /// 
        /// </summary>
        private const int FILE_BUFFER_SIZE = 4096;
        /// <summary>
        /// 
        /// </summary>
        private const int MINIMUM_BUFFER_SIZE = 128;
        /// <summary>
        /// 
        /// </summary>
        private const int MAX_TRIALS = 0;

    }


}
