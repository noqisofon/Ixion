using System;
using System.Collections.Generic;
using System.Text;


namespace Vanila.Utils.Formats.Csv {


    /// <summary>
    /// シンプルなフィールドフィルターです。
    /// </summary>
    public class SimpleFieldFilter : AbstractFieldFilter {
        /// <summary>
        /// 指定されたオブジェクトをフィルタリングした文字列を返します。
        /// </summary>
        /// <param name="field">書き込むべきオブジェクト。</param>
        /// <param name="field_type">オブジェクトの型。</param>
        /// <returns>フィルタリングされた文字列。</returns>
        public override string Format(object field, Type field_type) {
            if ( field_type == typeof( string ) ) {
                string temp_field = field.ToString();
                
                if ( temp_field.IndexOf( Environment.NewLine ) != -1 || temp_field.IndexOf( ',' ) != -1 ) {
                    return string.Format( "\"{0}\"", field );
                } else {
                    return field.ToString();
                }
            } else {
                return field.ToString();
            }
        }
    }


}
