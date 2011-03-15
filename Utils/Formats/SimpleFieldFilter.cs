using System;


namespace Ixion.Utils.Formats {


    /// <summary>
    /// 
    /// </summary>
    public class SimpleFieldFilter : AbstractFieldFilter {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field_type"></param>
        /// <param name="field_value"></param>
        /// <returns></returns>
        public override string Format(Type field_type, object field_value) {
            if ( field_value == null )
                return string.Empty;
            if ( field_type == typeof( string ) )
                return base.EnclosedDoubleQuotes( field_value.ToString() );

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
            return this.Format( field_type, field_value );
        }
    }


}
