using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Threading;


namespace Ixion.Utils.Formats.Csv {


    /// <summary>
    /// データリーダーやデータテーブルの内容を CSV に書き出す方法を提供します。
    /// </summary>
    public class CsvWriter : IDisposable {
        #region public-constructors
        /// <summary>
        /// 書き出す CSV ファイルのパスを指定して CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="path">書き出す CSV ファイルのパス。</param>
        public CsvWriter(string path)
            : this( path, Encoding.Default ) {
        }
        /// <summary>
        /// 書き出す CSV ファイルのパスとエンコーディングを指定して CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="path">書き出す CSV ファイルのパス。</param>
        /// <param name="fileEncoding">書き込むエンコーディング。</param>
        public CsvWriter(string path, Encoding fileEncoding)
            : this( CreateStreamWriter( CreateFileStream( path ), Encoding.Default ) ) {
        }
        /// <summary>
        /// ストリームを指定して CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="output_stream">書き込むデバイスへのストリーム。</param>
        public CsvWriter(Stream output_stream)
            : this( CreateStreamWriter( output_stream ) ) {
        }
        /// <summary>
        /// ストリームとエンコーディングを指定して CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="output_stream">書き込むデバイスへのストリーム。</param>
        /// <param name="stream_encoding">ストリームのエンコーディング。</param>
        public CsvWriter(Stream output_stream, Encoding stream_encoding)
            : this( CreateStreamWriter( output_stream, stream_encoding ) ) {
        }
        /// <summary>
        /// 指定されたテキストライターを使って CsvWriter オブジェクトを構築します。
        /// </summary>
        /// <param name="writer">書き込みに使うテキストライター。</param>
        public CsvWriter(TextWriter writer) {
            this.writer_ = writer;
        }
        #endregion public-constructors


        /// <summary>
        ///   CsvWriter オブジェクトがガベージコレクションにより収集される前に、その CsvWriter オブジェクトがリソースを開放し、
        /// その他のクリーンアップ操作を実行出来るようにします。
        /// </summary>
        ~CsvWriter() {
            if ( this.IsClosed )
                this.Dispose( false );
        }


        #region public-properties
        /// <summary>
        /// 内部テキストライターが閉じられていたら、真を返します。
        /// </summary>
        public bool IsClosed {
            get { return this.is_closed_; }
        }


        /// <summary>
        /// 改行するときに自動的にフラッシュするかどうかのフラグを取得、または設定します。
        /// </summary>
        public bool ImmediateFlush {
            get { return this.immediate_flush_; }
            set { this.immediate_flush_ = value; }
        }


        /// <summary>
        /// CSV ファイルにデータを書き込むのに使用するエンコーディングを取得します。
        /// </summary>
        public Encoding Encoding {
            get {
                ShouldBeDisposed();

                return this.writer_.Encoding;
            }
        }


        /// <summary>
        ///  現在の書きこもうとしている列位置を取得します。
        /// </summary>
        public int ColumnIndex {
            get { return this.column_index_; }
        }


        /// <summary>
        ///   現在書きこもうとしている行位置を取得します。
        /// </summary>
        public int RowIndex {
            get { return this.row_index_; }
        }


        /// <summary>
        /// 区切り文字を取得、または設定します。
        /// </summary>
        public string Separator {
            get { return this.separator_; }
            set { this.separator_ = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public IFieldFilter Filter {
            get { return this.filter_; }
            set { this.filter_ = value; }
        }
        #endregion public-properties


        #region public-methods
        #region IDisposable メンバ
        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
            this.Dispose( true );
            GC.SuppressFinalize( this );
        }
        #endregion IDisposable メンバ


        /// <summary>
        /// 指定された値をフィールドとして書き込みます。
        /// </summary>
        /// <param name="value">書き込む値。</param>
        public void Write(object value) {
            this.ShouldBeDisposed();

            this.InnerWrite( this.Filter.Format( value ) );
        }
        /// <summary>
        /// 指定された値をフィールドとして書き込みます。
        /// </summary>
        /// <param name="value">書き込む値。</param>
        public void Write(string value) {
            this.ShouldBeDisposed();

            this.InnerWrite( this.Filter.Format( value ) );
        }
        /// <summary>
        /// 指定されたオブジェクト配列を 1 つの行として書き込みます。
        /// </summary>
        /// <param name="row"></param>
        public void Write(object[] row) {
            this.ShouldBeDisposed();

            if ( row == null )
                throw new ArgumentNullException( "row" );

            foreach ( string value in row ) {
                this.InnerWrite( this.Filter.Format( value ) );
            }
            this.WriteLine();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public void Write(DbDataReader reader) {
            this.ShouldBeDisposed();

            if ( reader == null )
                throw new ArgumentNullException( "reader" );

            this.Write( reader, true );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writing_header"></param>
        public void Write(DbDataReader reader, bool writing_header) {
            this.ShouldBeDisposed();

            if ( reader == null )
                throw new ArgumentNullException( "reader" );

            // 列の数。
            int field_count = reader.FieldCount;

            if ( writing_header ) {
                // 
                // ヘッダーを出力します。
                // 
                for ( int i = 0; i < field_count; ++i ) {
                    // カラム名。
                    string column_name = reader.GetName( i );

                    this.InnerWrite( this.Filter.Format( column_name ) );
                }
                this.WriteLine();
            }

            //
            // データを書き込みます。
            //
            if ( reader.HasRows ) {
                while ( reader.Read() ) {
                    // 
                    // 行。
                    // 
                    for ( int i = 0; i < field_count; ++i ) {
                        // 
                        // 列。
                        // 
                        this.InnerWrite( this.Filter.Format( reader.GetName( i ),
                                                             reader.GetFieldType( i ),
                                                             reader[i] ) );
                    }
                    this.WriteLine();
                }
            }
        }


        /// <summary>
        /// 現在の行への書き込みを終了し、次の行へ移ります。
        /// </summary>
        public void WriteLine() {
            this.ShouldBeDisposed();

            this.column_index_ = 0;
            Interlocked.Increment( ref this.row_index_ );

            this.writer_.WriteLine();
            if ( this.ImmediateFlush )
                this.Flush();
        }


        /// <summary>
        /// この CsvWriter を終了し、レシーバーに関連付けられた全てのシステムリソースを開放します。
        /// </summary>
        public void Close() {
            this.Dispose( true );
        }


        /// <summary>
        /// 現在の CsvWriter のバッファをクリアし、バッファ内のデータを元になるデバイスに書き込みます。
        /// </summary>
        public void Flush() {
            this.ShouldBeDisposed();

            this.writer_.Flush();
        }
        #endregion public-methods


        #region protected-methods
        /// <summary>
        /// 
        /// </summary>
        protected void ShouldBeDisposed() {
            if ( this.IsClosed )
                throw new ObjectDisposedException( "writer_" );
        }


        /// <summary>
        /// フォーマットされたフィールドを書き込みます。
        /// </summary>
        /// <param name="field"></param>
        protected void InnerWrite(string field) {
            this.ShouldBeDisposed();

            StringBuilder buffer = new StringBuilder();
            // field が null なら空文字にします。
            if ( field == null )
                field = string.Empty;
            // 0 以上の列位置のときは先にセパレーター(区切り文字)を付け足します。
            if ( this.ColumnIndex > 0 )
                buffer.Append( this.Separator );

            buffer.Append( field );

            this.writer_.Write( buffer.ToString() );
            Interlocked.Increment( ref this.column_index_ );
        }


        /// <summary>
        /// CsvWriter で使用されているアンマネージリソースを開放し、オプションでマネージリソースも開放します。
        /// </summary>
        /// <param name="disposing">マネージリソースとアンマネージリソースの両方を解放する場合は真。</param>
        protected void Dispose(bool disposing) {
            this.ShouldBeDisposed();

            if ( disposing ) {
                this.writer_.Close();
            }
            this.writer_ = null;
            this.is_closed_ = true;
        }
        #endregion protected-methods


        #region protected-static-methods
        /// <summary>
        /// 指定されたパスからファイルストリームを作成して返します。
        /// </summary>
        /// <param name="path">開きたいファイルのパス。</param>
        /// <returns>書き込みファイルストリーム。</returns>
        protected static FileStream CreateFileStream(string path) {
            return File.Open( path, FileMode.Create, FileAccess.Write, FileShare.Read );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="output_stream"></param>
        /// <returns></returns>
        protected static TextWriter CreateStreamWriter(Stream output_stream) {
            return CreateStreamWriter( output_stream, Encoding.Default );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output_stream"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        protected static TextWriter CreateStreamWriter(Stream output_stream, Encoding encoding) {
            return new StreamWriter( output_stream, encoding );
        }
        #endregion protected-static-methods


        #region private-fields
        /// <summary>
        /// 内部テキストライターです。
        /// </summary>
        private TextWriter writer_;
        /// <summary>
        /// 改行するときに自動的にフラッシュするかどうかを表します。
        /// </summary>
        private bool immediate_flush_ = false;
        /// <summary>
        /// 内部テキストライターが閉じられたかどうかを表すフラグです。
        /// </summary>
        private bool is_closed_ = false;
        /// <summary>
        /// 現在書きこもうとしている列の位置を表します。
        /// </summary>
        private int column_index_ = 0;
        /// <summary>
        /// 現在書きこもうとしている行の位置を表します。
        /// </summary>
        private int row_index_ = 0;
        /// <summary>
        /// フィールドとフィールドの区切りを表す文字です。
        /// </summary>
        private string separator_ = ",";
        /// <summary>
        /// 
        /// </summary>
        private IFieldFilter filter_ = new NilFieldFilter();
        #endregion private-fields
    }


}
