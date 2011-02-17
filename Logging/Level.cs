using System;
using System.Collections.Generic;
using System.Text;


namespace Vanila.Logging {


    /// <summary>
    /// ログのレベルを表します。
    /// </summary>
    public enum Level : uint {
        /// <summary>
        /// 致命的なエラーを表します。
        /// </summary>
        Fatal = 10000,
        /// <summary>
        /// エラーを表します。
        /// </summary>
        Error = 8000,
        /// <summary>
        /// 警告を表します。
        /// </summary>
        Warn = 7000,
        /// <summary>
        /// 情報を表します。
        /// </summary>
        Info = 4000,
        /// <summary>
        /// デバッグ用情報を表します。
        /// </summary>
        Debug = 2000,
        /// <summary>
        /// トレース情報を表します。
        /// </summary>
        Trace = 1000,
        /// <summary>
        /// 
        /// </summary>
        None = 0
    }



}
