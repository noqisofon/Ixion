using System;
using System.Collections.Generic;
using System.Text;


namespace Vanila.Logging {


    /// <summary>
    /// ���O�̃��x����\���܂��B
    /// </summary>
    public enum Level : uint {
        /// <summary>
        /// �v���I�ȃG���[��\���܂��B
        /// </summary>
        Fatal = 10000,
        /// <summary>
        /// �G���[��\���܂��B
        /// </summary>
        Error = 8000,
        /// <summary>
        /// �x����\���܂��B
        /// </summary>
        Warn = 7000,
        /// <summary>
        /// ����\���܂��B
        /// </summary>
        Info = 4000,
        /// <summary>
        /// �f�o�b�O�p����\���܂��B
        /// </summary>
        Debug = 2000,
        /// <summary>
        /// �g���[�X����\���܂��B
        /// </summary>
        Trace = 1000,
        /// <summary>
        /// 
        /// </summary>
        None = 0
    }



}
