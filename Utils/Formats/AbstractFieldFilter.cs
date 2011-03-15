using System;


namespace Ixion.Utils.Formats {


    /// <summary>
    ///
    /// </summary>
    public abstract class AbstractFieldFilter : IFieldFilter {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsString(object value) {
            return IsString( value.GetType() );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsString(Type type) {
            return type.Equals( typeof( string ) );
        }


        /// <summary>
        /// 指定された文字列をダブルクォーテーションで括ります。
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string EnclosedDoubleQuotes(string field) {
            return string.Concat( "\"", field, "\"" );
        }


        #region IFieldFilter メンバ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Format(object value) {
            return this.Format( value.GetType(), value );
        }
        /// <summary>
        /// オブジェクトを指定された型として文字列に変換します。
        /// </summary>
        /// <param name="field_type">オブジェクトの型。</param>
        /// <param name="field_value">文字列に変換したいオブジェクト。</param>
        /// <returns>変換された文字列。</returns>
        public abstract string Format(Type field_type, object field_value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column_name"></param>
        /// <param name="field_value"></param>
        /// <returns></returns>
        public string Format(string column_name, object field_value) {
            return this.Format( column_name, field_value.GetType(), field_value );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column_name"></param>
        /// <param name="field_type"></param>
        /// <param name="field_value"></param>
        /// <returns></returns>
        public abstract string Format(string column_name, Type field_type, object field_value);
        #endregion
    }


}
