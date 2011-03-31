/* -*- encoding: utf-8 -*- */
using System;
using System.IO;
using System.Text;


namespace Ixion.Logging {


    using Ixion.Logging.Helper;


    /// <summary>
    ///
    /// </summary>
    public class FileAppender : TextWriterAppender {
        /// <summary>
        /// 
        /// </summary>
        public FileAppender() {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="filename"></param>
        public FileAppender(Layout layout, string filename)
            : base( layout ) {
            this.SetFile( filename );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="filename"></param>
        /// <param name="append"></param>
        public FileAppender(Layout layout, string filename, bool append)
            : base( layout ) {
            this.SetFile( filename, append );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="writer"></param>
        public FileAppender(Layout layout, TextWriter writer)
            : base( layout, writer ) {
        }


        /// <summary>
        /// ファイルにログを追加し続けるかどうかの真偽値を取得します。
        /// </summary>
        public bool CanAppend {
            get { return this.append_; }
            set { this.append_ = value; }
        }


        /// <summary>
        /// ログを書き込むファイルのパスを返します。
        /// </summary>
        public string FileName {
            get { return this.file_.FullName; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void SetFile(string filename) {
            this.SetFile( filename, false );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="append"></param>
        public void SetFile(string filename, bool append) {
            this.file_ = new FileInfo( filename );
            Stream stream = this.file_.Open( append ? FileMode.Append : FileMode.OpenOrCreate,
                                              FileAccess.Write,
                                              FileShare.Read );

            this.SetQuietWriterForFiles( new StreamWriter( stream, base.Encoding ) );
        }


        /// <summary>
        /// 先に開いていたファイルを閉じます。
        /// </summary>
        protected void CloseFile() {
            base.CloseWriter();
        }


        /// <summary>
        /// 先に開いていたファイルを閉じ、スーパークラスの Reset メソッドを呼び出します。
        /// </summary>
        protected override void Reset() {
            this.CloseFile();

            base.Reset();
        }


        /// <summary>
        /// 利用される quiet ライタを設定します。
        /// </summary>
        /// <param name="writer"></param>
        protected void SetQuietWriterForFiles(TextWriter writer) {
            base.SetWriter( writer );
        }


        /// <summary>
        /// 
        /// </summary>
        private bool append_ = false;
        /// <summary>
        /// 
        /// </summary>
        private FileInfo file_ = null;
    }


}
