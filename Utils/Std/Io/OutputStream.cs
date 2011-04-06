/* -*- encoding: utf-8; -*- */


namespace Std.Io {
    
    
    /// <summary>
    /// 
    /// </summary>
    public abstract class OutputStream : Closeable, Flushable {
        /// <summary>
        /// 
        /// </summary>
        public OutputStream() {

        }


        /// <summary>
        /// 
        /// </summary>
        public virtual void Close() {
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual void Flush() {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        public virtual void Write(byte[] buffer) {
            this.Write( buffer, 0, buffer.Length );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        public virtual void Write(byte[] buffer, int offset, int len) {
            int stop = len - offset;
            for ( int i = offset; i < stop; ++i ) {
                this.Write( buffer[i] );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        public abstract void Write(int b);
    }


}
