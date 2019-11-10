using Moq;
using System.Linq;

namespace BoardGamesOnlineTest.Helpers
{
    public static class MockDbSetHelper
    {
        public static void SetupData<T>(this Mock<System.Data.Entity.DbSet<T>> mock, IQueryable<T> dataList) where T : class
        {
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(dataList.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(dataList.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(dataList.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(dataList.GetEnumerator());
        }
    }
}
