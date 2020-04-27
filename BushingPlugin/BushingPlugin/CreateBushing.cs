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
            ksEntity entitySketch = DrawCircle(part, radius);
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
            ksEntity entitySketch = DrawCircle(part, radius);
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
            ksEntity entitySketch = DrawCircle(part, radius);
            ksEntity entityCutExtrusion = (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition cutExtrusionDefinition = (ksCutExtrusionDefinition)entityCutExtrusion.GetDefinition();
            cutExtrusionDefinition.directionType = (short)Direction_Type.dtBoth;
            cutExtrusionDefinition.SetSideParam(true, (short)End_Type.etBlind, topHeight);
            cutExtrusionDefinition.SetSideParam(false, (short)End_Type.etBlind, bottomHeight);
            cutExtrusionDefinition.SetSketch(entitySketch);
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
            const int xc = 0;
            const int yc = 0;
            const int styleLineBase = 1;
            const int styleLineAuxiliary = 6;
            const int stepCopy = 360;

            ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition sketchDefinition = (ksSketchDefinition)entitySketch.GetDefinition();
            ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            sketchDefinition.SetPlane(basePlane);
            entitySketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)sketchDefinition.BeginEdit();
            sketchEdit.ksCircle(xc, yc, coordinate, styleLineAuxiliary);
            sketchEdit.ksCircle(coordinate, yc, radius, styleLineBase);
            sketchDefinition.EndEdit();

            ksEntity entityCutExtrusion = (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition cutExtrusionDefinition = (ksCutExtrusionDefinition)entityCutExtrusion.GetDefinition();
            cutExtrusionDefinition.directionType = (short)Direction_Type.dtNormal;
            cutExtrusionDefinition.SetSideParam(true, (short)End_Type.etBlind, height);
            cutExtrusionDefinition.SetSketch(entitySketch);
            entityCutExtrusion.Create();

            ksEntity circularCopyEntity = (ksEntity)part.NewEntity((short)Obj3dType.o3d_circularCopy);
            ksCircularCopyDefinition circularCopyDefinition = (ksCircularCopyDefinition)circularCopyEntity.GetDefinition();
            circularCopyDefinition.SetCopyParamAlongDir(count, stepCopy, true, false);
            ksEntity baseAxisOZ = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_axisOZ);
            circularCopyDefinition.SetAxis(baseAxisOZ);
            ksEntityCollection entityCollection = (ksEntityCollection)circularCopyDefinition.GetOperationArray();
            entityCollection.Add(cutExtrusionDefinition);
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
            const int xc = 0;
            const int yc = 0;
            const int styleLine = 1;

            ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition sketchDefinition = (ksSketchDefinition)entitySketch.GetDefinition();
            ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            sketchDefinition.SetPlane(basePlane);
            entitySketch.Create();
            ksDocument2D sketchEdit = (ksDocument2D)sketchDefinition.BeginEdit();
            sketchEdit.ksCircle(xc, yc, radius, styleLine);
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
            ksEntity entityExtrusion = (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
            ksBaseExtrusionDefinition extrusionDefinition = (ksBaseExtrusionDefinition)entityExtrusion.GetDefinition();
            if (type == false)
            {
                extrusionDefinition.directionType = (short)Direction_Type.dtReverse;
                extrusionDefinition.SetSideParam(false, (short)End_Type.etBlind, height);
            }
            if (type == true)
            {
                extrusionDefinition.directionType = (short)Direction_Type.dtNormal;
                extrusionDefinition.SetSideParam(true, (short)End_Type.etBlind, height);
            }
            extrusionDefinition.SetSketch(sketch);
            entityExtrusion.Create();
        }
    }
}
