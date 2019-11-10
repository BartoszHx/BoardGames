using Ninject;

namespace BoardGames.KernelModules
{
    internal static class KernelInstance
    {
	    private static IKernel chessKernel;
	    private static IKernel checkerKernel;

	    public static IKernel ChessKernel => chessKernel ?? (chessKernel = new StandardKernel(new ChessModule()));
	    public static IKernel CheckerKernel => checkerKernel ?? (checkerKernel = new StandardKernel(new CheckersModule()));
    }
}
