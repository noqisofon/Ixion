using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Utils.Formats.Csv {


    /// <summary>
    /// CSV �̃t�B�[���h�̒l���t�B���^�����O������@��񋟂��܂��B
    /// </summary>
    public interface IFieldFilter {
        /// <summary>
        /// �w�肳�ꂽ�I�u�W�F�N�g���t�B���^�����O�����������Ԃ��܂��B
        /// </summary>
        /// <param name="field">�������ނׂ��I�u�W�F�N�g�B</param>
        /// <returns>�t�B���^�����O���ꂽ������B</returns>
        string Format(object field);
        /// <summary>
        /// �w�肳�ꂽ�I�u�W�F�N�g���t�B���^�����O�����������Ԃ��܂��B
        /// </summary>
        /// <param name="field">�������ނׂ��I�u�W�F�N�g�B</param>
        /// <param name="field_type">�I�u�W�F�N�g�̌^�B</param>
        /// <returns>�t�B���^�����O���ꂽ������B</returns>
        string Format(object field, Type field_type);
    }


}
