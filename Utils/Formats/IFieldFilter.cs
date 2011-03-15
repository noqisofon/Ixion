using System;


namespace Ixion.Utils.Formats {


    /// <summary>
    /// オブジェクトを CSV のフィールドとして書き込む時の文字列形式に変換する方法を提供します。
    /// </summary>
    public interface IFieldFilter {
        /// <summary>
        /// オブジェクトを規定の方法で文字列に変換します。
        /// </summary>
        /// <param name="value">文字列に変換したいオブジェクト。</param>
        /// <returns>変換された文字列。</returns>
        string Format(object value);
        /// <summary>
        /// オブジェクトを指定された型として文字列に変換します。
        /// </summary>
        /// <param name="field_type">オブジェクトの型。</param>
        /// <param name="field_value">文字列に変換したいオブジェクト。</param>
        /// <returns>変換された文字列。</returns>
        string Format(Type field_type, object field_value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field_name"></param>
        /// <param name="field_value"></param>
        /// <returns></returns>
        string Format(string field_name, object field_value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column_name"></param>
        /// <param name="field_type"></param>
        /// <param name="field_value"></param>
        /// <returns>変換された文字列。</returns>
        string Format(string column_name, Type field_type, object field_value);
    }


}
