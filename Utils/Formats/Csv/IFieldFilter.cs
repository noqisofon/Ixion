using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Utils.Formats.Csv {


    /// <summary>
    /// CSV のフィールドの値をフィルタリングする方法を提供します。
    /// </summary>
    public interface IFieldFilter {
        /// <summary>
        /// 指定されたオブジェクトをフィルタリングした文字列を返します。
        /// </summary>
        /// <param name="field">書き込むべきオブジェクト。</param>
        /// <returns>フィルタリングされた文字列。</returns>
        string Format(object field);
        /// <summary>
        /// 指定されたオブジェクトをフィルタリングした文字列を返します。
        /// </summary>
        /// <param name="field">書き込むべきオブジェクト。</param>
        /// <param name="field_type">オブジェクトの型。</param>
        /// <returns>フィルタリングされた文字列。</returns>
        string Format(object field, Type field_type);
    }


}
