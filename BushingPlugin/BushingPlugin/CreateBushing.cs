using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;

namespace BushingPlugin
{
    public class CreateBushing
    {
        /// <summary>
        /// Объект Компас API
        /// </summary>
        private KompasObject _kompas;

        /// <summary>
        /// Параметризированный конструктор
        /// </summary>
        /// <param name="kompas"></param>
        public CreateBushing(KompasObject kompas)
        {
            _kompas = kompas;
            var document = (ksDocument3D)kompas.Document3D();
            document.Create();
        }

        /// <summary>
        /// Построение детали по заданным параметрам
        /// </summary>
        /// <param name="bushing"></param>
        public void CreateDetail(Bushing bushing)
        {
            double radiusCap = bushing.TopDiametr / 2;
            double heightCap = bushing.TopLength;
            double locationRadius = bushing.LocationDiametr / 2;
            double heightArray = bushing.TotalLength - bushing.TopLength;
            double radiusArray = bushing.HolesDiametr / 2;
            int numberHoles = bushing.NumberHoles;
            double radiusLeg = bushing.OuterDiametr / 2;
            double heightLeg = bushing.TotalLength - bushing.TopLength;
            double radiusHole = bushing.InnerDiametr / 2;

            CreateCap(radiusCap, heightCap);
            CreateArray(locationRadius, radiusArray, heightArray, numberHoles);
            CreateLeg(radiusLeg, heightLeg);
            CreateHole(radiusHole, heightCap, heightLeg);
        }

        /// <summary>
        /// Создание верхней части втулки
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="height"></param>
        private void CreateCap(double radius, double height)
        {
            var document = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            // Создаем эскиз окружности
            ksEntity entitySketch = DrawCircle(part, radius);
            // Выдавливаем по созданному эскизу
            ExtrudeSketch(part, entitySketch, height, false);
        }

        /// <summary>
        /// Создание нижней части втулки
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="height"></param>
        private void CreateLeg(double radius, double height)
        {
            var document = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            // Создаем эскиз окружности
            ksEntity entitySketch = DrawCircle(part, radius);
            // Выдавливаем по созданному эскизу
            ExtrudeSketch(part, entitySketch, height, true);
        }

        /// <summary>
        /// Создание отверстия
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="topHeight"></param>
        /// <param name="bottomHeight"></param>
        private void CreateHole(double radius, double topHeight, double bottomHeight)
        {
            var document = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            // Создаем эскиз окружности
            ksEntity entitySketch = DrawCircle(part, radius);
            // Создаем переменую вырезания выдавливанием
            ksEntity entityCutExtrusion = (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            // Интерфейс свойств операции вырезания выдавливанием
            ksCutExtrusionDefinition cutExtrusionDefinition = (ksCutExtrusionDefinition)entityCutExtrusion.GetDefinition();
            // Устанавливаем тип направления вырезания выдавливанием
            cutExtrusionDefinition.directionType = (short)Direction_Type.dtBoth;
            // Устанавливаем параметры вырезания выдавливанием
            cutExtrusionDefinition.SetSideParam(true, // Прямое направление
                                    (short)End_Type.etBlind,
                                    topHeight);    // Строго на глубину
            cutExtrusionDefinition.SetSideParam(false, // Обратное направление
                                    (short)End_Type.etBlind,
                                    bottomHeight); //Строго на глубину
            // Эскиз операции вырезания выдавливанием
            cutExtrusionDefinition.SetSketch(entitySketch);
            // Создаем операцию вырезания выдавливанием
            entityCutExtrusion.Create();
        }

        /// <summary>
        /// Создание отверстий по концентрической сетке
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="radius"></param>
        /// <param name="height"></param>
        /// <param name="count"></param>
        private void CreateArray(double coordinate, double radius, double height, int count)
        {
            var document = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            // Создаем новый эскиз
            ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            // Интерфейс свойств эскиза
            ksSketchDefinition sketchDefinition = (ksSketchDefinition)entitySketch.GetDefinition();
            // Получаем интерфейс базовой плоскости
            ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            // Устанавливаем плоскость базовой для эскиза
            sketchDefinition.SetPlane(basePlane);
            // Создаем эскиз
            entitySketch.Create();
            // Интерфейс редактора эскиза
            ksDocument2D sketchEdit = (ksDocument2D)sketchDefinition.BeginEdit();
            // Рисуем вспомогательную окружность
            sketchEdit.ksCircle(0, 0, coordinate, 6);
            // Рисуем основную окружность с центром в заданной координате
            sketchEdit.ksCircle(coordinate, 0, radius, 1);
            // Завершаем редактирование эскиза
            sketchDefinition.EndEdit();

            // Создаем переменую вырезания выдавливанием
            ksEntity entityCutExtrusion = (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            // Интерфейс свойств операции вырезания выдавливанием
            ksCutExtrusionDefinition cutExtrusionDefinition = (ksCutExtrusionDefinition)entityCutExtrusion.GetDefinition();
            // Устанавливаем тип направления вырезания выдавливанием
            cutExtrusionDefinition.directionType = (short)Direction_Type.dtNormal;
            // Устанавливаем параметры вырезания выдавливанием
            cutExtrusionDefinition.SetSideParam(true, // Прямое направление
                                    (short)End_Type.etBlind,
                                    height); // Строго на глубину
            // Эскиз операции вырезания выдавливанием
            cutExtrusionDefinition.SetSketch(entitySketch);
            // Создаем операцию вырезания выдавливанием
            entityCutExtrusion.Create();

            // Создаем переменную копирования по концентрическрй сетке
            ksEntity circularCopyEntity = (ksEntity)part.NewEntity((short)Obj3dType.o3d_circularCopy);
            // Интерфейс свойств операции копирования по концентрической сетке
            ksCircularCopyDefinition circularCopyDefinition = (ksCircularCopyDefinition)circularCopyEntity.GetDefinition();
            // Устанавливаем параметры копирования
            circularCopyDefinition.SetCopyParamAlongDir(count, 360.0, true, false);
            // Получаем интерфейс оси копирования
            ksEntity baseAxisOZ = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_axisOZ);
            // Устанавливаем ось копирования
            circularCopyDefinition.SetAxis(baseAxisOZ);
            // Интерфейс массива объектов модели
            ksEntityCollection entityCollection = (ksEntityCollection)circularCopyDefinition.GetOperationArray();
            entityCollection.Add(cutExtrusionDefinition);
            // Создаем операцию копирования по концентрической сетке
            circularCopyEntity.Create();
        }

        /// <summary>
        /// Метод, создающий и возвращающий эскиз окружности c заданным радиусом
        /// </summary>
        /// <param name="part"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private ksEntity DrawCircle(ksPart part, double radius)
        {
            // Создаем новый эскиз
            ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            // Интерфейс свойств эскиза
            ksSketchDefinition sketchDefinition = (ksSketchDefinition)entitySketch.GetDefinition();
            // Получаем интерфейс базовой плоскости
            ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            // Устанавливаем плоскость базовой для эскиза
            sketchDefinition.SetPlane(basePlane);
            // Создаем эскиз
            entitySketch.Create();
            // Интерфейс редактора эскиза
            ksDocument2D sketchEdit = (ksDocument2D)sketchDefinition.BeginEdit();
            // Рисуем окружность по заданному радиусу
            sketchEdit.ksCircle(0, 0, radius, 1);
            // Завершаем редактирование эскиза
            sketchDefinition.EndEdit();

            return entitySketch;
        }

        /// <summary>
        /// Метод, выполняющий выдавливание по эскизу
        /// </summary>
        /// <param name="part"></param>
        /// <param name="sketch"></param>
        /// <param name="height"></param>
        /// <param name="type"></param>
        private void ExtrudeSketch(ksPart part, ksEntity sketch, double height, bool type)
        {
            // Создаем переменную выдавливания
            ksEntity entityExtrusion = (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
            // Интерфейс свойств базовой операции выдавливания
            ksBaseExtrusionDefinition extrusionDefinition = (ksBaseExtrusionDefinition)entityExtrusion.GetDefinition();
            if (type == false)
            {
                // Устанавливаем тип направления выдавливания
                extrusionDefinition.directionType = (short)Direction_Type.dtReverse; //Обратное направление
                // Устанавливаем параметры выдавливания
                extrusionDefinition.SetSideParam(false, // Обратное направление
                                          (short)End_Type.etBlind, // Строго на глубину
                                          height); // Расстояние выдавливания
            }
            if (type == true)
            {
                // Устанавливаем тип направления выдавливания
                extrusionDefinition.directionType = (short)Direction_Type.dtNormal; //Прямое направление
                // Устанавливаем параметры выдавливания
                extrusionDefinition.SetSideParam(true, // Прямое направление
                                          (short)End_Type.etBlind, // Строго на глубину
                                          height); // Расстояние выдавливания
            }
            //Эскиз операции выдавливания
            extrusionDefinition.SetSketch(sketch);
            // Создаем операцию выдавливания
            entityExtrusion.Create();
        }
    }
}
