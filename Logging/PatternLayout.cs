/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Logging {


    using Ixion.Logging.Helper;
    using Ixion.Logging.Spi;


    /// <summary>
    ///
    /// </summary>
    public class PatternLayout : Layout {
        /// <summary>
        /// 
        /// </summary>
        public PatternLayout()
            : this( DefaultConversionPattern ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        public PatternLayout(string pattern) {
            this.parser_ = CreatePatternParser( pattern );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        /// <returns></returns>
        public override string Format(LoggingEvent log_event) {
            return string.Format( "[{0}] - {1}", log_event.Level, log_event.Message );
        }


        /// <summary>
        /// 
        /// </summary>
        public override bool IgnoreException {
            get { return false; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        protected virtual PatternParser CreatePatternParser(string pattern) {
            return new PatternParser( pattern );
        }


        /// <summary>
        /// 
        /// </summary>
        public static readonly string DefaultConversionPattern = "%r [%t] %p %c %x - %m%n";
        /// <summary>
        /// 
        /// </summary>
        protected static readonly int BufferSize = 4096;
        /// <summary>
        /// 
        /// </summary>
        protected static readonly int MaxCapacity = 4096;


        /// <summary>
        /// 
        /// </summary>
        private PatternParser parser_;
    }


}
