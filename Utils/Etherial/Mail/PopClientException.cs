using System;
using System.Collections.Generic;
using System.Text;


namespace Ixion.Etherial.Mail {


    /// <summary>
    ///
    /// </summary>
    public class PopClientException : ApplicationException {
        /// <summary>
        /// 
        /// </summary>
        public PopClientException()
            : base() {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public PopClientException(string message)
            : base( message ) {
        }
    }


}
