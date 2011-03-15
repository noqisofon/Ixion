using System;


namespace Ixion.Utils.Formats {


    /// <summary>
    /// �I�u�W�F�N�g�� CSV �̃t�B�[���h�Ƃ��ď������ގ��̕�����`���ɕϊ�������@��񋟂��܂��B
    /// </summary>
    public interface IFieldFilter {
        /// <summary>
        /// �I�u�W�F�N�g���K��̕��@�ŕ�����ɕϊ����܂��B
        /// </summary>
        /// <param name="value">������ɕϊ��������I�u�W�F�N�g�B</param>
        /// <returns>�ϊ����ꂽ������B</returns>
        string Format(object value);
        /// <summary>
        /// �I�u�W�F�N�g���w�肳�ꂽ�^�Ƃ��ĕ�����ɕϊ����܂��B
        /// </summary>
        /// <param name="field_type">�I�u�W�F�N�g�̌^�B</param>
        /// <param name="field_value">������ɕϊ��������I�u�W�F�N�g�B</param>
        /// <returns>�ϊ����ꂽ������B</returns>
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
        /// <returns>�ϊ����ꂽ������B</returns>
        string Format(string column_name, Type field_type, object field_value);
    }


}
