using System;
using BushingPlugin;
using NUnit.Framework;

namespace BushingPlugin.UnitTests
{
    [TestFixture]
    public class BushingPluginTest
    {
        [TestCase(100, 50, 120, 80, 53.3, 6, 13.3, 96,
            TestName = "Позитивный: Максимальные значения параметров втулки")]
        [TestCase(20, 5, 55, 35, 20, 2, 4, 44,
            TestName = "Позитивный: Минимальные значения параметров втулки")]
        [TestCase(50, 15, 80, 45, 25, 4, 5.5, 62,
            TestName = "Позитивный: Средние значения параметров втулки")]
        [Test]
        public void TestPositiveBushingConstructor(double totalLength, double topLength, 
            double topDiametr, double outerDiametr, double innerDiametr,
            int numberHoles, double holesDiametr, double locationDiametr)
        {
            Assert.DoesNotThrow((() =>
            {
                new Bushing (totalLength, topLength, topDiametr, outerDiametr, innerDiametr,
            numberHoles, holesDiametr, locationDiametr);
            }
            ));
        }

        [TestCase(15, 7, 55, 35, 20, 2, 4, 44, typeof (TotalLengthException),
            TestName = "Негативный: Длина всей втулки менее 20 мм.")]
        [TestCase(120, 50, 120, 80, 53.3, 6, 10.6, 96, typeof(TotalLengthException),
            TestName = "Негативный: Длина всей втулки более 100 мм.")]

        [TestCase(20, 3, 55, 35, 20, 2, 4, 44, typeof(TopLengthException),
            TestName = "Негативный: Длина верхней части втулки менее 5 мм.")]
        [TestCase(100, 60, 120, 80, 53.3, 6, 10.6, 96, typeof(TopLengthException),
            TestName = "Негативный: Длина верхней части втулки более 1/2 длины всей втулки мм.")]

        [TestCase(20, 5, 54, 35, 20, 2, 4, 41, typeof(TopDiametrException),
            TestName = "Негативный: Диамтер верхней части втулки менее 55 мм.")]
        [TestCase(100, 50, 130, 80, 53.3, 6, 10.6, 100, typeof(TopDiametrException),
            TestName = "Негативный: Диамтер верхней части втулки быть более 120 мм.")]

        [TestCase(20, 5, 55, 34, 20, 2, 4, 44, typeof(OuterDiametrException),
            TestName = "Негативный: Наружный диамтер втулки менее 35 мм.")]
        [TestCase(100, 50, 120, 85, 53.3, 6, 10.6, 96, typeof(OuterDiametrException),
            TestName = "Негативный: Наружный диамтер втулки более 2/3 диаметра верхней части втулки мм.")]

        [TestCase(20, 5, 55, 35, 19, 2, 4.5, 44, typeof(InnerDiametrException),
            TestName = "Негативный: Внутренний диамтер втулки менее 20 мм.")]
        [TestCase(100, 50, 120, 80, 55, 6, 13, 96, typeof(InnerDiametrException),
            TestName = "Негативный: Внутренний диамтер втулки более 2/3 внешнего диаметра втулки мм.")]

        [TestCase(20, 5, 55, 35, 20, 1, 4, 44, typeof(NumberHolesException),
            TestName = "Негативный: Количество отверстий менее 2 шт.")]
        [TestCase(100, 50, 120, 80, 53.3, 7, 13.3, 96, typeof(NumberHolesException),
            TestName = "Негативный: Количество отверстий более 6 шт.")]

        [TestCase(20, 5, 55, 35, 20, 2, 3, 44, typeof(HolesDiametrException),
            TestName = "Негативный: Диамтер отверстий менее 4 мм.")]
        [TestCase(100, 50, 120, 80, 53.3, 6, 15, 96, typeof(HolesDiametrException),
            TestName = "Негативный: Диамтер отверстий более 1/4 внутреннего диаметра втулки мм.")]

        [TestCase(20, 5, 55, 35, 20, 2, 4, 40, typeof(LocationDiametrException),
            TestName = "Негативный: Диамтер расположения отверстий менее 3/4 диаметра верхней части втулки мм.")]
        [TestCase(100, 50, 120, 80, 53.3, 6, 13.3, 100, typeof(LocationDiametrException),
            TestName = "Негативный: Диамтер расположения отверстий более 4/5 диаметра верхней части втулки мм.")]

        [Test]
        public void TestNegativeBushingConstructor(double totalLength, double topLength,
            double topDiametr, double outerDiametr, double innerDiametr,
            int numberHoles, double holesDiametr, double locationDiametr, Type exceptionType)
        {
            Assert.That(() =>
            {
                new Bushing(totalLength, topLength, topDiametr, outerDiametr, innerDiametr,
                    numberHoles, holesDiametr, locationDiametr);
            }, Throws.TypeOf(exceptionType));
        }

        [TestCase(20, TestName = "Позитивный: Получение длины всей втулки")]
        [Test]
        public void TestTotalLengthGet(double value)
        {
           Bushing bushing = new Bushing (20, 5, 55, 35, 20, 2, 4, 44);
           Assert.AreEqual(value, bushing.TotalLength);
        }

        [TestCase(5, TestName = "Позитивный: Получение длины верхней части втулки")]
        [Test]
        public void TestTopLengthGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.TopLength);
        }

        [TestCase(55, TestName = "Позитивный: Получение диаметра верхней части втулки")]
        [Test]
        public void TestTopDiametrGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.TopDiametr);
        }

        [TestCase(35, TestName = "Позитивный: Получение внешнего диаметра втулки")]
        [Test]
        public void TestOuterDiametrGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.OuterDiametr);
        }

        [TestCase(20, TestName = "Позитивный: Получение внутреннего диаметра втулки")]
        [Test]
        public void TestInnerDiametrGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.InnerDiametr);
        }

        [TestCase(2, TestName = "Позитивный: Получение количества отверстий")]
        [Test]
        public void TestNumberHolesGet(int value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.NumberHoles);
        }

        [TestCase(4, TestName = "Позитивный: Получение диаметра отверстий")]
        [Test]
        public void TestHolesDiametrGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.HolesDiametr);
        }

        [TestCase(44, TestName = "Позитивный: Получение диаметра расположения отверстий")]
        [Test]
        public void TestLocationDiametrGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.LocationDiametr);
        }
    }
}
