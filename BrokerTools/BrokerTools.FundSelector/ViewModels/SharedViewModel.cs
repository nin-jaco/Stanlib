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
using BrokerTools.Base;
using BrokerTools.Data;
using BrokerTools.Data.Controller;
using System.Collections.ObjectModel;
using BrokerTools.Data.Model;
using System.ComponentModel;

namespace BrokerTools.FundSelector.ViewModels
{
    public enum BreadcrumbState
    {
        FundsAndClientDetails,
        AssetAllocationCheck,
        ComplianceCheck,
        RiskAssessor,
        CashFlowAnalysis
    }

    public class SharedViewModel : BaseViewModel
    {

        #region :: Fields ::

        /// <summary>
        ///  Field backing the "ClientPortfolio" property.
        /// </summary>
        Portfolio _clientportFolio;

        /// <summary>
        /// Field backing the "CurrentBreadcrumbState" property.
        /// </summary>
        BreadcrumbState _CurrentBreadcrumbState;

        /// <summary>
        /// Field backing the "SelectedAdvisor" property.
        /// </summary>
        Advisor _SelectedAdvisor;

        /// <summary>
        /// Field backing the "SelectedInvestmentTerm" property.
        /// </summary>
        InvestmentTerm _SelectedInvestmentTerm;

        /// <summary>
        /// Field backing the "SelectedProduct" property.
        /// </summary>
        Product _SelectedProduct;

        /// <summary>
        /// Field backing the "AdvisorList" property.
        /// </summary>
        ObservableCollection<Advisor> _Advisors = null;

        /// <summary>
        /// Field backing the "InvestmentTerms" property.
        /// </summary>
        ObservableCollection<InvestmentTerm> _InvestmentTerms = null;

        /// <summary>
        /// Field backing the "Products" property.
        /// </summary>
        ObservableCollection<Product> _Products = null;

        #endregion

        #region :: Properties ::

        /// <summary>
        /// Gets or sets the current Portfolio
        /// </summary>
        public Portfolio ClientPortfolio
        {
            get { return _clientportFolio; }
            set
            {
                _clientportFolio = value;
                RaisePropertyChanged(() => ClientPortfolio);
            }
        }

        /// <summary>
        /// Gets or sets the current breadcrumb state for this viewmodel.
        /// </summary>
        public BreadcrumbState CurrentBreadcrumbState
        {
            get
            {
                return _CurrentBreadcrumbState;
            }
            set
            {
                _CurrentBreadcrumbState = value;
                base.NotifyPropertyChanged("CurrentBreadcrumbState");
            }
        }

        /// <summary>
        /// Gets or sets the selected advisor.
        /// </summary>
        public Advisor SelectedAdvisor
        {
            get { return _SelectedAdvisor; }
            set
            {
                _SelectedAdvisor = value;
                RaisePropertyChanged(() => SelectedAdvisor);
            }
        }

        /// <summary>
        /// Gets or sets the selected investment term.
        /// </summary>
        public InvestmentTerm SelectedInvestmentTerm
        {
            get { return _SelectedInvestmentTerm; }
            set
            {
                _SelectedInvestmentTerm = value;
                RaisePropertyChanged(() => SelectedInvestmentTerm);
            }
        }

        /// <summary>
        /// Gets or sets the selected product.
        /// </summary>
        public Product SelectedProduct
        {
            get { return _SelectedProduct; }
            set
            {
                _SelectedProduct = value;
                RaisePropertyChanged(() => SelectedProduct);

                this.SelectedProductFunds = FundController.GetFundByProductCode(SelectedProduct.Code).ToObservableCollection<Fund>();

            }
        }

        /// <summary>
        /// Gets the List of funds for the selected Product
        /// </summary>
        public ObservableCollection<Fund> SelectedProductFunds  { get; set; }

        /// <summary>
        /// Gets or sets the various advisors.
        /// </summary>
        public ObservableCollection<Advisor> Advisors
        {
            get { return _Advisors; }
            set
            {
                _Advisors = value;
                RaisePropertyChanged(() => Advisors);
            }
        }

        /// <summary>
        /// Gets or sets the various investment terms.
        /// </summary>
        public ObservableCollection<InvestmentTerm> InvestmentTerms
        {
            get { return _InvestmentTerms; }
            set
            {
                _InvestmentTerms = value;
                RaisePropertyChanged(() => InvestmentTerms);
            }
        }

        /// <summary>
        /// Gets or sets the various products.
        /// </summary>
        public ObservableCollection<Product> Products
        {
            get { return _Products; }
            set
            {
                _Products = value;
                RaisePropertyChanged(() => Products);
            }
        }

        /// <summary>
        /// Funds Selected in the Portfolio
        /// </summary>
        public ObservableCollection<PortfolioFund> PortFolioFunds { get; set; }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Field backing the "Instance" property.
        /// </summary>
        static volatile SharedViewModel _Instance;

        /// <summary>
        /// Thread lock.
        /// </summary>
        static object _SyncRoot = new Object();
        /// <summary>
        /// Gets the viewmodel container instance.
        /// </summary>
        public static SharedViewModel Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new SharedViewModel();
                        }
                    }
                }
                return _Instance;
            }
        }

        public SharedViewModel()
        {
            //InitiateScreenData();

            SelectedProduct = ProductController.GetProductByCode(Data.FundProxy.ProductCode.IP);
            PortFolioFunds = new ObservableCollection<PortfolioFund>();


            PortFolioFunds.Add(new PortfolioFund()
            {
                FundOID = 1,
                Weighting = 0.1,
                TheFund = FundController.GetFundByOID(1)

            });

            PortFolioFunds.Add(new PortfolioFund()
            {
                FundOID = 2,
                Weighting = 0.9,
                TheFund = FundController.GetFundByOID(2)

            });

            PortFolioFunds.Add(new PortfolioFund()
            {
                FundOID = 3,
                Weighting = 2.5,
                TheFund = FundController.GetFundByOID(3)

            });

            PortFolioFunds.Add(new PortfolioFund()
            {
                FundOID = 4,
                Weighting = 3.5,
                TheFund = FundController.GetFundByOID(4)

            });
        }
            
        #endregion

        #region :: Private Methods ::

        /// <summary>
        /// Loads the default data reqruired by the UI.
        /// </summary>
        private void InitiateScreenData()
        {
            SetAdvisors();
            SetInvestmentTerms();
            SetProducts();
        }

        /// <summary>
        /// Sets the advisors from the local database.
        /// </summary>
        private void SetAdvisors()
        {
            Advisors = AdvisorController.GetAllAdvisors().ToObservableCollection<Advisor>();
        }

        /// <summary>
        /// Sets the investment terms from the local database.
        /// </summary>
        private void SetInvestmentTerms()
        {
            InvestmentTerms = InvestmentTermController.GetAllInvestmentTerms().ToObservableCollection<InvestmentTerm>();
        }

        /// <summary>
        /// Sets the products from the local database.
        /// </summary>
        private void SetProducts()
        {
            Products = ProductController.GetAllProduct().ToObservableCollection<Product>();
        }

        private void SetRiskProfiles()
        {

        }

        #endregion

    }
}
