using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace BoardGames
{
    internal sealed class KernelInstance
    {
        private readonly IKernel kernel;

        private static readonly KernelInstance instance = new KernelInstance();

        public static IKernel Kernel => instance.kernel;

        public static T Get<T>() => Kernel.Get<T>();

        public static T Get<T>(ConstructorArgument constructorArgument) => Kernel.Get<T>(constructorArgument);

        public KernelInstance()
        {
            kernel = new StandardKernel(new Kernels.BoardGameModule());
        } 
    }
}
