using System;

namespace PudelkoLib
{
    public enum UnitOfMeasure
    {
        milimeter,
        centimeter,
        meter
    }

    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable //sealed - zapieczętowanie klasy 
    {
        private readonly double a;
        private readonly double b;
        private readonly double c;

#pragma warning disable IDE0052 // Usuń nieodczytywane składowe prywatne
        private readonly UnitOfMeasure unit;
#pragma warning restore IDE0052 // Usuń nieodczytywane składowe prywatne

        public double Pole => Math.Round(2 * (a * b + a * c + b * c), 6);
        public double Objetosc => Math.Round(a * b * c, 9);
        public double Bok => a + b + c;
        public double A { get => a; }
        public double B { get => b; }
        public double C { get => c; }

        public double X = 0.1;
        public double Y = 0.1;
        public double Z = 0.1;

        public double LastN(double x)
        {
            return Math.Floor(x * 1000) / 1000;
        }

        public double Change(double a, UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.meter:
                    break;
                case UnitOfMeasure.centimeter:
                    a *= 0.01;
                    break;
                case UnitOfMeasure.milimeter:
                    a *= 0.001;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            double x = LastN(a);
            if (x <= 0 || a > 10)
            {
                throw new ArgumentOutOfRangeException();
            }
            return a;
        }

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            var check = unit switch
            {
                UnitOfMeasure.meter => 0.1,
                UnitOfMeasure.centimeter => 10,
                UnitOfMeasure.milimeter => 100,
                _ => throw new ArgumentOutOfRangeException(),
            };
            this.unit = unit;
            this.a = Change((a == null) ? check : (double)a, unit);
            this.b = Change((b == null) ? check : (double)b, unit);
            this.c = Change((c == null) ? check : (double)c, unit);
        }
    }
}