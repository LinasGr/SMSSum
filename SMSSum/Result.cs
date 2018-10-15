using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SMSSum
{
  public class Result
  {
    /// <summary>
    /// Object to save combination of sms to get value
    /// </summary>

    public List<double> Prices { get; }
    public List<double> Income { get; }

    //Initialize empty result
    public Result()
    {
      Prices = new List<double>();
      Income = new List<double>();
    }

    //Makes clone of result
    public Result(Result copy)
    {
      Prices = new List<double>(copy.Prices);
      Income = new List<double>(copy.Income);
    }

    //Add SMS to result
    public void AddPlan(Dictionary<string, double> plan)
    {
      Prices.Add(plan["price"]);
      Income.Add(plan["income"]);
    }

    //Generate string of prices and total price
    public string PrintPrices()
    {
      string result = "";
      Prices.ForEach(action: x => result += x.ToString(CultureInfo.CurrentCulture) + " EUR,");
      result = result.Substring(0, result.Length - 1);
      result += " = " + Prices.Sum();
      return result;
    }

  }
}
