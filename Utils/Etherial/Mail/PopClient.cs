/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace Ixion.Etherial.Mail {


    /// <summary>
    /// 
    /// </summary>
    public class PopClient : IDisposable {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        public PopClient(string hostname, int port) {
            this.client_ = new TcpClient( hostname, port );
            this.reader_ = new StreamReader( this.client_.GetStream(), Encoding.ASCII );

            string line = this.ReadLine();
            if ( !line.ToUpper().StartsWith( "+OK" ) )
                throw new PopClientException( string.Format( "接続時に POP サーバーが \"{0}\" を返しました。", line ) );
        }


        /// <summary>
        /// 
        /// </summary>
        ~PopClient() {
            this.Dispose( false );
        }


        /// <summary>
        /// リソースが解放されたかどうかの有無を返します。
        /// </summary>
        /// <value></value>
        public bool Disposed {
            get { return this.disposed_; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user_name"></param>
        /// <param name="password"></param>
        public void Login(string user_name, string password) {
            this.SendLine( string.Format( "USER {0}", user_name ) );
            string line = this.ReadLine();

            if ( !line.ToUpper().StartsWith( "+OK" ) ) 
                throw new PopClientException( string.Format( "USER 送信時に POP サーバーが \"{0}\" を返しました。", line ) );

            this.SendLine( string.Format( "PASS {0}", password ) );
            line = this.ReadLine();

            if ( !line.ToUpper().StartsWith( "+OK" ) )
                throw new PopClientException( string.Format( "PASS 送信時に POP サーバーが \"{0}\" を返しました。", line ) );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetList() {
            this.SendLine( "LIST" );

            string line = this.ReadLine();
            if ( !line.ToUpper().StartsWith( "+OK" ) )
                throw new PopClientException( string.Format( "LIST 送信時に POP サーバーが \"{0}\" を返しました。", line ) );


            List<string> mails = new List<string>();
            while ( true ) {
                line = this.ReadLine();
                if ( line == "." ) {
                    // 終端に到達しました。
                    break;
                }
                int mail_number_position = line.IndexOf( ' ' );
                if ( mail_number_position > 0 ) {
                    line = line.Substring( 0, mail_number_position );
                }
                mails.Add( line );
            }

            return mails.ToArray();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetMail(string number) {
            this.SendLine( string.Format( "RETR {0}", number ) );
            string line = this.ReadLine();

            if ( !line.ToUpper().StartsWith( "+OK" ) )
                throw new PopClientException( string.Format( "RETR{0} 送信時に POP サーバーが \"{1}\" を返しました。", number, line ) );

            StringBuilder mail_builder = new StringBuilder();
            while ( true ) {
                line = this.ReadLine();
                if ( line == "." )
                    break;

                mail_builder.Append( line ).AppendLine();
            }
            return mail_builder.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        public void DeleteMail(string number) {
            this.SendLine( string.Format( "DELE {0}", number ) );
            string line = this.ReadLine();

            if ( !line.ToUpper().StartsWith( "+OK" ) )
                throw new PopClientException( string.Format( "DELE {0} 送信時に POP サーバーが \"{1}\" を返しました。", number, line ) );
        }


        /// <summary>
        /// 
        /// </summary>
        public void Close() {
            this.Dispose();
        }


        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
            this.Dispose( true );
            GC.SuppressFinalize( this );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing) {
            this.ShouldBeDisposed();

            this.InnerQuit();

            if ( disposing ) {
                this.reader_.Close();
                this.client_.Close();
            }
            this.reader_ = null;
            this.client_ = null;

            this.disposed_ = true;
        }


        /// <summary>
        /// 
        /// </summary>
        protected void ShouldBeDisposed() {
            if ( this.Disposed )
                throw new ObjectDisposedException( this.GetType().ToString() );
        }


        /// <summary>
        /// 
        /// </summary>
        private void InnerQuit() {
            this.SendLine( "QUIT" );
            string line = this.ReadLine();

            if ( !line.ToUpper().StartsWith( "+OK" ) )
                throw new PopClientException( string.Format( "QUIT 送信時に POP サーバーが \"{0}\" を返しました。", line ) );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string ReadLine() {
            this.ShouldBeDisposed();

            byte[] temp_bytes = new byte[8096];
            int amount_of_bytes_read = 0;
            TimeSpan timeout = TimeSpan.FromMilliseconds( DEFAULT_MILLISECONDS );

            int output_trial_count = 0;

            ManualResetEvent readed = new ManualResetEvent( false );
            bool signal_captured = false;
            IAsyncResult byte_reading = this.reader_.BaseStream.BeginRead(
                temp_bytes,
                0,
                temp_bytes.Length,
                completed,
                readed );

            while ( !byte_reading.IsCompleted ) {
                if ( output_trial_count >= MAX_TRIALS )
                    throw new PopClientException( "タイムアウトしました。" );

                if ( signal_captured = readed.WaitOne( timeout ) )
                    break;
                if ( byte_reading.AsyncWaitHandle.WaitOne( 0 ) )
                    break;

                ++output_trial_count;
            }
            amount_of_bytes_read = this.reader_.BaseStream.EndRead( byte_reading );

            string response = this.reader_.CurrentEncoding.GetString( temp_bytes, 0, amount_of_bytes_read );
            int newline_index = response.IndexOf( Environment.NewLine );

            return newline_index != -1 ? response.Substring( 0, newline_index ) : response;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        private void SendLine(string line) {
            this.ShouldBeDisposed();

            int input_trial_count = 0;
            byte[] sent_bytes = this.reader_.CurrentEncoding.GetBytes( line );

            TimeSpan timeout = TimeSpan.FromMilliseconds( DEFAULT_MILLISECONDS );
            ManualResetEvent wrote = new ManualResetEvent( false );

            bool signal_captured = false;
            IAsyncResult byte_writing = this.reader_.BaseStream.BeginWrite(
                sent_bytes,
                0,
                sent_bytes.Length,
                completed,
                wrote );
            while ( !byte_writing.IsCompleted ) {
                if ( input_trial_count >= MAX_TRIALS )
                    throw new PopClientException( "タイムアウトしました。" );

                if ( signal_captured = wrote.WaitOne( timeout ) )
                    break;
                if ( byte_writing.AsyncWaitHandle.WaitOne( 0 ) )
                    break;

                ++input_trial_count;
            }

            this.reader_.BaseStream.EndWrite( byte_writing );
        }


        void completed(IAsyncResult ar) {
            ManualResetEvent signal = (ManualResetEvent)ar.AsyncState;

            signal.Set();
        }


        /// <summary>
        /// 
        /// </summary>
        private TcpClient client_;
        /// <summary>
        /// 
        /// </summary>
        private StreamReader reader_;
        /// <summary>
        /// 
        /// </summary>
        private bool disposed_;

        /// <summary>
        /// 
        /// </summary>
        private readonly int MAX_TRIALS = 32;
        /// <summary>
        /// 
        /// </summary>
        private readonly int DEFAULT_MILLISECONDS = 300;
    }


}
