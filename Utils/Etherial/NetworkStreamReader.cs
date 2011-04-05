/* -*- encoding: utf-8; -*- */
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace Ixion.Utils.Etherial {


    /// <summary>
    /// 
    /// </summary>
    public class NetworkStreamReader : TextReader {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public NetworkStreamReader(NetworkStream stream)
            : this( stream, Encoding.UTF8, true, STREAM_BUFFER_SIZE ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="detect_encoding_from_byteorder_marks"></param>
        public NetworkStreamReader(NetworkStream stream, bool detect_encoding_from_byteorder_marks)
            : this( stream, Encoding.UTF8, detect_encoding_from_byteorder_marks, STREAM_BUFFER_SIZE ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        public NetworkStreamReader(NetworkStream stream, Encoding encoding)
            : this( stream, encoding, true, STREAM_BUFFER_SIZE ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <param name="detect_encoding_from_byteorder_marks"></param>
        public NetworkStreamReader(NetworkStream stream, Encoding encoding, bool detect_encoding_from_byteorder_marks)
            : this( stream, encoding, detect_encoding_from_byteorder_marks, STREAM_BUFFER_SIZE ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <param name="detect_encoding_from_byteorder_marks"></param>
        /// <param name="buffer_size"></param>
        public NetworkStreamReader(NetworkStream stream, Encoding encoding, bool detect_encoding_from_byteorder_marks, int buffer_size) {
            if ( stream == null )
                throw new ArgumentNullException( "stream" );
            if ( encoding == null )
                throw new ArgumentNullException( "encoding" );
            if ( !stream.CanRead )
                throw new ArgumentException( "渡されたストリームは読込処理をサポートしていません。" );
            if ( buffer_size <= 0 )
                throw new ArgumentException( "buffer_size", "バッファサイズが負の値、または 0 です。" );

            if ( buffer_size < MINIMUM_BUFFER_SIZE )
                buffer_size = MINIMUM_BUFFER_SIZE;

            this.base_stream_ = stream;
            this.encoding_ = encoding;
            this.buffer_size_ = buffer_size;
            
            this.input_buffer_ = new byte[buffer_size];
            this.input_buffer_position_ = 0;
            this.input_buffer_length_ = 0;
            
            this.output_buffer_ = new char[buffer_size];
            this.output_buffer_position_ = 0;
            this.output_buffer_length_ = 0;
            
            this.saw_eof_ = false;
            this.stream_owner_ = false;
            this.encoding_detected_ = detect_encoding_from_byteorder_marks;

            this.decoder_ = encoding.GetDecoder();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public NetworkStreamReader(TcpClient client)
            : this( client, Encoding.UTF8, true, STREAM_BUFFER_SIZE ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="encoding"></param>
        public NetworkStreamReader(TcpClient client, Encoding encoding)
            : this( client, encoding, true, STREAM_BUFFER_SIZE ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="encoding"></param>
        /// <param name="detect_encoding_from_byteorder_marks"></param>
        public NetworkStreamReader(TcpClient client, Encoding encoding, bool detect_encoding_from_byteorder_marks)
            : this( client, encoding, detect_encoding_from_byteorder_marks, STREAM_BUFFER_SIZE ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="encoding"></param>
        /// <param name="detect_encoding_from_byteorder_marks"></param>
        /// <param name="buffer_size"></param>
        public NetworkStreamReader(TcpClient client, Encoding encoding, bool detect_encoding_from_byteorder_marks, int buffer_size) {
            if ( client == null )
                throw new ArgumentNullException( "client" );
            NetworkStream stream = client.GetStream();
            if ( !stream.CanRead )
                throw new ArgumentException( "渡されたストリームは読込処理をサポートしていません。" );
            if ( encoding == null )
                throw new ArgumentNullException( "encoding" );
            if ( buffer_size <= 0 )
                throw new ArgumentException( "buffer_size", "バッファサイズが負の値、または 0 です。" );

            if ( buffer_size < MINIMUM_BUFFER_SIZE )
                buffer_size = MINIMUM_BUFFER_SIZE;

            this.base_stream_ = stream;
            this.encoding_ = encoding;
            this.buffer_size_ = buffer_size;
            
            this.input_buffer_ = new byte[buffer_size];
            this.input_buffer_position_ = 0;
            this.input_buffer_length_ = 0;
            
            this.output_buffer_ = new char[buffer_size];
            this.output_buffer_position_ = 0;
            this.output_buffer_length_ = 0;
            
            this.saw_eof_ = false;
            this.stream_owner_ = true;
            this.encoding_detected_ = detect_encoding_from_byteorder_marks;

            this.decoder_ = encoding.GetDecoder();
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual Stream BaseStream {
            get {
                this.stream_owner_ = false;

                return this.base_stream_;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual Encoding CurrentEncoding {
            get { return this.encoding_; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int Read() {
            if ( this.output_buffer_position_ < this.output_buffer_length_ ) {

                return (int)this.output_buffer_[this.output_buffer_position_++];
            } else {
                this.ReadChars();

                return ( this.output_buffer_position_ < this.output_buffer_length_ ) ?
                    (int)this.output_buffer_[this.output_buffer_position_++] :
                    -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(char[] buffer, int index, int count) {
            if ( buffer == null )
                throw new ArgumentNullException( "buffer" );
            if ( index < 0 )
                throw new ArgumentOutOfRangeException( "index" );
            if ( count < 0 )
                throw new ArgumentOutOfRangeException( "count" );
            if ( ( buffer.Length - index ) < count )
                throw new ArgumentException( "count が長いです。" );

            int len = 0;
            if ( count > 0 ) {
                if ( this.output_buffer_position_ >= this.output_buffer_length_ ) {
                    this.ReadChars();

                    if ( this.output_buffer_position_ >= this.output_buffer_length_ )
                        return 0;
                }

                len = this.output_buffer_length_ - this.output_buffer_position_;
                if ( len > count )
                    len = count;

                Array.Copy( this.output_buffer_,
                            this.output_buffer_position_,
                            buffer,
                            index,
                            len );
                this.output_buffer_position_ += len;
                index += len;
            }
            return len;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ReadLine() {
            int ch;
            StringBuilder builder = new StringBuilder();

            for ( ; ; ) {
                if ( this.output_buffer_position_ >= this.output_buffer_length_ ) {
                    this.ReadChars();

                    if ( this.output_buffer_position_ >= this.output_buffer_length_ )
                        break;
                }

                while ( this.output_buffer_position_ >= this.output_buffer_length_ ) {
                    ch = this.output_buffer_[this.output_buffer_position_++];

                    if ( ch == 13 ) {
                        if ( this.output_buffer_position_ >= this.output_buffer_length_ )
                            this.ReadChars();
                        if ( this.output_buffer_position_ >= this.output_buffer_length_ &&
                             this.output_buffer_[this.output_buffer_position_] == '\u000A' )
                            ++this.output_buffer_position_;

                        return builder.ToString();
                    } else if ( ch == 10 )
                        return builder.ToString();
                    else
                        builder.Append( (char)ch );
                }
            }
            return builder.Length != 0 ? builder.ToString() : null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ReadToEnd() {
            StringBuilder builder = new StringBuilder();

            for ( ; ; ) {
                if ( this.output_buffer_position_ >= this.output_buffer_length_ ) {
                    this.ReadChars();

                    if ( this.output_buffer_position_ >= this.output_buffer_length_ )
                        break;
                }

                builder.Append( this.output_buffer_,
                                this.output_buffer_position_,
                                this.output_buffer_length_ - this.output_buffer_position_ );
                this.output_buffer_position_ = this.output_buffer_length_;
            }

            return builder.Length != 0 ? builder.ToString() : string.Empty;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int Peek() {
            if ( this.output_buffer_position_ < this.output_buffer_length_ ) {

                return (int)this.output_buffer_[this.output_buffer_position_];
            } else {
                this.ReadChars();

                return ( this.output_buffer_position_ < this.output_buffer_length_ ) ?
                    (int)this.output_buffer_[this.output_buffer_position_] :
                    -1;
            }
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
        public void DiscarfBufferedData() {
            this.input_buffer_position_ = 0;
            this.input_buffer_length_ = 0;

            this.output_buffer_position_ = 0;
            this.output_buffer_length_ = 0;

            this.saw_eof_ = false;

            this.decoder_ = this.encoding_.GetDecoder();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing) {
            if ( this.stream_owner_ ) {
                if ( this.base_stream_ == null ) {
                    this.base_stream_.Close();
                    this.base_stream_ = null;
                }
            }
            this.input_buffer_ = null;
            this.input_buffer_position_ = 0;
            this.input_buffer_length_ = 0;

            this.output_buffer_ = null;
            this.output_buffer_position_ = 0;
            this.output_buffer_length_ = 0;

            this.saw_eof_ = true;
        }


        /// <summary>
        /// 
        /// </summary>
        private void DetectByteOrder() {
            if ( this.input_buffer_length_ < 1 )
                return;

            if ( this.input_buffer_[0] != 0xFF &&
                 this.input_buffer_[0] != 0xFE &&
                 this.input_buffer_[0] != 0xEF ) {
                this.encoding_detected_ = true;
            } else if ( this.input_buffer_length_ >= 2 &&
                        this.input_buffer_[1] != 0xFE &&
                        this.input_buffer_[1] != 0xFF &&
                        this.input_buffer_[1] != 0xBB ) {
                this.encoding_detected_ = true;
            } else if ( this.input_buffer_length_ >= 3 &&
                        this.input_buffer_[2] != 0xBF ) {
                this.encoding_detected_ = true;
            }

            if ( this.input_buffer_length_ < 2 )
                return;
            if ( this.input_buffer_[0] == 0xFF &&
                 this.input_buffer_[1] == 0xFE ) {
                this.encoding_ = Encoding.Unicode;
                this.decoder_ = this.encoding_.GetDecoder();
                this.input_buffer_position_ = 2;
                this.encoding_detected_ = true;
            } else if ( this.input_buffer_[0] == 0xFE &&
                        this.input_buffer_[1] == 0xFF ) {
                this.encoding_ = Encoding.BigEndianUnicode;
                this.decoder_ = this.encoding_.GetDecoder();
                this.input_buffer_position_ = 2;
                this.encoding_detected_ = true;
            } else if ( this.input_buffer_length_ >= 3 &&
                        this.input_buffer_[0] == 0xFE &&
                        this.input_buffer_[1] == 0xFF ) {
                this.encoding_ = Encoding.UTF8;
                this.decoder_ = this.encoding_.GetDecoder();
                this.input_buffer_position_ = 3;
                this.encoding_detected_ = true;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void ReadChars() {
            int len, output_len;

            while ( this.output_buffer_position_ >= this.output_buffer_length_ && !this.saw_eof_ ) {
                while ( true ) {
                    if ( ( this.input_buffer_length_ - this.input_buffer_position_ ) < this.buffer_size_ ) {
                        if ( this.input_buffer_position_ < this.input_buffer_length_ ) {
                            Array.Copy( this.input_buffer_,
                                        this.input_buffer_position_,
                                        this.input_buffer_,
                                        0,
                                        this.input_buffer_length_ - this.input_buffer_position_ );

                            this.input_buffer_length_ -= this.input_buffer_position_;
                        } else
                            this.input_buffer_length_ = 0;

                        this.input_buffer_position_ = 0;

                        if ( this.base_stream_ == null )
                            throw new IOException( "ベースストリームが既に閉じています。" );

                        len = this.base_stream_.Read( this.input_buffer_,
                                                      this.input_buffer_length_,
                                                      this.buffer_size_ - this.input_buffer_length_ );
                        if ( len <= 0 ) {
                            if ( this.encoding_detected_ ) {
                                this.input_buffer_length_ = 0;
                                this.input_buffer_position_ = 0;
                            }
                            this.saw_eof_ = true;

                            break;
                        } else
                            this.input_buffer_length_ += len;

                        if ( this.input_buffer_length_ > 0 ) {
                            if ( !this.encoding_detected_ )
                                this.DetectByteOrder();
                            if ( this.encoding_detected_ && ( ( this.input_buffer_length_ - this.input_buffer_position_ ) > 0 ) ) {

                                break;
                            }
                        }
                    }

                    len = this.encoding_.GetMaxByteCount( this.buffer_size_ );
                    if ( len > ( this.input_buffer_length_ - this.input_buffer_position_ ) )
                        len = this.input_buffer_length_ - this.input_buffer_position_;
                }

                output_len = this.decoder_.GetChars( this.input_buffer_,
                                                     this.input_buffer_position_,
                                                     len,
                                                     this.output_buffer_,
                                                     0 );
                this.output_buffer_position_ = 0;
                this.output_buffer_length_ = output_len;

                this.input_buffer_position_ += len;

            }
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
        private Decoder decoder_;
        /// <summary>
        /// 
        /// </summary>
        private int buffer_size_;
        /// <summary>
        /// 
        /// </summary>
        private byte[] input_buffer_;
        /// <summary>
        /// 
        /// </summary>
        private int input_buffer_position_;
        /// <summary>
        /// 
        /// </summary>
        private int input_buffer_length_;
        /// <summary>
        /// 
        /// </summary>
        private char[] output_buffer_;
        /// <summary>
        /// 
        /// </summary>
        private int output_buffer_position_;
        /// <summary>
        /// 
        /// </summary>
        private int output_buffer_length_;
        /// <summary>
        /// 
        /// </summary>
        private bool saw_eof_;
        /// <summary>
        /// 
        /// </summary>
        private bool stream_owner_;
        /// <summary>
        /// 
        /// </summary>
        private bool encoding_detected_;


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
    }


}
