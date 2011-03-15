/* -*- encoding: utf-8; -*- */
using System;
using System.Text;


namespace Ixion.Logging.Helper {


    /// <summary>
    /// PatternLayout クラスの殆どの作業はこのクラスに移譲されます。
    /// </summary>
    public class PatternParser {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        public PatternParser(string pattern) {
            this.pattern_ = pattern;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        internal void addConverter(PatternConvertor converter) {
        }


        /// <summary>
        /// 
        /// </summary>
        internal void finalizeConvertor() {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PatternConvertor Parse() {
            return /*new PatternConvertor()*/null;
        }

        
        /// <summary>
        /// 
        /// </summary>
        private StringBuilder current_literal_ = new StringBuilder();
        /// <summary>
        /// 
        /// </summary>
        private FormattingInfo fomatting_info_ = null;
        /// <summary>
        /// 
        /// </summary>
        private int index_ = 0;
        /// <summary>
        /// 
        /// </summary>
        private string pattern_;
    }


}
