using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Web;
using System.Threading;

namespace BrokerTools.Web.MockService
{
    [ServiceBehavior(Name = "WcfFundService",
                     Namespace = "urn:Stanlinb.BrokerTools",
                     IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)] 
    public class FundService : IFundService
    {

        OLife IFundService.GetFunds(ProductCode productCode)
        {

            return OLifeLoader.LoadByProductCode(productCode);

            #region XML
            //Serializable.OLifE obj = OLifeLoader.LoadByProductCode(productCode);

            //var qry = from x in obj.InvestProduct
            //          select new InvestmentProduct
            //          {
            //            CarrierName = x.CarrierName,
            //            InvestProductSysKey = new String[]{x.InvestProductSysKey},
            //            ProductSymbol = x.ProductSymbol,
            //            FundFamilyName = x.FundFamilyName,
            //            FullName = x.FullName,
            //            SaleEffectiveDate = x.SaleEffectiveDate
            //          };

            //OLife olife = new OLife();
            //olife.Items = qry.ToArray();

            //return olife;
            #endregion
        }

        OLife IFundService.GetFund(SourceLookup sourceLookup)
        {

            return OLifeLoader.LoadBySourceLookup(sourceLookup);

            #region XML
            //Serializable.OLifE obj = OLifeLoader.LoadBySourceLookup(sourceLookup);

            //var sfund = obj.InvestProduct.FirstOrDefault();

            //InvestmentProduct fund = new InvestmentProduct()
            //{
            //    CarrierName = sfund.CarrierName,
            //    InvestProductSysKey = new String[] { sfund.InvestProductSysKey },
            //    ProductSymbol = sfund.ProductSymbol,
            //    FundFamilyName = sfund.FundFamilyName,
            //    FullName = sfund.FullName,
            //    SaleEffectiveDate = sfund.SaleEffectiveDate
            //};


            //List<OLifEExtension> extlst = new List<OLifEExtension>();

            //foreach (var itm in sfund.OLifEExtension)
            //{

            //    OLifEExtension ext = new OLifEExtension();
            //    ext.VendorCode = itm.VendorCode;

            //    if (itm.SectorAllocation != null && itm.SectorAllocation.Count() > 0)
            //    {

            //        List<SectorAllocation> marketsectors = new List<SectorAllocation>();

            //        foreach (var sec in itm.SectorAllocation)
            //        {
            //            marketsectors.Add(new SectorAllocation()
            //            {
            //                JSECode = sec.JSECode,
            //                Local = Convert.ToDouble(sec.Local),
            //                MarketSector = sec.MarketSector,
            //                Total = sec.Total,
            //            });
            //        }

            //        ext.Items = (object[])marketsectors.ToArray();
            //        extlst.Add(ext);
            //    }


            //    if (itm.FundPrice != null && itm.FundPrice.Count() > 0)
            //    {
            //        List<FundPrice> fundprices = new List<FundPrice>();

            //        foreach (var pr in itm.FundPrice)
            //        {
            //            fundprices.Add(new FundPrice()
            //            {
            //                Date = pr.Date,
            //                Name = pr.Name,
            //                Performance = Convert.ToDouble(pr.Performance)
            //            });
            //        }

            //        ext.Items = (object[])fundprices.ToArray();
            //        extlst.Add(ext);
            //    }
            //}


            //OLife olife = new OLife();
            //fund.OLifEExtension = extlst.ToArray();

            //List<OLifeBase> lst = new List<OLifeBase>();
            //lst.Add(fund);

            //olife.Items = lst.ToArray();
            //return olife;
            #endregion
        }
    }
}
