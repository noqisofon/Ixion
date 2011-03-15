using System;
using System.Data.Common;


namespace Ixion.Utils.Relation.Common {


    /// <summary>
    ///
    /// </summary>
    public abstract class DatabaseProvider {

        /// <summary>
        /// 空の DbProvider オブジェクトを作成します。
        /// </summary>
        public DatabaseProvider()
            : this( string.Empty, string.Empty ) {
        }
        /// <summary>
        /// ローカルな SQL Server の、指定されたデータベースへの接続オブジェクトを
        /// 作成するための DbProvider オブジェクトを作成します。
        /// </summary>
        /// <param name="initial_catalog">データベース名。</param>
        public DatabaseProvider(string initial_catalog)
            : this( initial_catalog, "(local)" ) {
        }
        /// <summary>
        /// 指定された SQL Server の、指定されたデータベースへの接続オブジェクトを
        /// 作成するための DbProvider オブジェクトを作成します。
        /// </summary>
        /// <param name="initial_catalog">データベース名。</param>
        /// <param name="data_source">SQL Server の名前。</param>
        public DatabaseProvider(string initial_catalog, string data_source) {
            this.DataSource = data_source;
            this.InitialCatalog = initial_catalog;
            this.Timeout = 30;
        }


        /// <summary>
        /// 接続する SQL Server の名前を取得したり、設定したりします。
        /// </summary>
        public string DataSource {
            get { return this.data_source_; }
            set { this.data_source_ = value; }
        }


        /// <summary>
        /// 接続するデータベース名の名前を取得したり、設定したりします。
        /// </summary>
        public string InitialCatalog {
            get { return this.initial_catalog_; }
            set { this.initial_catalog_ = value; }
        }


        /// <summary>
        /// データベースへ接続するときのタイムアウトを取得したり設定したりします。
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
        /// 接続するサーバー名を表します。
        /// </summary>
        private string data_source_;
        /// <summary>
        /// 接続するデータベース名を表します。
        /// </summary>
        private string initial_catalog_;
        /// <summary>
        /// 
        /// </summary>
        private int timeout_;
    }


}
