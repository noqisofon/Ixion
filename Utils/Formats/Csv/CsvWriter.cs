using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;


namespace Vanila.Utils.Formats.Csv {


    /// <summary>
    ///データを CSV ファイルに書き出します。
    /// </summary>
    public class CsvWriter : IDisposable {
        /// <summary>
        /// 書きだすファイルパスを指定して CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="path">書きだすファイルのパス。</param>
        public CsvWriter(string path)
            : this( path, Encoding.Default ) {
        }
        /// <summary>
        /// 書きだすファイルパスとフィルターを指定して CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="path">書きだすファイルのパス。</param>
        /// <param name="filter"></param>
        public CsvWriter(string path, IFieldFilter filter)
            : this( path, Encoding.Default, filter ) {
        }
        /// <summary>
        /// 書きだすファイルパスとエンコーディングを指定して CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="path">書きだすファイルのパス。</param>
        /// <param name="fileEncoding">書き込み用のエンコーディング。</param>
        public CsvWriter(string path, Encoding fileEncoding)
            : this( path, fileEncoding, new SimpleFieldFilter() ) {
        }
        /// <summary>
        /// 書きだすファイルパスとエンコーディングとフィルターを指定して CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="path">書きだすファイルのパス。</param>
        /// <param name="fileEncoding">書き込み用のエンコーディング。</param>
        /// <param name="filter"></param>
        public CsvWriter(string path, Encoding fileEncoding, IFieldFilter filter) {
            FileInfo file = new FileInfo( path );

            if ( file.Exists )
                this.writer_ = new StreamWriter( file.Open( FileMode.Truncate, FileAccess.Write ), fileEncoding );
            else
                this.writer_ = new StreamWriter( file.Open( FileMode.CreateNew, FileAccess.Write ), fileEncoding );

            this.filter_ = filter;
        }
        /// <summary>
        /// ストリームを指定して CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="stream">書き込むターゲットへのストリーム。</param>
        public CsvWriter(Stream stream)
            : this( new StreamWriter( stream ) ) {
        }
        /// <summary>
        /// ストリームとエンコーディングを指定して、CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="stream">書き込むターゲットへのストリーム。</param>
        /// <param name="streamEncoding">ストリームのエンコーディング。</param>
        public CsvWriter(Stream stream, Encoding streamEncoding)
            : this( new StreamWriter( stream, streamEncoding ) ) {
        }
        /// <summary>
        /// ストリームとエンコーディング、フィルターを指定して、CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="stream">書き込むターゲットへのストリーム。</param>
        /// <param name="streamEncoding">ストリームのエンコーディング。</param>
        /// <param name="filter">フィールド用のフィルター。</param>
        public CsvWriter(Stream stream, Encoding streamEncoding, IFieldFilter filter)
            : this( new StreamWriter( stream, streamEncoding ), filter ) {
        }
        /// <summary>
        /// 任意のテキストライターを指定して、CsvWriter オブジェクトを作成します。
        /// </summary>
        /// <param name="writer">CSV 書き込みに使用するテキストライター。</param>
        public CsvWriter(TextWriter writer)
            : this( writer, new SimpleFieldFilter() ) {
        }
        /// <summary>
        /// 指定されたテキストライターとフィールドフィルターを
        /// 使用して CsvWriter オブジェクトを構築します。
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="filter"></param>
        public CsvWriter(TextWriter writer, IFieldFilter filter) {
            this.writer_ = writer;
            this.filter_ = filter;
        }


        /// <summary>
        /// 
        /// </summary>
        ~CsvWriter() {
            this.Dispose( false );
        }


        /// <summary>
        /// 内部テキストライターが閉じられていたら真を返します。
        /// </summary>
        public bool IsClosed {
            get {
                return this.is_closed_;
            }
        }


        /// <summary>
        /// CSV ファイルにデータを書き込むのに利用するエンコーディングを取得します。
        /// </summary>
        public Encoding Encoding {
            get {
                ShouldBeDisposed();

                return this.writer_.Encoding;
            }
        }


        /// <summary>
        /// 現在の列インデックスを取得します。
        /// </summary>
        /// <field></field>
        public int ColumnIndex {
            get {
                return this.column_index_;
            }
        }


        /// <summary>
        /// 現在の行インデックスを取得します。
        /// </summary>
        public int RowIndex {
            get {
                return this.row_index_;
            }
        }


        /// <summary>
        /// 指定されたオブジェクトをフィールドとして書き込みます。
        /// </summary>
        /// <param name="field">フィールドとして書き込むオブジェクト。</param>
        public void Write(object field) {
            this.Write( field, field.GetType() );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="field_type"></param>
        public void Write(object field, Type field_type) {
            ShouldBeDisposed();

            StringBuilder buffer = new StringBuilder();
            if ( field == null ) {
                field = string.Empty;
            }

            if ( this.column_index_ > 0 ) {
                buffer.Append( "," );
            }
            buffer.Append( this.filter_.Format( field, field_type ) );
            this.writer_.Write( buffer.ToString() );

            ++this.column_index_;
        }
        /// <summary>
        /// 指定された文字列をフィールドとして書き込みます。
        /// </summary>
        /// <param name="field">書き込む文字列。</param>
        public void Write(string field) {
            ShouldBeDisposed();

            StringBuilder buffer = new StringBuilder();
            if ( field == null ) {
                field = string.Empty;
            }

            if ( this.column_index_ > 0 ) {
                buffer.Append( "," );
            }
            buffer.Append( this.filter_.Format( field ) );
            this.writer_.Write( buffer.ToString() );

            ++this.column_index_;
        }
        /// <summary>
        /// 指定された文字列の配列を 1 つの行として書き込みます。
        /// </summary>
        /// <param name="rows">行として書き込みたい文字列配列。</param>
        public void Write(string[] rows) {
            if ( rows == null ) {
                throw new ArgumentNullException( "rows" );
            }
            ShouldBeDisposed();

            foreach ( string row in rows ) {
                this.Write( row, typeof( string ) );
            }
            this.WriteLine();
        }
        /// <summary>
        /// データリーダーを受け取り、その内容を CSV ファイルに書き出します。
        /// </summary>
        /// <param name="reader"></param>
        public void Write(DbDataReader reader) {
            if ( reader == null )
                throw new ArgumentNullException( "reader" );
            ShouldBeDisposed();

            DataTable schema = reader.GetSchemaTable();
            foreach ( DataRow row in schema.Rows ) {
                this.Write( row["ColumnName"], typeof( string ) );
            }
            this.WriteLine();

            if ( reader.HasRows ) {
                while ( reader.Read() ) {

                    for ( int i = 0; i < reader.FieldCount; ++i ) {
                        Type datatype = schema.Rows[i]["DataType"] as Type;

                        this.Write( reader[i], datatype );
                    }
                    this.WriteLine();
                }
            }
            this.Flush();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="header"></param>
        public void Write(DbDataReader reader, string[] header) {
            if ( reader == null )
                throw new ArgumentNullException( "reader" );
            ShouldBeDisposed();
            if ( header == null )
                Write( reader );

            DataTable schema = reader.GetSchemaTable();
            /*
             * ヘッダーを書き出します。
             */
            foreach ( string field in header ) {
                this.Write( field );
            }
            this.WriteLine();

            if ( reader.HasRows ) {
                while ( reader.Read() ) {

                    for ( int i = 0; i < reader.FieldCount; ++i ) {
                        Type datatype = schema.Rows[i]["DataType"] as Type;

                        this.Write( reader[i], datatype );
                    }
                    this.WriteLine();
                }
            }
            this.Flush();
        }
        /// <summary>
        /// 指定されたデータテーブルの内容を CSV ファイルに書き出します。
        /// </summary>
        /// <param name="table"></param>
        public void Write(DataTable table) {
            if ( table == null ) {
                throw new ArgumentNullException( "table" );
            }
            ShouldBeDisposed();
            /*
             * ヘッダーを書き込みます。
             */
            foreach ( DataColumn column in table.Columns ) {
                this.Write( column.Caption );
            }
            this.WriteLine();

            /*
             * データを書き込みます。
             */
            foreach ( DataRow row in table.Rows ) {
                foreach ( DataColumn column in table.Columns ) {
                    this.Write( row[column], column.DataType );
                }
                this.WriteLine();
            }
            this.WriteLine();
            this.Flush();
        }


        /// <summary>
        /// 現在の行への書き込みを終了し、次の行へ移ります。
        /// </summary>
        public void WriteLine() {
            ShouldBeDisposed();

            this.column_index_ = 0;
            this.row_index_ = 0;
            this.writer_.WriteLine();
        }


        /// <summary>
        /// この CsvWriter を終了し、レシーバーに関連付けられた全てのシステムリソースを開放します。
        /// </summary>
        public void Close() {
            this.Dispose( true );
        }


        /// <summary>
        /// 現在のライターのバッファをクリアし、バッファ内のデータを元になるデバイスに書き込みます。
        /// </summary>
        public void Flush() {
            ShouldBeDisposed();

            this.writer_.Flush();
        }


        /// <summary>
        /// 既に Close() が呼び出されている時にシステムリソースを使用した処理を呼び出した場合に
        /// ObjectDisposedException を投げます。
        /// </summary>
        /// <remarks>定型処理で、しかも何度も書く処理なので、メソッドにしています。</remarks>
        protected void ShouldBeDisposed() {
            if ( this.IsClosed ) {
                throw new ObjectDisposedException( "writer_" );
            }
        }


        /// <summary>
        /// CsvWriter で使用されているアンマネージリソースを開放し、オプションでマネージリソースも開放します。
        /// </summary>
        /// <param name="disposing">マネージリソースとアンマネージリソースの両方を解放する場合は真。</param>
        private void Dispose(bool disposing) {
            ShouldBeDisposed();

            if ( disposing ) {
                // disposing が真のときだけ Close() を呼び出します。
                this.writer_.Close();
            }
            this.writer_ = null;

            this.is_closed_ = true;
        }


        #region IDisposable メンバ
        /// <summary>
        /// アンマネージ リソースの解放およびリセットに関連付けられているアプリケーション定義のタスクを実行します。
        /// </summary>
        void IDisposable.Dispose() {
            this.Dispose( true );
            GC.SuppressFinalize( this );
        }
        #endregion


        /// <summary>
        /// 内部テキストライターです。
        /// </summary>
        private TextWriter writer_;
        /// <summary>
        /// 内部テキストライターが閉じられたかどうかを表すフラグです。
        /// </summary>
        private bool is_closed_ = false;
        /// <summary>
        /// 現在の列インデックスを表します。
        /// </summary>
        private int column_index_ = 0;
        /// <summary>
        /// 現在の行インデックスを表します。
        /// </summary>
        private int row_index_ = 0;
        /// <summary>
        /// 書き込むフィールド値をフィルタリングします。
        /// </summary>
        private IFieldFilter filter_ = null;
    }


}
