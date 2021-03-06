﻿using Chronox.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Wrappers
{
    public class ResultWrapper
    {
        
        public ChronoxParser Parser { get; private set; }

        public List<IChronoxExtraction> Results { get; set; }

        public ResultWrapper(ChronoxParser parser, List<IChronoxExtraction> results)
        {
            Parser = parser;
            Results = results;
        }

        public static ResultWrapper EmptyResult(ChronoxParser parser){
            return new ResultWrapper(parser, new List<IChronoxExtraction>());
        }
    }
}
