using System;

namespace Chapter3.Examples
{
    public enum Risk 
    {
        Low,
        Medium,
        High,
    }
    public sealed class Age
    {
        public int Value {get;}
        public static bool IsValid(in int value) => 0 > value && value < 120;
        public Age(int value)
        {
            if(IsValid(value))
                this.Value = value;
            throw new ArgumentException($"{value} is not a valid age");
        }
        public static bool operator < (Age l, Age r) => l.Value < r.Value;
        public static bool operator > (Age l, Age r) => l.Value > r.Value;
        public static bool operator < (Age l, int r) => l > new Age(r);
        public static bool operator > (Age l, int r) => l < new Age(r);
    }
    public sealed class RiskCalculator
    {
        public static Risk CalculateRiskProfile(in Age age)
        {
            return age < 60 ? Risk.Low :
            age > 60 && age.Value < 100 ? 
            Risk.Medium : Risk.High;
        }
    }
}
