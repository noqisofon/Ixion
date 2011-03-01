/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging {


    /// <summary>
    ///
    /// </summary>
    public class RootLogger : Logger {
        /// <summary>
        /// 
        /// </summary>
        internal RootLogger()
            : base( "root" ) {
            base.Level = Level.Info;
            base.AddAppender( new ConsoleAppender() );
        }
    }


}
