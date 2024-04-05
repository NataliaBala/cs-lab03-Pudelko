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
            public override string ToString()
        {
            return string.Format($"{A:F3} m × {B:F3} m × {C:F3} m");
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (formatProvider == null)
            {
                _ = CultureInfo.CurrentCulture;
            }
            if (format == null)
            {
                format = "m";
            }

            switch (format)
            {
                case "m":
                    return ToString();
                case "cm":
                    X = Math.Round(A * 100, 1);
                    Y = Math.Round(B * 100, 1);
                    Z = Math.Round(C * 100, 1);
                    return string.Format($"{X:F1} {format} × {Y:F1} {format} × {Z:F1} {format}");
                case "mm":
                    X = Math.Round(A * 1000);
                    Y = Math.Round(B * 1000);
                    Z = Math.Round(C * 1000);
                    return $"{X} {format} × {Y} {format} × {Z} {format}";
                default:
                    throw new FormatException();
            }
        }

        public bool Equals(Pudelko pud)
        {
            if (pud == null)
            {
                return false;
            }
            if (this.Objetosc == pud.Objetosc)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Pudelko Obj = obj as Pudelko;
            if (Obj == null)
            {
                return false;
            }
            else
            {
                return Equals(Obj);
            }
        }

    }
}