/* -*- encoding: utf-8; -*- */
using System;


namespace Ixion.Logging.Spi {


    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public class LocationInfo {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="classname"></param>
        /// <param name="method"></param>
        /// <param name="line"></param>
        public LocationInfo(string file, string classname, string method, string line) {
            this.file_name_ = file;
            this.class_name_ = classname;
            this.method_name_ = method;
            this.line_number_ = line;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fqn_of_caling_class"></param>
        public LocationInfo(Exception e, string fqn_of_caling_class) {
        }


        /// <summary>
        /// 
        /// </summary>
        public string ClassName {
            get { return this.class_name_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string FileName {
            get { return this.file_name_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string LineNumber {
            get { return this.line_number_; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string MethodName {
            get { return this.method_name_; }
        }


        /// <summary>
        /// 
        /// </summary>
        private string class_name_;
        /// <summary>
        /// 
        /// </summary>
        private string file_name_;
        /// <summary>
        /// 
        /// </summary>
        private string line_number_;
        /// <summary>
        /// 
        /// </summary>
        private string method_name_;
    }


}
