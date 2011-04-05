using System;


namespace Ixion.Std.Io {


    /// <summary>
    ///   
    /// </summary>
    /// <remarks>
    /// <para>PrintFormatParser �N���X�� sprintf �t�H�[�}�b�g��������R���p�C�����đf�����ϊ����悤�Ƃ���
    /// �R���Z�v�g�̌��ɏ�����܂����B</para>
    /// </remarks>
    public class PrintFormatParser {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        public PrintFormatParser(string format) {
            this.format_ = format;
        }


        /// <summary>
        /// 
        /// </summary>
        public string FormatString {
            get { return this.format_; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// <para>PFC �I�u�W�F�N�g�� PFS �I�u�W�F�N�g��ǉ����Ă��܂��B
        /// PFS �I�u�W�F�N�g�͕ϊ��w��q��\���A�Ή���������̕ϊ����@���w�肵�܂��B</para>
        /// </remarks>
        /// <returns></returns>
        public PrintFormatConvertor Parse() {
            bool whatever_loop = true;

            int specify_start = 0,
                specify_end = 0;
            //int normal_text_size = 0;
            char[] type_specifies = new char[] { 'b', 'd', 'u', 'o', 'x', 'X', 'f', 'c', 's', '%' };
            PrintFormatSpecify specify = null;

            PrintFormatConvertor convertor = new PrintFormatConvertor( this.FormatString );

            while ( whatever_loop ) {
                if ( specify != null )
                    specify_start += specify.FullSpecifier.Length;

                specify_end = this.format_.IndexOf( '%', specify_start );
                if ( specify_end == -1 )
                    whatever_loop = false;

                //normal_text_size = specify_end == -1 ? this.FormatString.Length - specify_start : specify_end - specify_start;

                if ( !whatever_loop )
                    continue;

                specify_start = this.FormatString.IndexOf( '%', specify_start );
                specify_end = this.FormatString.IndexOfAny( type_specifies, specify_start + 1 ) + 1;
                specify = new PrintFormatSpecify( specify_start, specify_end, this.FormatString );

                convertor.AddSpecify( specify );
            }
            return convertor;
        }


        /// <summary>
        /// 
        /// </summary>
        private string format_;
    }


}
