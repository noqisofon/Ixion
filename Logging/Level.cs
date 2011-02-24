/* -*- encoding: utf-8; -*- */
using System;


namespace Ixion.Logging {


    /// <summary>
    /// ログのレベルを表します。
    /// </summary>
    [Serializable]
    public class Level : IComparable<Level>, IEquatable<Level> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="level_text"></param>
        /// <param name="syslog_equivalent"></param>
        protected Level(int level, string level_text, int syslog_equivalent) {
            this.level_ = level;
            this.text_ = level_text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="priority"></param>
        internal Level(Priority priority)
            : this( (int)priority, priority.ToString().ToLower(), 0 ) {
        }



        #region IComparable<Level> メンバ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Level other) {
            return this.level_ - other.level_;
        }
        #endregion


        #region IEquatable<Level> メンバ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Level other) {
            return this.level_ == other.level_;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return this.level_;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Level ToLevel(int val) {
            switch ( val ) {
                case (int)Priority.All:
                    return All;

                case (int)Priority.Debug:
                    return Debug;

                case (int)Priority.Error:
                    return Error;

                case (int)Priority.Fatal:
                    return Fatal;

                case (int)Priority.Info:
                    return Info;

                case (int)Priority.None:
                    return None;

                case (int)Priority.Trace:
                    return Trace;

                case (int)Priority.Warn:
                    return Warn;
            }
            return Debug;
        }


        /// <summary>
        /// 
        /// </summary>
        public static readonly Level All = new Level( Priority.All );
        /// <summary>
        /// 
        /// </summary>
        public static readonly Level Trace = new Level( Priority.Trace );
        /// <summary>
        /// 
        /// </summary>
        public static readonly Level Debug = new Level( Priority.Debug );
        /// <summary>
        /// 
        /// </summary>
        public static readonly Level Info = new Level( Priority.Info );
        /// <summary>
        /// 
        /// </summary>
        public static readonly Level Warn = new Level( Priority.Warn );
        /// <summary>
        /// 
        /// </summary>
        public static readonly Level Error = new Level( Priority.Error );
        /// <summary>
        /// 
        /// </summary>
        public static readonly Level Fatal = new Level( Priority.Fatal );
        /// <summary>
        /// 
        /// </summary>
        public static readonly Level None = new Level( Priority.None );


        /// <summary>
        /// 
        /// </summary>
        private int level_;
        /// <summary>
        /// 
        /// </summary>
        private string text_;
    }


}
