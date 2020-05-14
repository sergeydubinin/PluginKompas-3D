using System;
using BushingParametrs;
using NUnit.Framework;
using System.Collections.Generic;

namespace BushingPlugin.UnitTests
{
    [TestFixture]
    public class BushingPluginTest
    {
        [TestCase(TestName = "Негативный: Длина всей втулки менее 20 мм.")]
        public void SetTotalLengthLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "длина всей втулки не может быть " +
                "менее 20 мм.";
            listError.Add(ParametersType.TotalLength, message);
            Bushing bushing = new Bushing(15, 7, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Длина всей втулки более 100 мм.")]
        public void SetTotalLengthMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "длина всей втулки не может быть " +
                "более 100 мм.";
            listError.Add(ParametersType.TotalLength, message);
            Bushing bushing = new Bushing(120, 50, 120, 80, 53.3, 6, 10.6,
                96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Длина верхней части втулки менее " +
            "5 мм.")]
        public void SetTopLengthLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "длина верхней части втулки не может " +
                "быть менее 5 мм.";
            listError.Add(ParametersType.TopLength, message);
            Bushing bushing = new Bushing(20, 3, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Длина верхней части втулки более " +
            "1/2 длины всей втулки мм.")]
        public void SetTopLengthMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "длина верхней части втулки не может " +
                "быть более 1/2 длины всей втулки мм.";
            listError.Add(ParametersType.TopLength, message);
            Bushing bushing = new Bushing(100, 60, 120, 80, 53.3, 6, 10.6,
                96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диаметр верхней части втулки " +
            "менее 55 мм.")]
        public void SetTopDiametrLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "диаметр верхней части втулки не может " +
                "быть менее 55 мм.";
            listError.Add(ParametersType.TopDiametr,message);
            Bushing bushing = new Bushing(20, 5, 54, 35, 20, 2, 4, 41);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диаметр верхней части втулки " +
            "быть более 120 мм.")]
        public void SetTopDiametrMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "диаметр верхней части втулки не может " +
                "быть более 120 мм.";
            listError.Add(ParametersType.TopDiametr, message);
            Bushing bushing = new Bushing(100, 50, 130, 80, 53.3, 6, 10.6,
                100);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Внешний диаметр втулки менее " +
            "35 мм.")]
        public void SetOuterDiametrLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "внешний диаметр втулки не может быть " +
                "менее 35 мм.";
            listError.Add(ParametersType.OuterDiametr, message);
            Bushing bushing = new Bushing(20, 5, 55, 34, 20, 2, 4, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Внешний диаметр втулки более " +
            "2/3 диаметра верхней части втулки мм.")]
        public void SetOuterDiametrMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "внешний диаметр втулки не может быть " +
                "более 2/3 диаметра верхней части втулки мм.";
            listError.Add(ParametersType.OuterDiametr, message);
            Bushing bushing = new Bushing(100, 50, 120, 85, 53.3, 6, 10.6,
                96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Внутренний диаметр втулки менее " +
            "20 мм.")]
        public void SetInnerDiametrLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "внутренний диаметр втулки не может " +
                "быть менее 20 мм.";
            listError.Add(ParametersType.InnerDiametr, message);
            Bushing bushing = new Bushing(20, 5, 55, 35, 19, 2, 4.5, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Внутренний диаметр втулки более " +
            "2/3 внешнего диаметра втулки мм.")]
        public void SetInnerDiametrMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "внутренний диаметр втулки не может " + 
                "быть более 2/3 внешнего диаметра втулки мм.";
            listError.Add(ParametersType.InnerDiametr, message);
            Bushing bushing = new Bushing(100, 50, 120, 80, 55, 6, 13, 96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Количество отверстий менее 2 шт.")]
        public void SetNumberHolesLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "количество отверстий не может быть " +
                "менее 2 шт.";
            listError.Add(ParametersType.NumberHoles, message);
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 1, 4, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Количество отверстий более 6 шт.")]
        public void SetNumberHolesMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "количество отверстий не может быть " +
                "более 6 шт.";
            listError.Add(ParametersType.NumberHoles, message);
            Bushing bushing = new Bushing(100, 50, 120, 80, 53.3, 7, 13.3, 
                96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диаметр отверстий менее 4 мм.")]
        public void SetHolesDiametrLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError =
                new Dictionary<ParametersType, string>();
            string message = "диаметр отверстий не может быть менее " +
                "4 мм.";
            listError.Add(ParametersType.HolesDiametr, message);
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 3, 44);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диаметр отверстий более 1/4 " +
            "внутреннего диаметра втулки мм.")]
        public void SetHolesDiametrMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "диаметр отверстий не может быть более " +
                "1/4 внутреннего диаметра втулки мм.";
            listError.Add(ParametersType.HolesDiametr, message);
            Bushing bushing = new Bushing(100, 50, 120, 80, 53.3, 6, 15, 96);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диаметр расположения отверстий " +
            "менее 3/4 диаметра верхней части втулки мм.")]
        public void SetLocationDiametrLess_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "диаметр расположения отверстий не " +
                "может быть менее 3/4 диаметра верхней части втулки мм.";
            listError.Add(ParametersType.LocationDiametr, message);
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 40);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Диаметр расположения отверстий " +
            "более 4/5 диаметра верхней части втулки мм.")]
        public void SetLocationDiametrMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "диаметр расположения отверстий не " + 
                "может быть более 4/5 диаметра верхней части втулки мм.";
            listError.Add(ParametersType.LocationDiametr, message);
            Bushing bushing = new Bushing(100, 50, 120, 80, 53.3, 6, 13.3,
                100);
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Текст гравировки более 15 " +
            "символов")]
        public void SetEngravingTextMore_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "текст гравировки не может быть более 15 " +
                "символов";
            listError.Add(ParametersType.EngravingText, message);
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44,
                "ТекстТекстТекстТекст");
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(TestName = "Негативный: Не введен текст гравировки")]
        public void SetEngravingTextNull_NegativeTest()
        {
            Dictionary<ParametersType, string> listError = 
                new Dictionary<ParametersType, string>();
            string message = "не введен текст гравировки";
            listError.Add(ParametersType.EngravingText, message);
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44, "");
            Assert.AreEqual(bushing._listError, listError);
        }

        [TestCase(20, TestName = "Позитивный: Получение длины всей втулки")]
        [Test]
        public void TestTotalLengthGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.TotalLength);
        }

        [TestCase(5, TestName = "Позитивный: Получение длины верхней части " +
            "втулки")]
        [Test]
        public void TestTopLengthGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.TopLength);
        }

        [TestCase(55, TestName = "Позитивный: Получение диаметра верхней " +
            "части втулки")]
        [Test]
        public void TestTopDiametrGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.TopDiametr);
        }

        [TestCase(35, TestName = "Позитивный: Получение внешнего диаметра " +
            "втулки")]
        [Test]
        public void TestOuterDiametrGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.OuterDiametr);
        }

        [TestCase(20, TestName = "Позитивный: Получение внутреннего " +
            "диаметра втулки")]
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

        [TestCase(44, TestName = "Позитивный: Получение диаметра " +
            "расположения отверстий")]
        [Test]
        public void TestLocationDiametrGet(double value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44);
            Assert.AreEqual(value, bushing.LocationDiametr);
        }

        [TestCase("ГОСТ", TestName = "Позитивный: Получение текста " +
            "гравировки")]
        [Test]
        public void TestEngravingTextGet(string value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44, 
                "ГОСТ");
            Assert.AreEqual(value, bushing.EngravingText);
        }

        [TestCase(true, TestName = "Позитивный: Получение наличия " +
            "гравировки")]
        [Test]
        public void TestPresenceEngravingGet(bool value)
        {
            Bushing bushing = new Bushing(20, 5, 55, 35, 20, 2, 4, 44,
                "ГОСТ");
            Assert.AreEqual(value, bushing.PresenceEngraving);
        }
    }
}
