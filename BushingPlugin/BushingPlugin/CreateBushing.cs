using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BushingParametrs;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;

namespace BushingPlugin
{
    /// <summary>
    /// Класс, предназначенный для поэтапного
    /// создания втулки
    /// </summary>
    public class CreateBushing
    {
        /// <summary>
        /// Объект Компас API
        /// </summary>
        private KompasObject _kompas;

        /// <summary>
        /// Параметризированный конструктор
        /// </summary>
        /// <param name="kompas">Объект Компас API</param>
        public CreateBushing(KompasObject kompas)
        {
            _kompas = kompas;
            var document = (ksDocument3D)kompas.Document3D();
            document.Create();
        }

        /// <summary>
        /// Построение детали по заданным параметрам
        /// </summary>
        /// <param name="bushing">Объект втулки</param>
        public void CreateDetail(Bushing bushing)
        {
            double radiusCap = bushing.TopDiametr / 2;
            double heightCap = bushing.TopLength;
            double locationRadius = bushing.LocationDiametr / 2;
            double radiusArray = bushing.HolesDiametr / 2;
            int numberHoles = bushing.NumberHoles;
            double radiusLeg = bushing.OuterDiametr / 2;
            double heightLeg = bushing.TotalLength - bushing.TopLength;
            double radiusHole = bushing.InnerDiametr / 2;
            string engravingText = bushing.EngravingText;

            CreateTop(radiusCap, heightCap);
            CreateArray(locationRadius, radiusArray, heightCap, numberHoles);
            CreateBottom(radiusLeg, heightLeg);
            CreateHole(radiusHole, heightCap, heightLeg);

            if (bushing.PresenceEngraving)
            {
                CreateEngraving(heightLeg, radiusLeg, radiusHole, 
                    engravingText);
            }
        }

        /// <summary>
        /// Создание верхней части втулки
        /// </summary>
        /// <param name="radius">Радиус верхней части втулки</param>
        /// <param name="height">Высота выдавливания</param>
        private void CreateTop(double radius, double height)
        {
            var document = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            ksEntity entitySketch = DrawCircle(part, radius);
            ExtrudeSketch(part, entitySketch, height, false);
        }

        /// <summary>
        /// Создание нижней части втулки
        /// </summary>
        /// <param name="radius">Радиус нижней части втулки</param>
        /// <param name="height">Высота выдавливания</param>
        private void CreateBottom(double radius, double height)
        {
            var document = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            ksEntity entitySketch = DrawCircle(part, radius);
            ExtrudeSketch(part, entitySketch, height, true);
        }

        /// <summary>
        /// Создание отверстия
        /// </summary>
        /// <param name="radius">Радиус отверстия</param>
        /// <param name="topHeight">Высота вырезания вверх</param>
        /// <param name="bottomHeight">Высота вырезания вниз</param>
        private void CreateHole(double radius, double topHeight, 
            double bottomHeight)
        {
            var document = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            ksEntity entitySketch = DrawCircle(part, radius);
            CutExtrusion(part, entitySketch, topHeight, bottomHeight, true);
        }

        /// <summary>
        /// Создание отверстий по концентрической сетке
        /// </summary>
        /// <param name="coordinate">Координата расположения</param>
        /// <param name="radius">Радиус отверстия</param>
        /// <param name="height">Высота вырезания</param>
        /// <param name="count">Количество отверстий</param>
        private void CreateArray(double coordinate, double radius,
            double height, int count)
        {
            var document = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            const int coordinateX = 0;
            const int coordinateY = 0;
            const int styleLineBase = 1;
            const int styleLineAuxiliary = 6;
            const int stepCopy = 360;

            ksEntity entitySketch = (ksEntity)part.NewEntity((short)
                Obj3dType.o3d_sketch);
            ksSketchDefinition sketchDefinition = (ksSketchDefinition)
                entitySketch.GetDefinition();
            ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)
                Obj3dType.o3d_planeXOY);
            sketchDefinition.SetPlane(basePlane);
            entitySketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)sketchDefinition.
                BeginEdit();
            sketchEdit.ksCircle(coordinateX, coordinateY, coordinate, 
                styleLineAuxiliary);
            sketchEdit.ksCircle(coordinate, coordinateY, radius, 
                styleLineBase);
            sketchDefinition.EndEdit();

            ksEntity entityCutExtrusion = (ksEntity)part.NewEntity((short)
                Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition cutExtrusionDefinition =
                (ksCutExtrusionDefinition)entityCutExtrusion.GetDefinition();
            cutExtrusionDefinition.directionType = 
                (short)Direction_Type.dtNormal;
            cutExtrusionDefinition.SetSideParam(true, 
                (short)End_Type.etBlind, height);
            cutExtrusionDefinition.SetSketch(entitySketch);
            entityCutExtrusion.Create();

            ksEntity circularCopyEntity = (ksEntity)part.NewEntity((short)
                Obj3dType.o3d_circularCopy);
            ksCircularCopyDefinition circularCopyDefinition =
                (ksCircularCopyDefinition)circularCopyEntity.GetDefinition();
            circularCopyDefinition.SetCopyParamAlongDir(count, stepCopy, 
                true, false);
            ksEntity baseAxisOZ = (ksEntity)part.GetDefaultEntity((short)
                Obj3dType.o3d_axisOZ);
            circularCopyDefinition.SetAxis(baseAxisOZ);
            ksEntityCollection entityCollection = (ksEntityCollection)
                circularCopyDefinition.GetOperationArray();
            entityCollection.Add(cutExtrusionDefinition);
            circularCopyEntity.Create();
        }

        /// <summary>
        /// Метод, создающий и возвращающий эскиз окружности c заданным 
        /// радиусом
        /// </summary>
        /// <param name="part">Интерфейс компонента</param>
        /// <param name="radius">Радиус окружности</param>
        /// <returns></returns>
        private ksEntity DrawCircle(ksPart part, double radius)
        {
            const int coordinateX = 0;
            const int coordinateY = 0;
            const int styleLine = 1;

            ksEntity entitySketch = (ksEntity)part.NewEntity((short)
                Obj3dType.o3d_sketch);
            ksSketchDefinition sketchDefinition = (ksSketchDefinition)
                entitySketch.GetDefinition();
            ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)
                Obj3dType.o3d_planeXOY);
            sketchDefinition.SetPlane(basePlane);
            entitySketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)sketchDefinition.
                BeginEdit();
            sketchEdit.ksCircle(coordinateX, coordinateY, radius, styleLine);
            sketchDefinition.EndEdit();

            return entitySketch;
        }

        /// <summary>
        /// Метод, выполняющий выдавливание по эскизу
        /// </summary>
        /// <param name="part">Интерфейс компонента</param>
        /// <param name="sketch">Эскиз</param>
        /// <param name="height">Высота выдавливания</param>
        /// <param name="type">Тип выдавливания</param>
        private void ExtrudeSketch(ksPart part, ksEntity sketch, 
            double height, bool type)
        {
            ksEntity entityExtrusion = (ksEntity)part.NewEntity((short)
                Obj3dType.o3d_baseExtrusion);
            ksBaseExtrusionDefinition extrusionDefinition = 
                (ksBaseExtrusionDefinition)entityExtrusion.GetDefinition();
            if (type == false)
            {
                extrusionDefinition.directionType = (short)Direction_Type.
                    dtReverse;
                extrusionDefinition.SetSideParam(false, 
                    (short)End_Type.etBlind, height);
            }
            if (type == true)
            {
                extrusionDefinition.directionType = (short)Direction_Type.
                    dtNormal;
                extrusionDefinition.SetSideParam(true, 
                    (short)End_Type.etBlind, height);
            }
            extrusionDefinition.SetSketch(sketch);
            entityExtrusion.Create();
        }

        /// <summary>
        /// Создание гравировки текста на детали
        /// </summary>
        /// <param name="length">Длина нижней части втулки</param>
        /// <param name="outerRadius">Внешний радиус втулки</param>
        /// <param name="innerRadius">Внутренний радус втулки</param>
        private void CreateEngraving(double length, double outerRadius,
            double innerRadius, string engravingText)
        {
            var document = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            double coordinateY = ((outerRadius + innerRadius) / 2) - 1.5;
            const int coordinateX = 0;
            const int angle = 0;
            const double characterHeight = 2;
            const int textNarrowing = 1;
            const int bitVector = 0;
            const int align = 1;
            const double topHeight = 1;
            const double bottomHeight = 0;

            ksEntity entityPlaneOffset = (ksEntity)part.NewEntity((short)
                Obj3dType.o3d_planeOffset);
            ksEntity entityPlaneXOY = (ksEntity)part.GetDefaultEntity((short)
                Obj3dType.o3d_planeXOY);
            ksPlaneOffsetDefinition planeOffsetDefinition = 
                (ksPlaneOffsetDefinition)entityPlaneOffset.GetDefinition();
            planeOffsetDefinition.SetPlane(entityPlaneXOY);
            planeOffsetDefinition.direction = true;
            planeOffsetDefinition.offset = length;
            entityPlaneOffset.Create();

            ksEntity entitySketch = (ksEntity)part.NewEntity((short)
                Obj3dType.o3d_sketch);
            ksSketchDefinition sketchDefinition = (ksSketchDefinition)
                entitySketch.GetDefinition();
            sketchDefinition.SetPlane(entityPlaneOffset);
            entitySketch.Create();

            ksDocument2D sketchEdit = (ksDocument2D)sketchDefinition.
                BeginEdit();
            int text = sketchEdit.ksText(coordinateX, coordinateY, angle, 
                characterHeight, textNarrowing, bitVector, engravingText);
            sketchEdit.ksSetTextAlign(text, align);
            sketchDefinition.EndEdit();

            CutExtrusion(part, entitySketch, topHeight, bottomHeight, false);
        }

        /// <summary>
        /// Метод, выполняющий вырезание выдавливанием по эскизу
        /// </summary>
        /// <param name="part">Интерфейс компонента</param>
        /// <param name="sketch">Эскиз</param>
        /// <param name="topHeight">Высота вырезания вверх</param>
        /// <param name="bottomHeight">Высота вырезания вниз</param>
        /// <param name="type">Тип выдавливания</param>
        private void CutExtrusion(ksPart part, ksEntity sketch,
            double topHeight, double bottomHeight, bool type)
        {
            ksEntity entityCutExtrusion = (ksEntity)part.NewEntity((short)
                Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition cutExtrusionDefinition = 
                (ksCutExtrusionDefinition)entityCutExtrusion.GetDefinition();
            if (type == false)
            {
                cutExtrusionDefinition.directionType = (short)Direction_Type.
                    dtNormal;
                cutExtrusionDefinition.SetSideParam(true, (short)End_Type.
                    etBlind, topHeight);
            }
            if (type == true)
            {
                cutExtrusionDefinition.directionType = (short)Direction_Type.
                    dtBoth;
                cutExtrusionDefinition.SetSideParam(true, (short)End_Type.
                    etBlind, topHeight);
                cutExtrusionDefinition.SetSideParam(false, (short)End_Type.
                    etBlind, bottomHeight);
            }
            cutExtrusionDefinition.SetSketch(sketch);
            entityCutExtrusion.Create();
        }
    }
}
