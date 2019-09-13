using System.Dynamic;
using CheckSumSHA256Inspector.ViewModels;
using Ninject;

namespace CheckSumSHA256Inspector
{
    public static class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public static void Setup()
        {
            BindViewModels();
        }

        private static void BindViewModels()
        {
            Kernel.Bind<MainWindowViewModel>().ToConstant( new MainWindowViewModel() );
        }
    }
}