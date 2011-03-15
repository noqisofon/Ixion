/* -*- encoding: utf-8; -*- */
using System;
using System.Diagnostics;
using System.Reflection;


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
            this.full_info_ = string.Format( "{0}::{1} ({2}:{3})", classname, method, file, line );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caller_stack_boundary_declaring_type"></param>
        public LocationInfo(Type caller_stack_boundary_declaring_type)
            : this( "?", "?", "?", "?" ) {
            StackTrace stack_trace = new StackTrace( true );
            StackFrame stack_frame = null;
            int frame_index = 0;

            if ( caller_stack_boundary_declaring_type != null ) {
                while ( frame_index < stack_trace.FrameCount ) {
                    stack_frame = stack_trace.GetFrame( frame_index );
                    MethodInfo method_info = (MethodInfo)( stack_frame.GetMethod() );
                    if ( method_info.DeclaringType == caller_stack_boundary_declaring_type )
                        break;

                    ++frame_index;
                }
            } else {
                stack_frame = stack_trace.GetFrame( 1 );
            }

            this.class_name_ = stack_frame.GetMethod().DeclaringType.Name;
            this.method_name_ = stack_frame.GetMethod().Name;
            this.file_name_ = stack_frame.GetFileName();
            this.line_number_ = string.Format( "{0},{1}",
                                               stack_frame.GetFileLineNumber(),
                                               stack_frame.GetFileColumnNumber() );
            this.full_info_ = string.Format( "{0}::{1} ({2}:{3})", this.ClassName, this.MethodName, this.FileName, this.LineNumber );
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
        /// <returns></returns>
        public override string ToString() {
            return string.Format( "{0}::{1} ({2}:{3})", this.ClassName, this.MethodName, this.FileName, this.LineNumber );
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
        /// <summary>
        /// 
        /// </summary>
        private string full_info_;
    }


}
