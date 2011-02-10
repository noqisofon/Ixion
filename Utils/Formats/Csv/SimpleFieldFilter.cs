using System;
using System.Collections.Generic;
using System.Text;


namespace Vanila.Utils.Formats.Csv {


    /// <summary>
    /// �V���v���ȃt�B�[���h�t�B���^�[�ł��B
    /// </summary>
    public class SimpleFieldFilter : AbstractFieldFilter {
        /// <summary>
        /// �w�肳�ꂽ�I�u�W�F�N�g���t�B���^�����O�����������Ԃ��܂��B
        /// </summary>
        /// <param name="field">�������ނׂ��I�u�W�F�N�g�B</param>
        /// <param name="field_type">�I�u�W�F�N�g�̌^�B</param>
        /// <returns>�t�B���^�����O���ꂽ������B</returns>
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
