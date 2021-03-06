﻿using System;

namespace BitAI.DataService.Models
{
    /// <summary>
    /// The result of the /account/getdepositaddress/ end point
    /// </summary>
    public class DepositAddress
    {
        /// <summary>
        /// The currency of the deposit address, i.e. BTC
        /// </summary>
        public String Currency { get; set; }
        /// <summary>
        /// The wallet address
        /// </summary>
        public String Address { get; set; }
    }
}
