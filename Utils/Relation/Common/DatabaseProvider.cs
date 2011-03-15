using System;
using System.Data.Common;


namespace Ixion.Utils.Relation.Common {


    /// <summary>
    ///
    /// </summary>
    public abstract class DatabaseProvider {

        /// <summary>
        /// ��� DbProvider �I�u�W�F�N�g���쐬���܂��B
        /// </summary>
        public DatabaseProvider()
            : this( string.Empty, string.Empty ) {
        }
        /// <summary>
        /// ���[�J���� SQL Server �́A�w�肳�ꂽ�f�[�^�x�[�X�ւ̐ڑ��I�u�W�F�N�g��
        /// �쐬���邽�߂� DbProvider �I�u�W�F�N�g���쐬���܂��B
        /// </summary>
        /// <param name="initial_catalog">�f�[�^�x�[�X���B</param>
        public DatabaseProvider(string initial_catalog)
            : this( initial_catalog, "(local)" ) {
        }
        /// <summary>
        /// �w�肳�ꂽ SQL Server �́A�w�肳�ꂽ�f�[�^�x�[�X�ւ̐ڑ��I�u�W�F�N�g��
        /// �쐬���邽�߂� DbProvider �I�u�W�F�N�g���쐬���܂��B
        /// </summary>
        /// <param name="initial_catalog">�f�[�^�x�[�X���B</param>
        /// <param name="data_source">SQL Server �̖��O�B</param>
        public DatabaseProvider(string initial_catalog, string data_source) {
            this.DataSource = data_source;
            this.InitialCatalog = initial_catalog;
            this.Timeout = 30;
        }


        /// <summary>
        /// �ڑ����� SQL Server �̖��O���擾������A�ݒ肵���肵�܂��B
        /// </summary>
        public string DataSource {
            get { return this.data_source_; }
            set { this.data_source_ = value; }
        }


        /// <summary>
        /// �ڑ�����f�[�^�x�[�X���̖��O���擾������A�ݒ肵���肵�܂��B
        /// </summary>
        public string InitialCatalog {
            get { return this.initial_catalog_; }
            set { this.initial_catalog_ = value; }
        }


        /// <summary>
        /// �f�[�^�x�[�X�֐ڑ�����Ƃ��̃^�C���A�E�g���擾������ݒ肵���肵�܂��B
        /// </summary>
        public int Timeout {
            get { return this.timeout_; }
            set { this.timeout_ = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public DbConnection GetConnection() {
            return GetDbConnection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public DbConnection GetConnection(string user_id, string password) {
            return GetDbConnection( user_id, password );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract DbConnection GetDbConnection();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract DbConnection GetDbConnection(string user_id, string password);


        /// <summary>
        /// �ڑ�����T�[�o�[����\���܂��B
        /// </summary>
        private string data_source_;
        /// <summary>
        /// �ڑ�����f�[�^�x�[�X����\���܂��B
        /// </summary>
        private string initial_catalog_;
        /// <summary>
        /// 
        /// </summary>
        private int timeout_;
    }


}
