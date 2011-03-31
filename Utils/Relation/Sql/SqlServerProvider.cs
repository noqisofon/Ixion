using System;
using System.Data.Common;
using System.Data.SqlClient;


namespace Ixion.Utils.Relation.Sql {


    using Ixion.Utils.Relation.Common;


    /// <summary>
    /// SQL Server への接続オブジェクトを作成するためのファクトリクラスです。
    /// </summary>
    public class SqlServerProvider : DatabaseProvider {
        /// <summary>
        /// 空の SqlServerProvider オブジェクトを作成します。
        /// </summary>
        public SqlServerProvider()
            : base( string.Empty, string.Empty ) {
        }
        /// <summary>
        /// ローカルな SQL Server の、指定されたデータベースへの接続オブジェクトを
        /// 作成するための SqlServerProvider オブジェクトを作成します。
        /// </summary>
        /// <param name="initial_catalog">データベース名。</param>
        public SqlServerProvider(string initial_catalog)
            : base( initial_catalog, "(local)" ) {
        }
        /// <summary>
        /// 指定された SQL Server の、指定されたデータベースへの接続オブジェクトを
        /// 作成するための SqlServerProvider オブジェクトを作成します。
        /// </summary>
        /// <param name="initial_catalog">データベース名。</param>
        /// <param name="data_source">SQL Server の名前。</param>
        public SqlServerProvider(string initial_catalog, string data_source)
            : base( initial_catalog, data_source ) {
        }


        /// <summary>
        /// SqlServer への接続オブジェクトを作成して返します。
        /// </summary>
        /// <returns>新しい接続オブジェクト。</returns>
        new public SqlConnection GetConnection() {
            return (SqlConnection)GetDbConnection();
        }
        /// <summary>
        /// アカウント名とパスワードを指定して SqlServer への接続オブジェクトを作成して返します。
        /// </summary>
        /// <param name="acount_name">接続時に使用するユーザー ID。</param>
        /// <param name="password">ユーザー ID のパスワード。</param>
        /// <returns>新しい接続オブジェクト。</returns>
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
