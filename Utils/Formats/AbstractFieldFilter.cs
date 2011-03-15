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
        /// �w�肳�ꂽ��������_�u���N�H�[�e�[�V�����Ŋ���܂��B
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string EnclosedDoubleQuotes(string field) {
            return string.Concat( "\"", field, "\"" );
        }


        #region IFieldFilter �����o
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Format(object value) {
            return this.Format( value.GetType(), value );
        }
        /// <summary>
        /// �I�u�W�F�N�g���w�肳�ꂽ�^�Ƃ��ĕ�����ɕϊ����܂��B
        /// </summary>
        /// <param name="field_type">�I�u�W�F�N�g�̌^�B</param>
        /// <param name="field_value">������ɕϊ��������I�u�W�F�N�g�B</param>
        /// <returns>�ϊ����ꂽ������B</returns>
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
