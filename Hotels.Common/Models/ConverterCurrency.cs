﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
  public  class ConverterCurrency
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public double ExchangeRate { get; set; }

    }
}
