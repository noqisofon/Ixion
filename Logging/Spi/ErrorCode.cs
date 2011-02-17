/* -*- encoding: utf-8; -*- */
using System;


namespace Vanila.Logging.Spi {


    /// <summary>
    ///
    /// </summary>
    public enum ErrorCode : uint {
        /// <summary>
        /// 
        /// </summary>
        AddressParseFailure = 2 << 1,
        /// <summary>
        /// 
        /// </summary>
        CloseFailure = 2 << 2,
        /// <summary>
        /// 
        /// </summary>
        FileOpenFailure = 2 << 3,
        /// <summary>
        /// 
        /// </summary>
        FlushFailure = 2 << 4,
        /// <summary>
        /// 
        /// </summary>
        GenericFailure = 2 << 5,
        /// <summary>
        /// 
        /// </summary>
        MissingLayout = 2 << 6,
        /// <summary>
        /// 
        /// </summary>
        WriteFailure = 2 << 7
    }


}
