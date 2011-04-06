/* -*- encoding: utf-8; -*- */
using System;


namespace Std.Io {


    /// <summary>
    /// バイト入力ストリームを表現するクラスのスーパークラスです。
    /// </summary>
    public abstract class InputStream : Closeable {
        /// <summary>
        /// 
        /// </summary>
        public InputStream() {
        }


        /// <summary>
        /// この入力ストリームのメソッドの次の呼び出しによって、ブロックせずに
        /// この入力ストリームから読み込むことのできる(またはスキップできる)推定バイト数を取得します。
        /// </summary>
        public virtual int Available {
            get { return 0; }
        }


        /// <summary>
        /// この入力ストリームが Mark と Reset メソッドをサポートしているかどうかを表す真偽値を取得します。
        /// </summary>
        public virtual bool MarkSupported {
            get { return false; }
        }


        /// <summary>
        /// この入力ストリームを閉じて、そのストリームに関連する全てのシステムリソースを開放します。
        /// </summary>
        public virtual void Close() {
        }


        /// <summary>
        /// この入力ストリームの現在位置にマークを設定します。
        /// </summary>
        /// <param name="read_limit"></param>
        public virtual void Mark(int read_limit) {
        }


        /// <summary>
        /// 入力ストリームからデータの次のバイトを読み込みます。
        /// </summary>
        /// <exception cref="IOException"></exception>
        /// <returns></returns>
        public abstract int Read();
        /// <summary>
        /// 入力ストリームから数バイトを読み込み、それをバイト列 buffer に格納します。
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public virtual int Read(byte[] buffer) {
            return this.Read( buffer, 0, buffer.Length );
        }
        /// <summary>
        /// 最大 len バイトまでのデータを入力ストリームからバイト配列に読み込みます。
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public virtual int Read(byte[] buffer, int offset, int len) {
            if ( buffer == null )
                throw new ArgumentNullException( "buffer" );
            if ( offset < 0 )
                throw new ArgumentOutOfRangeException( "offset" );
            if ( len < 0 )
                throw new ArgumentOutOfRangeException( "len" );
            if ( buffer.Length < ( len - offset ) )
                throw new ArgumentException( "指定されたバイト列の長さより長いバイト列を読み込もうとしました。" );
            
            int stop = len - offset;
            int result = 0;

            for ( int i = offset; i < stop; ++i ) {
                int tmp = this.Read();
                if ( tmp == -1 )
                    break;
                
                buffer[i] = (byte)( 0xff & tmp );
                ++result;
            }
            return result;
        }


        /// <summary>
        /// このストリームの位置を、入力ストリームで最後に Mark メソッドが呼び出されたときのマーク位置に再設定します。
        /// </summary>
        public virtual void Reset() {
        }


        /// <summary>
        /// この入力ストリームから n バイト分をスキップ、および破棄します。
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public virtual long Skip(long n) {
            return -1;
        }

    }


}
