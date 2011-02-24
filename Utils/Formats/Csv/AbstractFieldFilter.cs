using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Utils.Formats.Csv {


    /// <summary>
    ///
    /// </summary>
    public abstract class AbstractFieldFilter : IFieldFilter {
        #region IFieldFilter ÉÅÉìÉo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string Format(object field) {
            return Format( field, field.GetType() );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="field_type"></param>
        /// <returns></returns>
        public abstract string Format(object field, Type field_type);
        #endregion
    }


}
