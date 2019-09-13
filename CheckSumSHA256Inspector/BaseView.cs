using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using CheckSumSHA256Inspector.ViewModels;

namespace CheckSumSHA256Inspector
{
    public class BaseView<TModel> : BaseView
        where TModel : BaseViewModel, new()
    {
        public TModel ViewModel
        {
            get => (TModel)ViewModelObject;
            set => ViewModelObject = value;
        }

        #region Constructor

        public BaseView() : base()
        {
            ViewModel = IoC.Get<TModel>();
        }

        public BaseView(TModel specificViewModel = null) : base()
        {
            ViewModel = specificViewModel ?? IoC.Get<TModel>();
        }

        #endregion
    }

    public class BaseView : UserControl
    {
        private object _viewModel;
        public BaseView()
        {
            // don't run in design mode
            if (DesignerProperties.GetIsInDesignMode(this)) return;


        }

        public object ViewModelObject
        {
            get { return _viewModel; }
            set
            {
                // If nothing has changed, return
                if (_viewModel == value)
                    return;

                // Update the value
                _viewModel = value;

                // Fire the view model changed method
                OnViewModelChanged();

                // Set the data context for this page
                DataContext = _viewModel;
            }
        }

        protected virtual void OnViewModelChanged()
        {
        }
    }
}