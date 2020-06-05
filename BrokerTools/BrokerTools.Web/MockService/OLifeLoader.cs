using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;

using BrokerTools.Web.Utilities;
using System.ServiceModel.Channels;
using FileHelpers;

namespace BrokerTools.Web.MockService
{
    public class OLifeLoader
    {
      
        public static OLife LoadByProductCode(ProductCode productCode)
        {
            FileHelperEngine engine = new FileHelperEngine(typeof(Funds));
            Funds[] funds = (Funds[])engine.ReadFile(HttpContext.Current.Server.MapPath("~/App_Data/Funds.csv"));

            OLife obj = new OLife();

            var lst = from x in funds
                      select new InvestmentProduct
                        {
                            id = String.Concat("STLB_InvestmentProduct_", x.TrustNo),
                            InvestProductSysKey = new String[] { String.Concat("Compass_FundCode{", x.JSECode.ToString(), "}") },
                            ProductCode = productCode.ToString("F"),
                            FullName = x.FundName,
                            SaleEffectiveDate = x.QuarterEnd
                        };

            OLife olife = new OLife();
            olife.Items = lst.ToArray();

            return olife;

        }

        public static OLife LoadBySourceLookup(SourceLookup sourceLookup)
        {

            FileHelperEngine engine = new FileHelperEngine(typeof(Funds));
            Funds[] fundsexport = (Funds[])engine.ReadFile(HttpContext.Current.Server.MapPath("~/App_Data/Funds.csv"));

            engine = new FileHelperEngine(typeof(FundPerformance));
            FundPerformance[] fundPerformance = (FundPerformance[])engine.ReadFile(HttpContext.Current.Server.MapPath("~/App_Data/FundPerformance.csv"));

            OLife olife = new OLife();

            List<OLifeBase> lstProduct = new List<OLifeBase>();

            //THIS WILL CHANGE ON LIVE
            //int SourceId = int.Parse(sourceLookup.SourceId);

           // var funds = fundsexport.Where(t => t.TrustNo == SourceId).ToList();

            //var sfund = fundsexport.FirstOrDefault(t => t.TrustNo == SourceId);

            var sfund = fundsexport.SingleOrDefault(t => t.JSECode == sourceLookup.SourceId);

            //foreach (var sfund in funds)
            //{
            if (sfund != null)
            {
                InvestmentProduct fund = new InvestmentProduct()
                {
                    id = String.Concat("STLB_InvestmentProduct_", sfund.TrustNo),
                    //InvestProductSysKey = new String[] { String.Concat("Silica_FundCode{", sfund.TrustNo.ToString(), "}") },
                    InvestProductSysKey = new String[] { String.Concat("Silica_FundCode{", sfund.JSECode.ToString(), "}") },
                    ProductCode = sourceLookup.ProductCode,
                    FullName = sfund.FundName,
                    SaleEffectiveDate = sfund.QuarterEnd
                };


                OLifEExtension ext = new OLifEExtension();
                List<OLifEExtension> extlst = new List<OLifEExtension>();

                List<FundPrice> fundprices = new List<FundPrice>();
                List<SectorAllocation> sectors = new List<SectorAllocation>();

                ext.VendorCode = "STANLIB";
                sectors.Add(new SectorAllocation
                {
                    JSECode = sfund.JSECode,
                    MarketSector = "Resources",
                    Local = sfund.Resources,
                    LocalSpecified = true
                });

                sectors.Add(new SectorAllocation
                {
                    JSECode = sfund.JSECode,
                    MarketSector = "Financials",
                    Local = sfund.Financials,
                    LocalSpecified = true
                });

                sectors.Add(new SectorAllocation
                {
                    JSECode = sfund.JSECode,
                    MarketSector = "Industrials",
                    Local = sfund.Industrials,
                    LocalSpecified = true
                });

                sectors.Add(new SectorAllocation
                {
                    JSECode = sfund.JSECode,
                    MarketSector = "Alt X",
                    Local = sfund.AltX,
                    LocalSpecified = true
                });

                sectors.Add(new SectorAllocation
                {
                    JSECode = sfund.JSECode,
                    MarketSector = "Property",
                    Local = sfund.Property,
                    LocalSpecified = true
                });

                sectors.Add(new SectorAllocation
                {
                    JSECode = sfund.JSECode,
                    MarketSector = "Bonds",
                    Local = sfund.Bonds,
                    LocalSpecified = true
                });

                sectors.Add(new SectorAllocation
                {
                    JSECode = sfund.JSECode,
                    MarketSector = "Cash",
                    Local = sfund.Cash,
                    LocalSpecified = true
                });

                sectors.Add(new SectorAllocation
                {
                    JSECode = sfund.JSECode,
                    MarketSector = "Foreign Equity",
                    Foreign = sfund.ForeignEquity,
                    ForeignSpecified = true
                });

                sectors.Add(new SectorAllocation
                {
                    JSECode = sfund.JSECode,
                    MarketSector = "Foreign Bonds",
                    Foreign = sfund.ForeignBonds,
                    ForeignSpecified = true
                });

                sectors.Add(new SectorAllocation
                {
                    JSECode = sfund.JSECode,
                    MarketSector = "Foreign Property",
                    Foreign = sfund.ForeignProperty,
                    ForeignSpecified = true
                });

                sectors.Add(new SectorAllocation
                {
                    JSECode = sfund.JSECode,
                    MarketSector = "Foreign Cash",
                    Foreign = sfund.ForeignCash,
                    ForeignSpecified = true
                });

                ext.Items = (object[])sectors.ToArray();
                extlst.Add(ext);

                ext = new OLifEExtension();

                var fundprice = fundPerformance.FirstOrDefault(t => t.JSECode.Trim() == sfund.JSECode.ToString());

                if (fundprice != null)
                {
                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M1"),
                        Performance = fundprice.M1,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M2"),
                        Performance = fundprice.M2,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M3"),
                        Performance = fundprice.M3,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M4"),
                        Performance = fundprice.M4,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M5"),
                        Performance = fundprice.M5
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M6"),
                        Performance = fundprice.M6,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M7"),
                        Performance = fundprice.M7
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M8"),
                        Performance = fundprice.M8,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M9"),
                        Performance = fundprice.M9
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M10"),
                        Performance = fundprice.M10,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M11"),
                        Performance = fundprice.M11
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M12"),
                        Performance = fundprice.M12,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M13"),
                        Performance = fundprice.M13,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M14"),
                        Performance = fundprice.M14,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M15"),
                        Performance = fundprice.M15
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M16"),
                        Performance = fundprice.M16,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M17"),
                        Performance = fundprice.M17,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M18"),
                        Performance = fundprice.M18,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M19"),
                        Performance = fundprice.M19,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M20"),
                        Performance = fundprice.M20,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M21"),
                        Performance = fundprice.M21,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M22"),
                        Performance = fundprice.M22,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M23"),
                        Performance = fundprice.M23,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M24"),
                        Performance = fundprice.M24,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M25"),
                        Performance = fundprice.M25,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M26"),
                        Performance = fundprice.M26
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M26"),
                        Performance = fundprice.M27,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M28"),
                        Performance = fundprice.M28,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M29"),
                        Performance = fundprice.M29,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M30"),
                        Performance = fundprice.M30,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M31"),
                        Performance = fundprice.M31,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M32"),
                        Performance = fundprice.M32,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M33"),
                        Performance = fundprice.M33,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M34"),
                        Performance = fundprice.M34,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M35"),
                        Performance = fundprice.M35,
                        Name = sfund.FundName,
                        Type = PriceType.Price
                    });

                    fundprices.Add(new FundPrice()
                    {
                        JSECode = fundprice.JSECode,
                        Date = LookupDate("M36"),
                        Performance = fundprice.M36
                    });

                    ext.Items = (object[])fundprices.ToArray();
                    extlst.Add(ext);
                }

                fund.OLifEExtension = extlst.ToArray();
                lstProduct.Add(fund);
            }
            //}

            olife.Items = lstProduct.ToArray();
            return olife;

        }

        public static DateTime LookupDate(string Column)
        {
            DateTime result = new DateTime();


            switch (Column)
            {
                case "M1":
                    result = DateTime.Parse("2007-07-28");
                    break;
                case "M2":
                    result = DateTime.Parse("2007-08-28");
                    break;
                case "M3":
                    result = DateTime.Parse("2007-09-28");
                    break;
                case "M4":
                    result = DateTime.Parse("2007-10-28");
                    break;
                case "M5":
                    result = DateTime.Parse("2007-11-28");
                    break;
                case "M6":
                    result = DateTime.Parse("2007-12-28");
                    break;
                case "M7":
                    result = DateTime.Parse("2008-01-28");
                    break;
                case "M8":
                    result = DateTime.Parse("2008-02-28");
                    break;
                case "M9":
                    result = DateTime.Parse("2008-03-28");
                    break;
                case "M10":
                    result = DateTime.Parse("2008-04-28");
                    break;
                case "M11":
                    result = DateTime.Parse("2008-05-28");
                    break;
                case "M12":
                    result = DateTime.Parse("2008-06-28");
                    break;
                case "M13":
                    result = DateTime.Parse("2008-07-28");
                    break;
                case "M14":
                    result = DateTime.Parse("2008-08-28");
                    break;
                case "M15":
                    result = DateTime.Parse("2008-09-28");
                    break;
                case "M16":
                    result = DateTime.Parse("2008-10-28");
                    break;
                case "M17":
                    result = DateTime.Parse("2008-11-28");
                    break;
                case "M18":
                    result = DateTime.Parse("2008-12-28");
                    break;
                case "M19":
                    result = DateTime.Parse("2009-01-28");
                    break;
                case "M20":
                    result = DateTime.Parse("2009-02-28");
                    break;
                case "M21":
                    result = DateTime.Parse("2009-03-28");
                    break;
                case "M22":
                    result = DateTime.Parse("2009-04-28");
                    break;
                case "M23":
                    result = DateTime.Parse("2009-05-28");
                    break;
                case "M24":
                    result = DateTime.Parse("2009-06-28");
                    break;
                case "M25":
                    result = DateTime.Parse("2009-07-28");
                    break;
                case "M26":
                    result = DateTime.Parse("2009-08-28");
                    break;
                case "M27":
                    result = DateTime.Parse("2009-09-28");
                    break;
                case "M28":
                    result = DateTime.Parse("2009-10-28");
                    break;
                case "M29":
                    result = DateTime.Parse("2009-11-28");
                    break;
                case "M30":
                    result = DateTime.Parse("2009-12-28");
                    break;
                case "M31":
                    result = DateTime.Parse("2010-01-28");
                    break;
                case "M32":
                    result = DateTime.Parse("2010-02-28");
                    break;
                case "M33":
                    result = DateTime.Parse("2010-03-30");
                    break;
                case "M34":
                    result = DateTime.Parse("2010-04-30");
                    break;
                case "M35":
                    result = DateTime.Parse("2010-05-30");
                    break;
                case "M36":
                    result = DateTime.Parse("2010-06-30");
                    break;
            }

            return result;
        }

        #region XML
        //public static Serializable.OLifE LoadByProductCode(ProductCode productCode)
        //{
        //    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/sample-olife (fund list).xml")))
        //    {
        //        return XMLSerializer.DeserializeObjectFromXML<Serializable.OLifE>(reader.ReadToEnd());
        //    }
        //}

        //public static Serializable.OLifE LoadBySourceLookup(SourceLookup sourceLookup)
        //{
        //    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/sample-olife (fund detail).xml")))
        //    {
        //        return XMLSerializer.DeserializeObjectFromXML<Serializable.OLifE>(reader.ReadToEnd());
        //    }
        //}
        #endregion
    }
}