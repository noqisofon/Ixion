using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Utils.Formats {


    /// <summary>
    /// フィルタリングを行わないフィルターです。
    /// </summary>
    public class NilFieldFilter : AbstractFieldFilter {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field_type"></param>
        /// <param name="field_value"></param>
        /// <returns></returns>
        public override string Format(Type field_type, object field_value) {
            return field_value.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column_name"></param>
        /// <param name="field_type"></param>
        /// <param name="field_value"></param>
        /// <returns></returns>
        public override string Format(string column_name, Type field_type, object field_value) {
            return field_value.ToString();
        }
    }


}
