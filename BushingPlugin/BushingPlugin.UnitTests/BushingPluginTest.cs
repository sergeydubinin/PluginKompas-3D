using System;
using BushingPlugin;
using NUnit.Framework;
using System.Collections.Generic;

namespace BushingPlugin.UnitTests
{
    [TestFixture]
    public class BushingPluginTest
    {
        [TestCase(TestName = "Негативный: Длина всей втулки менее 20 мм.")] /////////////
        public void SetTotalLengthLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.TotalLength, "Значение параметра введено некорректно: " +
                "длина всей втулки не может быть менее 20 мм.");
            Bushing bushing = new Bushing(15, 7, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Длина всей втулки более 100 мм.")] /////////////////
        public void SetTotalLengthMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.TotalLength, "Значение параметра введено некорректно: " +
                "длина всей втулки не может быть более 100 мм.");
            Bushing bushing = new Bushing(120, 50, 120, 80, 53.3, 6, 10.6, 96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Длина верхней части втулки менее 5 мм.")]
        public void SetTopLengthLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.TopLength, "Значение параметра введено некорректно: " +
                "длина верхней части втулки не может быть менее 5 мм.");
            Bushing bushing = new Bushing(20, 3, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Длина верхней части втулки более 1/2 длины всей втулки мм.")]
        public void SetTopLengthMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.TopLength, "Значение параметра введено некорректно: " +
                "длина верхней части втулки не может быть более 1/2 длины всей втулки мм.");
            Bushing bushing = new Bushing(100, 60, 120, 80, 53.3, 6, 10.6, 96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диамтер верхней части втулки менее 55 мм.")] ////////////////
        public void SetTopDiametrLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.TopDiametr, "Значение параметра введено некорректно: " +
                "диамтер верхней части втулки не может быть менее 55 мм.");
            Bushing bushing = new Bushing(20, 5, 54, 35, 20, 2, 4, 41);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диамтер верхней части втулки быть более 120 мм.")] /////////////////
        public void SetTopDiametrMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.TopDiametr, "Значение параметра введено некорректно: " +
                "диамтер верхней части втулки не может быть более 120 мм.");
            Bushing bushing = new Bushing(100, 50, 130, 80, 53.3, 6, 10.6, 100);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Внешний диамтер втулки менее 35 мм.")] ////////////////////
        public void SetOuterDiametrLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.OuterDiametr, "Значение параметра введено некорректно: " +
                "внешний диамтер втулки не может быть менее 35 мм.");
            Bushing bushing = new Bushing(20, 5, 55, 34, 20, 2, 4, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Внешний диамтер втулки более 2/3 диаметра верхней части втулки мм.")] //////////////
        public void SetOuterDiametrMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.OuterDiametr, "Значение параметра введено некорректно: " +
                "внешний диамтер втулки не может быть более 2/3 диаметра верхней части втулки мм.");
            Bushing bushing = new Bushing(100, 50, 120, 85, 53.3, 6, 10.6, 96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Внутренний диамтер втулки менее 20 мм.")] /////////////////
        public void SetInnerDiametrLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.InnerDiametr, "Значение параметра введено некорректно: " +
                "внутренний диамтер втулки не может быть менее 20 мм.");
            Bushing bushing = new Bushing(20, 5, 55, 35, 19, 2, 4.5, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Внутренний диамтер втулки более 2/3 внешнего диаметра втулки мм.")] //////////////////
        public void SetInnerDiametrMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.InnerDiametr, "Значение параметра введено некорректно: " +
                "внутренний диамтер втулки не может быть более 2/3 внешнего диаметра втулки мм.");
            Bushing bushing = new Bushing(100, 50, 120, 80, 55, 6, 13, 96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Количество отверстий менее 2 шт.")]
        public void SetNumberHolesLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.NumberHoles, "Значение параметра введено некорректно: " +
                "количество отверстий не может быть менее 2 шт.");
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 1, 4, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Количество отверстий более 6 шт.")]
        public void SetNumberHolesMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.NumberHoles, "Значение параметра введено некорректно: " +
                "количество отверстий не может быть более 6 шт.");
            Bushing bushing = new Bushing(100, 50, 120, 80, 53.3, 7, 13.3, 96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диамтер отверстий менее 4 мм.")]
        public void SetHolesDiametrLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.HolesDiametr, "Значение параметра введено некорректно: " +
                        "диамтер отверстий не может быть менее 4 мм.");
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 3, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диамтер отверстий более 1/4 внутреннего диаметра втулки мм.")]
        public void SetHolesDiametrMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.HolesDiametr, "Значение параметра введено некорректно: " +
                        "диамтер отверстий не может быть более 1/4 внутреннего диаметра втулки мм.");
            Bushing bushing = new Bushing(100, 50, 120, 80, 53.3, 6, 15, 96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диамтер расположения отверстий менее 3/4 диаметра верхней части втулки мм.")]
        public void SetLocationDiametrLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.LocationDiametr, "Значение параметра введено некорректно: " +
                "диамтер расположения отверстий не может быть менее 3/4 диаметра верхней части втулки мм.");
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 40);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диамтер расположения отверстий более 4/5 диаметра верхней части втулки мм.")]
        public void SetLocationDiametrMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = new Dictionary<ParametersType, string>();
            listError.Add(ParametersType.LocationDiametr, "Значение параметра введено некорректно: " +
                "диамтер расположения отверстий не может быть более 4/5 диаметра верхней части втулки мм.");
            Bushing bushing = new Bushing(100, 50, 120, 80, 53.3, 6, 13.3, 100);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(20, TestName = "Позитивный: Получение длины всей втулки")]
        [Test]
        public void TestTotalLengthGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
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

        [TestCase(100, 50, 120, 80, 53.3, 6, 13.3, 96,
            TestName = "Позитивный: Максимальные значения параметров втулки")]
        [TestCase(20, 5, 55, 35, 20, 2, 4, 44,
            TestName = "Позитивный: Минимальные значения параметров втулки")]
        [TestCase(50, 15, 80, 45, 25, 4, 5.5, 62,
            TestName = "Позитивный: Средние значения параметров втулки")]
        public void TestPositiveBushingConstructor(double totalLength, double topLength,
            double topDiametr, double outerDiametr, double innerDiametr,
            int numberHoles, double holesDiametr, double locationDiametr)
        {
            Bushing bushing = new Bushing(totalLength, topLength, topDiametr, outerDiametr, innerDiametr,
                    numberHoles, holesDiametr, locationDiametr);
            Assert.AreEqual(bushing.TotalLength, totalLength);
            Assert.AreEqual(bushing.TopLength, topLength);
            Assert.AreEqual(bushing.TopDiametr, topDiametr);
            Assert.AreEqual(bushing.OuterDiametr, outerDiametr);
            Assert.AreEqual(bushing.InnerDiametr, innerDiametr);
            Assert.AreEqual(bushing.NumberHoles, numberHoles);
            Assert.AreEqual(bushing.HolesDiametr, holesDiametr);
            Assert.AreEqual(bushing.LocationDiametr, locationDiametr);
        }
    }
}
