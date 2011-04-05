using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Threading;


namespace Ixion.Utils.Formats.Csv {


    /// <summary>
    /// �f�[�^���[�_�[��f�[�^�e�[�u���̓��e�� CSV �ɏ����o�����@��񋟂��܂��B
    /// </summary>
    public class CsvWriter : IDisposable {
        #region public-constructors
        /// <summary>
        /// �����o�� CSV �t�@�C���̃p�X���w�肵�� CsvWriter �I�u�W�F�N�g���쐬���܂��B
        /// </summary>
        /// <param name="path">�����o�� CSV �t�@�C���̃p�X�B</param>
        public CsvWriter(string path)
            : this( path, Encoding.Default ) {
        }
        /// <summary>
        /// �����o�� CSV �t�@�C���̃p�X�ƃG���R�[�f�B���O���w�肵�� CsvWriter �I�u�W�F�N�g���쐬���܂��B
        /// </summary>
        /// <param name="path">�����o�� CSV �t�@�C���̃p�X�B</param>
        /// <param name="fileEncoding">�������ރG���R�[�f�B���O�B</param>
        public CsvWriter(string path, Encoding fileEncoding)
            : this( CreateStreamWriter( CreateFileStream( path ), Encoding.Default ) ) {
        }
        /// <summary>
        /// �X�g���[�����w�肵�� CsvWriter �I�u�W�F�N�g���쐬���܂��B
        /// </summary>
        /// <param name="output_stream">�������ރf�o�C�X�ւ̃X�g���[���B</param>
        public CsvWriter(Stream output_stream)
            : this( CreateStreamWriter( output_stream ) ) {
        }
        /// <summary>
        /// �X�g���[���ƃG���R�[�f�B���O���w�肵�� CsvWriter �I�u�W�F�N�g���쐬���܂��B
        /// </summary>
        /// <param name="output_stream">�������ރf�o�C�X�ւ̃X�g���[���B</param>
        /// <param name="stream_encoding">�X�g���[���̃G���R�[�f�B���O�B</param>
        public CsvWriter(Stream output_stream, Encoding stream_encoding)
            : this( CreateStreamWriter( output_stream, stream_encoding ) ) {
        }
        /// <summary>
        /// �w�肳�ꂽ�e�L�X�g���C�^�[���g���� CsvWriter �I�u�W�F�N�g���\�z���܂��B
        /// </summary>
        /// <param name="writer">�������݂Ɏg���e�L�X�g���C�^�[�B</param>
        public CsvWriter(TextWriter writer) {
            this.writer_ = writer;
        }
        #endregion public-constructors


        /// <summary>
        ///   CsvWriter �I�u�W�F�N�g���K�x�[�W�R���N�V�����ɂ����W�����O�ɁA���� CsvWriter �I�u�W�F�N�g�����\�[�X���J�����A
        /// ���̑��̃N���[���A�b�v��������s�o����悤�ɂ��܂��B
        /// </summary>
        ~CsvWriter() {
            if ( this.IsClosed )
                this.Dispose( false );
        }


        #region public-properties
        /// <summary>
        /// �����e�L�X�g���C�^�[�������Ă�����A�^��Ԃ��܂��B
        /// </summary>
        public bool IsClosed {
            get { return this.is_closed_; }
        }


        /// <summary>
        /// ���s����Ƃ��Ɏ����I�Ƀt���b�V�����邩�ǂ����̃t���O���擾�A�܂��͐ݒ肵�܂��B
        /// </summary>
        public bool ImmediateFlush {
            get { return this.immediate_flush_; }
            set { this.immediate_flush_ = value; }
        }


        /// <summary>
        /// CSV �t�@�C���Ƀf�[�^���������ނ̂Ɏg�p����G���R�[�f�B���O���擾���܂��B
        /// </summary>
        public Encoding Encoding {
            get {
                ShouldBeDisposed();

                return this.writer_.Encoding;
            }
        }


        /// <summary>
        ///  ���݂̏����������Ƃ��Ă����ʒu���擾���܂��B
        /// </summary>
        public int ColumnIndex {
            get { return this.column_index_; }
        }


        /// <summary>
        ///   ���ݏ����������Ƃ��Ă���s�ʒu���擾���܂��B
        /// </summary>
        public int RowIndex {
            get { return this.row_index_; }
        }


        /// <summary>
        /// ��؂蕶�����擾�A�܂��͐ݒ肵�܂��B
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
        #region IDisposable �����o
        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
            this.Dispose( true );
            GC.SuppressFinalize( this );
        }
        #endregion IDisposable �����o


        /// <summary>
        /// �w�肳�ꂽ�l���t�B�[���h�Ƃ��ď������݂܂��B
        /// </summary>
        /// <param name="value">�������ޒl�B</param>
        public void Write(object value) {
            this.ShouldBeDisposed();

            this.InnerWrite( this.Filter.Format( value ) );
        }
        /// <summary>
        /// �w�肳�ꂽ�l���t�B�[���h�Ƃ��ď������݂܂��B
        /// </summary>
        /// <param name="value">�������ޒl�B</param>
        public void Write(string value) {
            this.ShouldBeDisposed();

            this.InnerWrite( this.Filter.Format( value ) );
        }
        /// <summary>
        /// �w�肳�ꂽ�I�u�W�F�N�g�z��� 1 �̍s�Ƃ��ď������݂܂��B
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

            // ��̐��B
            int field_count = reader.FieldCount;

            if ( writing_header ) {
                // 
                // �w�b�_�[���o�͂��܂��B
                // 
                for ( int i = 0; i < field_count; ++i ) {
                    // �J�������B
                    string column_name = reader.GetName( i );

                    this.InnerWrite( this.Filter.Format( column_name ) );
                }
                this.WriteLine();
            }

            //
            // �f�[�^���������݂܂��B
            //
            if ( reader.HasRows ) {
                while ( reader.Read() ) {
                    // 
                    // �s�B
                    // 
                    for ( int i = 0; i < field_count; ++i ) {
                        // 
                        // ��B
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
        /// ���݂̍s�ւ̏������݂��I�����A���̍s�ֈڂ�܂��B
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
        /// ���� CsvWriter ���I�����A���V�[�o�[�Ɋ֘A�t����ꂽ�S�ẴV�X�e�����\�[�X���J�����܂��B
        /// </summary>
        public void Close() {
            this.Dispose( true );
        }


        /// <summary>
        /// ���݂� CsvWriter �̃o�b�t�@���N���A���A�o�b�t�@���̃f�[�^�����ɂȂ�f�o�C�X�ɏ������݂܂��B
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
        /// �t�H�[�}�b�g���ꂽ�t�B�[���h���������݂܂��B
        /// </summary>
        /// <param name="field"></param>
        protected void InnerWrite(string field) {
            this.ShouldBeDisposed();

            StringBuilder buffer = new StringBuilder();
            // field �� null �Ȃ�󕶎��ɂ��܂��B
            if ( field == null )
                field = string.Empty;
            // 0 �ȏ�̗�ʒu�̂Ƃ��͐�ɃZ�p���[�^�[(��؂蕶��)��t�������܂��B
            if ( this.ColumnIndex > 0 )
                buffer.Append( this.Separator );

            buffer.Append( field );

            this.writer_.Write( buffer.ToString() );
            Interlocked.Increment( ref this.column_index_ );
        }


        /// <summary>
        /// CsvWriter �Ŏg�p����Ă���A���}�l�[�W���\�[�X���J�����A�I�v�V�����Ń}�l�[�W���\�[�X���J�����܂��B
        /// </summary>
        /// <param name="disposing">�}�l�[�W���\�[�X�ƃA���}�l�[�W���\�[�X�̗������������ꍇ�͐^�B</param>
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
        /// �w�肳�ꂽ�p�X����t�@�C���X�g���[�����쐬���ĕԂ��܂��B
        /// </summary>
        /// <param name="path">�J�������t�@�C���̃p�X�B</param>
        /// <returns>�������݃t�@�C���X�g���[���B</returns>
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
        /// �����e�L�X�g���C�^�[�ł��B
        /// </summary>
        private TextWriter writer_;
        /// <summary>
        /// ���s����Ƃ��Ɏ����I�Ƀt���b�V�����邩�ǂ�����\���܂��B
        /// </summary>
        private bool immediate_flush_ = false;
        /// <summary>
        /// �����e�L�X�g���C�^�[������ꂽ���ǂ�����\���t���O�ł��B
        /// </summary>
        private bool is_closed_ = false;
        /// <summary>
        /// ���ݏ����������Ƃ��Ă����̈ʒu��\���܂��B
        /// </summary>
        private int column_index_ = 0;
        /// <summary>
        /// ���ݏ����������Ƃ��Ă���s�̈ʒu��\���܂��B
        /// </summary>
        private int row_index_ = 0;
        /// <summary>
        /// �t�B�[���h�ƃt�B�[���h�̋�؂��\�������ł��B
        /// </summary>
        private string separator_ = ",";
        /// <summary>
        /// 
        /// </summary>
        private IFieldFilter filter_ = new NilFieldFilter();
        #endregion private-fields
    }


}
