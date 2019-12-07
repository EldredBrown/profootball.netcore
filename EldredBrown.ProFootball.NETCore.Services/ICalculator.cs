using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public interface ICalculator
    {
        double Add(double lVal, double rVal);
        decimal? CalculatePythagoreanWinningPercentage(SeasonTeam seasonTeam);
        decimal? CalculateWinningPercentage(SeasonTeam seasonTeam);
        decimal? Divide(decimal numerator, decimal denominator);
        double Multiply(double lVal, double rVal);
        double Subtract(double lVal, double rVal);
    }
}
