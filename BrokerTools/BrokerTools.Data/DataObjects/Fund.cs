using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BrokerTools.Data.FundProxy;
using System.Collections.Generic;
using System.Linq;
using Sqo.Attributes;

namespace BrokerTools.Data.Model
{

    public partial class Fund
    {

        public Fund()
        { 
        
        }

        public Fund(InvestmentProduct investmentproduct)
        {

            this.PerformanceList = new List<FundPerformance>();

            ProductCode code;
            Enum.TryParse<ProductCode>(investmentproduct.ProductCode, out code);
            this.ProductCode = code;
            
            this.FullName = investmentproduct.FullName;
            this.ID = investmentproduct.id;

            List<SectorAllocation1> sectorlist = new List<SectorAllocation1>();
            List<FundPrice1> performancelist = new List<FundPrice1>();

            foreach (var ext in investmentproduct.OLifEExtension.ToList())
            {
                foreach (var item in ext.Items)
                {
                    if (item.GetType() == typeof(SectorAllocation1))
                        sectorlist.Add(item as SectorAllocation1);

                    else if (item.GetType() == typeof(FundPrice1))
                        performancelist.Add(item as FundPrice1);
                }
            }

            SetSectorValues(sectorlist);
            SetPerformanceValues(performancelist);
        }

        /// <summary>
        /// Set Fund Sector Values
        /// </summary>
        /// <param name="sectorlist"></param>
        void SetSectorValues(List<SectorAllocation1> sectorlist)
        {
            SectorAllocation1 sector = sectorlist.FirstOrDefault(t => t.MarketSector.ToUpper() == "RESOURCES");
            if (sector != null && sector.Local.HasValue)
                this.Resources = Math.Round(sector.Local.Value / 100, 4);
            else
                this.Resources = 0.0d;

            sector = sectorlist.FirstOrDefault(t => t.MarketSector.ToUpper() == "FINANCIALS");

            if (sector != null && sector.Local.HasValue)
                this.Financials = Math.Round(sector.Local.Value / 100, 4);
            else
                this.Financials = 0.0d;

            sector = sectorlist.FirstOrDefault(t => t.MarketSector.ToUpper() == "INDUSTRIALS");

            if (sector != null && sector.Local.HasValue)
                this.Industrials = Math.Round(sector.Local.Value / 100, 4);
            else
                this.Industrials = 0.0d;

            sector = sectorlist.FirstOrDefault(t => t.MarketSector.ToUpper() == "ALT X");

            if (sector != null && sector.Local.HasValue)
                this.AltX = Math.Round(sector.Local.Value / 100, 4);
            else
                this.AltX = 0.0d;

            sector = sectorlist.FirstOrDefault(t => t.MarketSector.ToUpper() == "PROPERTY");

            if (sector != null && sector.Local.HasValue)
                this.Property = Math.Round(sector.Local.Value / 100, 4);
            else
                this.Property = 0.0d;

            sector = sectorlist.FirstOrDefault(t => t.MarketSector.ToUpper() == "BONDS");

            if (sector != null && sector.Local.HasValue)
                this.Bonds = Math.Round(sector.Local.Value / 100, 4);
            else
                this.Bonds = 0.0d;

            sector = sectorlist.FirstOrDefault(t => t.MarketSector.ToUpper() == "CASH");

            if (sector != null && sector.Local.HasValue)
                this.Cash = Math.Round(sector.Local.Value / 100, 4);
            else
                this.Cash = 0.0d;

            sector = sectorlist.FirstOrDefault(t => t.MarketSector.ToUpper() == "FOREIGN EQUITY");

            if (sector != null && sector.Foreign.HasValue)
                this.ForeignEquity = Math.Round(sector.Foreign.Value / 100, 4);
            else
                this.ForeignEquity = 0.0d;

            sector = sectorlist.FirstOrDefault(t => t.MarketSector.ToUpper() == "FOREIGN BONDS");

            if (sector != null && sector.Foreign.HasValue)
                this.ForeignBonds = Math.Round(sector.Foreign.Value / 100, 4);
            else
                this.ForeignBonds = 0.0d;

            sector = sectorlist.FirstOrDefault(t => t.MarketSector.ToUpper() == "FOREIGN PROPERTY");

            if (sector != null && sector.Foreign.HasValue)
                this.ForeignProperty = Math.Round(sector.Foreign.Value / 100, 4);
            else
                this.ForeignProperty = 0.0d;

            sector = sectorlist.FirstOrDefault(t => t.MarketSector.ToUpper() == "FOREIGN CASH");

            if (sector != null && sector.Foreign.HasValue)
                this.ForeignCash = Math.Round(sector.Foreign.Value / 100, 4);
            else
                this.ForeignCash = 0.0d;


            this.Equity = this.Resources + this.Financials + this.Industrials + this.AltX;

            //double FundRisk = this.Equity * 5;
            this.FundRisk += this.Equity * 5;
            this.FundRisk += this.Property * 4;
            this.FundRisk += this.Bonds * 2;
            this.FundRisk += this.Cash * 1;
            this.FundRisk += this.ForeignEquity * 5;
            this.FundRisk += this.ForeignBonds * 3;
            this.FundRisk += this.ForeignProperty * 3;
            this.FundRisk += this.ForeignCash * 3;

            this.FundRisk = Math.Round(this.FundRisk, 4);
            this.RiskRating = (int)(FundRisk < 0 ? Math.Floor(this.FundRisk - 0.5) : Math.Ceiling(this.FundRisk + 0.5));
        }

        /// <summary>
        /// Set Fund Performance Values
        /// </summary>
        /// <param name="fundpricelist"></param>
        void SetPerformanceValues(List<FundPrice1> performances)
        {
            foreach (var pi in performances)
            {
                this.PerformanceList.Add(new FundPerformance()
                {
                    FundID = this.ID,
                    Date = pi.Date,
                    Performance = pi.Performance
                });
            }
        }

        public object GetValue(System.Reflection.FieldInfo field)
        {
            return field.GetValue(this);
        }

        public void SetValue(System.Reflection.FieldInfo field, object value)
        {
            field.SetValue(this, value);
        }
    }
}
