using System;


namespace Ixion.Std.IO {


    /// <summary>
    /// sprintf 関数の変換指定子を表します。
    /// </summary>
    public class PrintFormatSpecify {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="specify"></param>
        public PrintFormatSpecify(int min, int max, string specify) {
            this.min_ = min;
            this.max_ = max;
            this.full_specifier_ = specify.Substring( min, max - min );
        }


        /// <summary>
        /// 
        /// </summary>
        public int Min {
            get { return this.min_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int Max {
            get { return this.max_; }
        }


        /// <summary>
        /// 変換指定子を取得します。
        /// </summary>
        public string FullSpecifier {
            get { return this.full_specifier_; }
        }


        /// <summary>
        /// 符号指定子を取得します。
        /// </summary>
        public string SignedSpecifier {
            get { return this.signed_specifier_; }
        }


        /// <summary>
        /// パディング指定子を取得します。
        /// </summary>
        public string PaddingSpecifier {
            get { return this.padding_specifier_; }
        }


        /// <summary>
        /// アライメント指定子を取得します。
        /// </summary>
        public string AlignmentSpecifier {
            get { return this.alignment_specifier_; }
        }


        /// <summary>
        /// 表示幅指定子を取得します。
        /// </summary>
        public string DisplayWidthSpecifier {
            get { return this.display_width_specifier_; }
        }


        /// <summary>
        /// 精度指定子を取得します。
        /// </summary>
        public string PresicionSpecifier {
            get { return this.precision_specifier_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string TypeSpecifier {
            get { return this.type_specifier_; }
        }


        /// <summary>
        /// 
        /// </summary>
        private int min_ = int.MinValue;
        /// <summary>
        /// 
        /// </summary>
        private int max_ = int.MaxValue;
        /// <summary>
        /// 
        /// </summary>
        private string full_specifier_ = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string signed_specifier_ = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string padding_specifier_ = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string alignment_specifier_ = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string display_width_specifier_ = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string precision_specifier_ = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string type_specifier_ = string.Empty;
    }


}
