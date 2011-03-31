using System;
using System.Data.Common;
using System.Data.SqlClient;


namespace Ixion.Utils.Relation.Sql {


    using Ixion.Utils.Relation.Common;


    /// <summary>
    /// SQL Server �ւ̐ڑ��I�u�W�F�N�g���쐬���邽�߂̃t�@�N�g���N���X�ł��B
    /// </summary>
    public class SqlServerProvider : DatabaseProvider {
        /// <summary>
        /// ��� SqlServerProvider �I�u�W�F�N�g���쐬���܂��B
        /// </summary>
        public SqlServerProvider()
            : base( string.Empty, string.Empty ) {
        }
        /// <summary>
        /// ���[�J���� SQL Server �́A�w�肳�ꂽ�f�[�^�x�[�X�ւ̐ڑ��I�u�W�F�N�g��
        /// �쐬���邽�߂� SqlServerProvider �I�u�W�F�N�g���쐬���܂��B
        /// </summary>
        /// <param name="initial_catalog">�f�[�^�x�[�X���B</param>
        public SqlServerProvider(string initial_catalog)
            : base( initial_catalog, "(local)" ) {
        }
        /// <summary>
        /// �w�肳�ꂽ SQL Server �́A�w�肳�ꂽ�f�[�^�x�[�X�ւ̐ڑ��I�u�W�F�N�g��
        /// �쐬���邽�߂� SqlServerProvider �I�u�W�F�N�g���쐬���܂��B
        /// </summary>
        /// <param name="initial_catalog">�f�[�^�x�[�X���B</param>
        /// <param name="data_source">SQL Server �̖��O�B</param>
        public SqlServerProvider(string initial_catalog, string data_source)
            : base( initial_catalog, data_source ) {
        }


        /// <summary>
        /// SqlServer �ւ̐ڑ��I�u�W�F�N�g���쐬���ĕԂ��܂��B
        /// </summary>
        /// <returns>�V�����ڑ��I�u�W�F�N�g�B</returns>
        new public SqlConnection GetConnection() {
            return (SqlConnection)GetDbConnection();
        }
        /// <summary>
        /// �A�J�E���g���ƃp�X���[�h���w�肵�� SqlServer �ւ̐ڑ��I�u�W�F�N�g���쐬���ĕԂ��܂��B
        /// </summary>
        /// <param name="acount_name">�ڑ����Ɏg�p���郆�[�U�[ ID�B</param>
        /// <param name="password">���[�U�[ ID �̃p�X���[�h�B</param>
        /// <returns>�V�����ڑ��I�u�W�F�N�g�B</returns>
        new public SqlConnection GetConnection(string acount_name, string password) {
            return (SqlConnection)GetDbConnection( acount_name, password );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override DbConnection GetDbConnection() {
            SqlConnectionStringBuilder connection_text_builder = new SqlConnectionStringBuilder();

            connection_text_builder.InitialCatalog = base.InitialCatalog;
            connection_text_builder.DataSource = base.DataSource;
            connection_text_builder.IntegratedSecurity = true;

            connection_text_builder.ConnectTimeout = base.Timeout;

            return new SqlConnection( connection_text_builder.ToString() );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="acount_name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        protected override DbConnection GetDbConnection(string acount_name, string password) {
            SqlConnectionStringBuilder connection_text_builder = new SqlConnectionStringBuilder();

            connection_text_builder.InitialCatalog = base.InitialCatalog;
            connection_text_builder.DataSource = base.DataSource;
            connection_text_builder.IntegratedSecurity = false;

            connection_text_builder.ConnectTimeout = base.Timeout;

            connection_text_builder.UserID = acount_name;
            connection_text_builder.Password = password;

            return new SqlConnection( connection_text_builder.ToString() );
        }
    }


}
