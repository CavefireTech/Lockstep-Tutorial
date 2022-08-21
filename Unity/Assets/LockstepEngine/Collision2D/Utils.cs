using System;
using System.Collections.Generic;
using Lockstep.Math;
using static Lockstep.Math.FMath;

namespace Lockstep.Collision2D {
    public enum ECollisionEvent {
        Enter,
        Stay,
        Exit
    }

    public enum EShape2D {
        Segment,
        Ray,
        Circle,
        AABB,
        OBB,
        Polygon,
        EnumCount,
    }

    public struct CollisionResult {
        public bool Collides;
        public FVector3 Normal;
        public FP Penetration;
        public ECollisionEvent Type;
        public bool First;
    }

    public  static unsafe partial class Utils {
      
        //TODO
        public static bool TestPolygonPolygon(FVector2* _points, int vertexCount, FVector2* _points2, int vertexCount2){
            return false;
        }




        //http://www.kevlindev.com/geometry/2D/intersections/index.htm
        //http://www.kevlindev.com/geometry/2D/intersections/index.htm
        //https://bitlush.com/blog/circle-vs-polygon-collision-detection-in-c-sharp
        public static bool TestCirclePolygon(FVector2 c, FP r, FVector2* _points, int vertexCount){
            var radiusSquared = r * r;
            var circleCenter = c;
            var nearestDistance = FP.MaxValue;
            int nearestVertex = -1;

            for (var i = 0; i < vertexCount; i++) {
                FVector2 axis = circleCenter - _points[i];
                var distance = axis.sqrMagnitude - radiusSquared;
                if (distance <= 0) {
                    return true;
                }

                if (distance < nearestDistance) {
                    nearestVertex = i;
                    nearestDistance = distance;
                }
            }

            FVector2 GetPoint(int index){
                if (index < 0) {
                    index += vertexCount;
                }
                else if (index >= vertexCount) {
                    index -= vertexCount;
                }

                return _points[index];
            }

            var vertex = GetPoint(nearestVertex - 1);
            for (var i = 0; i < 2; i++) {
                var nextVertex = GetPoint(nearestVertex + i);
                var edge = nextVertex - vertex;
                var edgeLengthSquared = edge.sqrMagnitude;
                if (edgeLengthSquared != 0) {
                    FVector2 axis = circleCenter - vertex;
                    var dot = FVector2.Dot(edge, axis);
                    if (dot >= 0 && dot <= edgeLengthSquared) {
                        FVector2 projection = vertex + (dot / edgeLengthSquared) * edge;
                        axis = projection - circleCenter;
                        if (axis.sqrMagnitude <= radiusSquared) {
                            return true;
                        }
                        else {
                            if (edge.x > 0) {
                                if (axis.y > 0) {
                                    return false;
                                }
                            }
                            else if (edge.x < 0) {
                                if (axis.y < 0) {
                                    return false;
                                }
                            }
                            else if (edge.y > 0) {
                                if (axis.x < 0) {
                                    return false;
                                }
                            }
                            else {
                                if (axis.x > 0) {
                                    return false;
                                }
                            }
                        }
                    }
                }

                vertex = nextVertex;
            }

            return true;
        }

        //https://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect  
        public static bool TestRayPolygon(FVector2 o, FVector2 dir, FVector2* points, int vertexCount){
            for (var i = 0; i < vertexCount; i++) {
                var b1 = points[i];
                var b2 = points[(i + 1) % vertexCount];
                var inter = TestRaySegment(o, dir, b1, b2);
                if (inter >= 0) {
                    return true;
                }
            }

            return false;
        }

        public static bool TestRayPolygon(FVector2 o, FVector2 dir, FVector2* points, int vertexCount,
            ref FVector2 point){
            FP t = FP.FLT_MAX;
            for (var i = 0; i < vertexCount; i++) {
                var b1 = points[i];
                var b2 = points[(i + 1) % vertexCount];
                var inter = TestRaySegment(o, dir, b1, b2);
                if (inter >= 0) {
                    if (inter < t) {
                        t = inter;
                    }
                }
            }

            if (t < FP.FLT_MAX) {
                point = o + dir * t;
            }

            return false;
        }


        public static FP TestRaySegment(FVector2 o, FVector2 d1, FVector2 p2, FVector2 p3){
            var diff = p2 - o;
            var d2 = p3 - p2;

            var demo = Cross2D(d1, d2); //det
            if (FMath.Abs(demo) < FP.EPSILON) //parallel
                return FP.negOne;

            var t1 = Cross2D(d2, diff) / demo; // Cross2D(diff,-d2)
            var t2 = Cross2D(d1, diff) / demo; //Dot(v1,pd0) == cross(d0,d1)

            if (t1 >= 0 && (t2 >= 0 && t2 <= 1))
                return t1;
            return FP.negOne;
        }

        public static FP TestSegmentSegment(FVector2 p0, FVector2 p1, FVector2 p2, FVector2 p3){
            var diff = p2 - p0;
            var d1 = p1 - p0;
            var d2 = p3 - p2;


            var demo = Cross2D(d1, d2); //det
            if (FMath.Abs(demo) < FP.EPSILON) //parallel
                return FP.negOne;

            var t1 = Cross2D(d2, diff) / demo; // Cross2D(diff,-d2)
            var t2 = Cross2D(d1, diff) / demo; //Dot(v1,pd0) == cross(d0,d1)

            if ((t1 >= 0 && t1 <= 1) && (t2 >= 0 && t2 <= 1))
                return t1; // return p0 + (p1-p0) * t1
            return FP.negOne;
        }
        //http://geomalgorithms.com/

//https://stackoverflow.com/questions/1073336/circle-line-segment-collision-detection-algorithm
        public static bool TestRayCircle(FVector2 cPos, FP cR, FVector2 rB, FVector2 rDir, ref FP t){
            var d = rDir;
            var f = rB - cPos;
            var a = FVector2.Dot(d, d);
            var b = 2 * FVector2.Dot(f, d);
            var c = FVector2.Dot(f, f) - cR * cR;
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0) {
                // no intersection
                return false;
            }
            else {
                discriminant = FMath.Sqrt(discriminant);
                var t1 = (-b - discriminant) / (2 * a);
                var t2 = (-b + discriminant) / (2 * a);
                if (t1 >= 0) {
                    t = t1;
                    return true;
                }

                if (t2 >= 0) {
                    t = t2;
                    return true;
                }

                return false;
            }
        }

        public static bool TestRayOBB(FVector2 o, FVector2 d, FVector2 c, FVector2 size, FP deg,
            out FP tmin){
            var fo = o - c;
            fo = fo.Rotate(deg);
            var fd = d.Rotate(deg);
            return TestRayAABB(fo, fd, -size, size, out tmin);
        }

        public static bool TestRayAABB(FVector2 o, FVector2 d, FVector2 min, FVector2 max, out FP tmin){
            tmin = FP.zero; // set to -FLT_MAX to get first hit on line
            FP tmax = FP.FLT_MAX; // set to max distance ray can travel (for segment)

            // For all three slabs
            for (int i = 0; i < 2; i++) {
                if (Abs(d[i]) < FP.EPSILON) {
                    // Ray is parallel to slab. No hit if origin not within slab
                    if (o[i] < min[i] || o[i] > max[i]) return false;
                }
                else {
                    // Compute intersection t value of ray with near and far plane of slab
                    FP ood = FP.one / d[i];
                    FP t1 = (min[i] - o[i]) * ood;
                    FP t2 = (max[i] - o[i]) * ood;
                    // Make t1 be intersection with near plane, t2 with far plane
                    if (t1 > t2) {
                        var temp = t1;
                        t1 = t2;
                        t2 = temp;
                    }

                    // Compute the intersection of slab intersections intervals
                    tmin = Max(tmin, t1);
                    tmax = Min(tmax, t2);
                    // Exit with no collision as soon as slab intersection becomes empty
                    if (tmin > tmax) return false;
                }
            }

            return true;
        }

        public static bool TestCircleOBB(FVector2 posA, FP rA, FVector2 posB, FP rB, FVector2 sizeB,
            FVector2 up){
            var diff = posA - posB;
            var allRadius = rA + rB;
//circle 判定CollisionHelper
            if (diff.sqrMagnitude > allRadius * allRadius) {
                return false;
            }

//空间转换
            var absX = FMath.Abs(FVector2.Dot(diff, new FVector2(up.y, -up.x)));
            var absY = FMath.Abs(FVector2.Dot(diff, up));
            var size = sizeB;
            var radius = rA;
            var x = FMath.Max(absX - size.x, FP.zero);
            var y = FMath.Max(absY - size.y, FP.zero);
            return x * x + y * y < radius * radius;
        }

        public static bool TestAABBOBB(FVector2 posA, FP rA, FVector2 sizeA, FVector2 posB, FP rB,
            FVector2 sizeB,
            FVector2 upB){
            var diff = posA - posB;
            var allRadius = rA + rB;
//circle 判定
            if (diff.sqrMagnitude > allRadius * allRadius) {
                return false;
            }

            var absUPX = FMath.Abs(upB.x); //abs(up dot aabb.right)
            var absUPY = FMath.Abs(upB.y); //abs(right dot aabb.right)
            {
//轴 投影 AABBx
                var distX = absUPX * sizeB.y + absUPY * sizeB.x;
                if (FMath.Abs(diff.x) > distX + sizeA.x) {
                    return false;
                }

//轴 投影 AABBy
//absUPX is abs(right dot aabb.up)
//absUPY is abs(up dot aabb.up)
                var distY = absUPY * sizeB.y + absUPX * sizeB.x;
                if (FMath.Abs(diff.y) > distY + sizeA.y) {
                    return false;
                }
            }
            {
                var right = new FVector2(upB.y, -upB.x);
                var diffPObbX = FVector2.Dot(diff, right);
                var diffPObbY = FVector2.Dot(diff, upB);

//absUPX is abs(aabb.up dot right )
//absUPY is abs(aabb.right dot right)
//轴 投影 OBBx
                var distX = absUPX * sizeA.y + absUPY * sizeA.x;
                if (FMath.Abs(diffPObbX) > distX + sizeB.x) {
                    return false;
                }

//absUPX is abs(aabb.right dot up )
//absUPY is abs(aabb.up dot up)
//轴 投影 OBBy
                var distY = absUPY * sizeA.y + absUPX * sizeA.x;
                if (FMath.Abs(diffPObbY) > distY + sizeB.y) {
                    return false;
                }
            }
            return true;
        }

        public static bool TestOBBOBB(FVector2 posA, FP rA, FVector2 sizeA, FVector2 upA, FVector2 posB,
            FP rB,
            FVector2 sizeB,
            FVector2 upB){
            var diff = posA - posB;
            var allRadius = rA + rB;
//circle 判定
            if (diff.sqrMagnitude > allRadius * allRadius) {
                return false;
            }

            var rightA = new FVector2(upA.y, -upA.x);
            var rightB = new FVector2(upB.y, -upB.x);
            {
//轴投影到 A.right
                var BuProjAr = FMath.Abs(FVector2.Dot(upB, rightA));
                var BrProjAr = FMath.Abs(FVector2.Dot(rightB, rightA));
                var DiffProjAr = FMath.Abs(FVector2.Dot(diff, rightA));
                var distX = BuProjAr * sizeB.y + BrProjAr * sizeB.x;
                if (DiffProjAr > distX + sizeA.x) {
                    return false;
                }

//轴投影到 A.up
                var BuProjAu = FMath.Abs(FVector2.Dot(upB, upA));
                var BrProjAu = FMath.Abs(FVector2.Dot(rightB, upA));
                var DiffProjAu = FMath.Abs(FVector2.Dot(diff, upA));
                var distY = BuProjAu * sizeB.y + BrProjAu * sizeB.x;
                if (DiffProjAu > distY + sizeA.y) {
                    return false;
                }
            }
            {
//轴投影到 B.right
                var AuProjBr = FMath.Abs(FVector2.Dot(upA, rightB));
                var ArProjBr = FMath.Abs(FVector2.Dot(rightA, rightB));
                var DiffProjBr = FMath.Abs(FVector2.Dot(diff, rightB));
                var distX = AuProjBr * sizeA.y + ArProjBr * sizeA.x;
                if (DiffProjBr > distX + sizeB.x) {
                    return false;
                }

//轴投影到 B.right
                var AuProjBu = FMath.Abs(FVector2.Dot(upA, upB));
                var ArProjBu = FMath.Abs(FVector2.Dot(rightA, upB));
                var DiffProjBu = FMath.Abs(FVector2.Dot(diff, upB));
                var distY = AuProjBu * sizeA.y + ArProjBu * sizeA.x;
                if (DiffProjBu > distY + sizeB.x) {
                    return false;
                }
            }
            return true;
        }

        public static bool TestCircleCircle(FVector2 posA, FP rA, FVector2 posB, FP rB){
            var diff = posA - posB;
            var allRadius = rA + rB;
            return diff.sqrMagnitude <= allRadius * allRadius;
        }

        public static bool TestCircleAABB(FVector2 posA, FP rA, FVector2 posB, FP rB, FVector2 sizeB){
            var diff = posA - posB;
            var allRadius = rA + rB;
//circle 判定
            if (diff.sqrMagnitude > allRadius * allRadius) {
                return false;
            }

            var absX = FMath.Abs(diff.x);
            var absY = FMath.Abs(diff.y);

//AABB & circle
            var size = sizeB;
            var radius = rA;
            var x = FMath.Max(absX - size.x, FP.zero);
            var y = FMath.Max(absY - size.y, FP.zero);
            return x * x + y * y < radius * radius;
        }

        public static bool TestAABBAABB(FVector2 posA, FP rA, FVector2 sizeA, FVector2 posB, FP rB,
            FVector2 sizeB){
            var diff = posA - posB;
            var allRadius = rA + rB;
//circle 判定
            if (diff.sqrMagnitude > allRadius * allRadius) {
                return false;
            }

            var absX = FMath.Abs(diff.x);
            var absY = FMath.Abs(diff.y);

//AABB and AABB
            var allSize = sizeA + sizeB;
            if (absX > allSize.x) return false;
            if (absY > allSize.y) return false;
            return true;
        }

        /// <summary>
        /// 判定两线段是否相交 并求交点
        /// https://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect/565282#
        /// </summary>
        public static bool IntersectSegment(ref FVector2 seg1Src, ref FVector2 seg1Vec, ref FVector2 seg2Src,
            ref FVector2 seg2Vec, out FVector2 interPoint){
            interPoint = FVector2.zero;
            long denom = (long) seg1Vec._x * seg2Vec._y - (long) seg2Vec._x * seg1Vec._y; //sacle 1000
            if (denom == 0L)
                return false; // Collinear
            bool denomPositive = denom > 0L;
            var s02_x = seg1Src._x - seg2Src._x;
            var s02_y = seg1Src._y - seg2Src._y;
            long s_numer = (long) seg1Vec._x * s02_y - (long) seg1Vec._y * s02_x; //scale 1000
            if ((s_numer < 0L) == denomPositive)
                return false; // No collision
            long t_numer = seg2Vec._x * s02_y - seg2Vec._y * s02_x; //scale 1000
            if ((t_numer < 0L) == denomPositive)
                return false; // No collision
            if (((s_numer > denom) == denomPositive) || ((t_numer > denom) == denomPositive))
                return false; // No collision
// Collision detected
            var t = t_numer * 1000 / denom; //sacle 1000
            interPoint._x = (int) (seg1Src._x + ((long) ((t * seg1Vec._x)) / 1000));
            interPoint._y = (int) (seg1Src._y + ((long) ((t * seg1Vec._y)) / 1000));
            return true;
        }

        /// <summary>
        ///  判定点是否在多边形内
        /// https://stackoverflow.com/questions/217578/how-can-i-determine-whether-a-2d-point-is-within-a-polygon
        /// </summary>
        public static bool IsPointInPolygon(FVector2 p, FVector2[] polygon){
            var minX = polygon[0]._x;
            var maxX = polygon[0]._x;
            var minY = polygon[0]._y;
            var maxY = polygon[0]._y;
            for (int i = 1; i < polygon.Length; i++) {
                FVector2 q = polygon[i];
                minX = FMath.Min(q._x, minX);
                maxX = FMath.Max(q._x, maxX);
                minY = FMath.Min(q._y, minY);
                maxY = FMath.Max(q._y, maxY);
            }

            if (p._x < minX || p._x > maxX || p._y < minY || p._y > maxY) {
                return false;
            }

// http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
            bool inside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++) {
                if ((polygon[i]._y > p._y) != (polygon[j]._y > p._y) &&
                    p._x < (polygon[j]._x - polygon[i]._x) * (p._y - polygon[i]._y) /
                    (polygon[j]._y - polygon[i]._y) +
                    polygon[i]._x) {
                    inside = !inside;
                }
            }

            return inside;
        }
    }
}