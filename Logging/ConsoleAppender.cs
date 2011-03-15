/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging {


    /// <summary>
    ///
    /// </summary>
    public class ConsoleAppender : WriterAppender {
        /// <summary>
        /// 
        /// </summary>
        public ConsoleAppender()
            : this( new SimpleLayout() ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        public ConsoleAppender(Layout layout)
            : base( layout, Console.OpenStandardError() ) {
        }
    }


}
