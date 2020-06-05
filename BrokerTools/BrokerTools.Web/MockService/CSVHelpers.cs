using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;

namespace BrokerTools.Web.MockService
{
    [DelimitedRecord(",")]
    public class Funds
    {
        [FieldIgnored]
        public string InvestProductSysKey;

        public int TrustNo;
        public string JSECode;
        public string FundName;
        public DateTime QuarterEnd;	
        public double Resources;
        public double Financials;
        public double Industrials;
        public double AltX;
        public double Property;
        public double Bonds;
        public double Cash;
        public double ForeignEquity;
        public double ForeignBonds;
        public double ForeignProperty;
        public double ForeignCash;
    }
            
    [DelimitedRecord(",")]
    public class FundPerformance
    {

        public string JSECode;
        public double	M1;
        public double	M2;
        public double	M3;
        public double	M4;
        public double	M5;
        public double	M6;
        public double	M7;
        public double	M8;
        public double	M9;
        public double	M10;
        public double	M11;
        public double	M12;
        public double	M13;
        public double	M14;
        public double	M15;
        public double	M16;
        public double	M17;
        public double	M18;
        public double	M19;
        public double	M20;
        public double	M21;
        public double	M22;
        public double	M23;
        public double	M24;
        public double	M25;
        public double	M26;
        public double	M27;
        public double	M28;
        public double	M29;
        public double	M30;
        public double	M31;
        public double	M32;
        public double	M33;
        public double	M34;
        public double	M35;
        public double   M36;



    }
}