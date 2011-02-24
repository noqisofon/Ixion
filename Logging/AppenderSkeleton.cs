/* -*- encoding: utf-8; -*- */
using System;
using System.Collections.Generic;
using System.Threading;


namespace Ixion.Logging {


    using Ixion.Logging.Spi;


    /// <summary>
    ///
    /// </summary>
    public abstract class AppenderSkeleton : IAppender, IOptionHandler {
        /// <summary>
        /// 
        /// </summary>
        protected AppenderSkeleton()
            : this( false ) {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="is_active"></param>
        protected AppenderSkeleton(bool is_active) {
            this.closed = false;
            this.ErrorHandler = null;
            this.filters = new List<Filter>();
            this.Layout = null;
            this.Name = string.Empty;
            this.Threshold = Level.Debug;
        }


        /// <summary>
        /// 
        /// </summary>
        ~AppenderSkeleton() {
            this.Dispose( false );
        }


        /// <summary>
        /// 
        /// </summary>
        public Level Threshold {
            get { return this.threshold_; }
            set { this.threshold_ = value; }
        }


        /// <summary>
        /// 指定されたプライオリティがアペンダーのしきい値を下回っているかどうか判別します。
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsAsSevereAsThreshould(Level level) {
            return this.Threshold.CompareTo( level ) > 0;
        }


        #region IAppender メンバ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_filter"></param>
        public void AddFilter(Filter new_filter) {
            this.filters.Add( new_filter );
        }


        /// <summary>
        /// 
        /// </summary>
        public void ClearFilters() {
            this.filters.Clear();
        }


        /// <summary>
        /// 
        /// </summary>
        public abstract void Close();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="log_event"></param>
        public void DoAppend(LoggingEvent log_event) {
            Append( log_event );
        }


        /// <summary>
        /// 
        /// </summary>
        public Filter Filter {
            get {
                if ( this.filters_.Count == 0 )
                    return null;

                return this.filters_[0];
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public Layout Layout {
            get { return this.layout_; }
            set { this.layout_ = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string Name {
            get { return this.name_; }
            set { this.name_ = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public IErrorHandler ErrorHandler {
            get { return this.error_handler_; }
            set { this.error_handler_ = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public abstract bool RequiresLayout();
        #endregion


        #region IOptionHandler メンバ
        /// <summary>
        /// 
        /// </summary>
        public virtual void ActiveOptions() {
        }
        #endregion


        #region protected-properties
        /// <summary>
        /// 
        /// </summary>
        protected bool closed {
            get { return this.closed_; }
            set { this.closed_ = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        protected IList<Filter> filters {
            get { return this.filters_; }
            set { this.filters_ = value; }
        }
        #endregion


        #region protected-methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logging_event"></param>
        protected abstract void Append(LoggingEvent logging_event);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dispoing"></param>
        protected virtual void Dispose(bool dispoing) {
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        private bool closed_;
        /// <summary>
        /// 
        /// </summary>
        private IErrorHandler error_handler_;
        /// <summary>
        /// 
        /// </summary>
        private IList<Filter> filters_;
        /// <summary>
        /// 
        /// </summary>
        private Layout layout_;
        /// <summary>
        /// 
        /// </summary>
        private string name_;
        /// <summary>
        /// 
        /// </summary>
        private Level threshold_;


        /// <summary>
        ///
        /// </summary>
        internal int objectID { get { return object_id_; } }


        /// <summary>
        ///
        /// </summary>
        internal readonly int object_id_ = Interlocked.Increment( ref object_type_count_ );


        /// <summary>
        ///
        /// </summary>
        private static int object_type_count_;
    }


}
