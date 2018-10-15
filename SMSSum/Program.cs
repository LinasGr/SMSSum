using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace SMSSum
{
  /// <summary>
  /// There is given sms plans and value to get by sending sms
  /// Finde cheepest for custumer combination of sms to vet required value
  /// If there is several combinations, find with least sms
  /// </summary>

  class Program
  {
    //Best result
    public static Result FinalResult;

    //List of sms plans
    public static List<Dictionary<string, double>> SmsPlans;

    //Max messages per result.
    //0 - unlimited
    private static int _maxMessages;

    //Minimum sum to get per result
    private static double _requiredIncome;

    //Read Json file to get data
    private static bool ReadJson(string path)
    {
      string str;
      if (File.Exists(path)) str = File.ReadAllText(path);
      else return false;
      var smsPlanJson = JsonConvert.DeserializeObject<SmsPlanJson>(str);
      if (smsPlanJson.sms_list.Count==0) return false;
      _requiredIncome = smsPlanJson.required_income;
      _maxMessages = smsPlanJson.max_messages;
      SmsPlans = smsPlanJson.sms_list;
      return true;
    }

    public static void Recursion(double val, Result tmpResult)
    {
      foreach (var plan in SmsPlans)
      {
        //Make copy of working result
        var tmpResultCopy = new Result(tmpResult);

        //Add current plan to copy of result
        tmpResultCopy.AddPlan(plan);

        //If sms count reached but value not, stop this recursion
        if (tmpResultCopy.Income.Count > _maxMessages
            && _maxMessages > 0
            && tmpResultCopy.Income.Sum() < val) return;

        //Got enough sms income 
        if (tmpResultCopy.Income.Sum() >= val)
        {

          //SAVE if current result is better or its first result
          //New result cheaper
          //Or its first result
          if (tmpResultCopy.Prices.Sum() < FinalResult.Prices.Sum()
              || FinalResult.Prices.Count == 0)
          {
            //Save copy of working result to main result
            FinalResult = tmpResultCopy;
          }
          //In case current result sum = main result sum
          // And takes less sms
          else if (tmpResultCopy.Prices.Sum().Equals(FinalResult.Prices.Sum())
                   && tmpResultCopy.Prices.Count < FinalResult.Prices.Count)
          {
            //Save copy of working result to main result
            FinalResult = tmpResultCopy;
          }
        }
        //If got not enough sms value go deeper to recursion
        else
        {
          Recursion(val, tmpResultCopy);
        }
      }
    }


    static void Main(string[] args)
    {
      if (args.Length == 0 || !File.Exists(args[0]))
      {
        Console.Write("Data file not found!\nInput file name:");
        while (!ReadJson(Console.ReadLine())) Console.Write("Data file not found!\nInput file name:");
      }
      else
      {
        ReadJson(args[0]);
      }

      FinalResult = new Result();
      Stopwatch timer = new Stopwatch();
      timer.Start();
      Recursion(_requiredIncome, new Result());
      timer.Stop();
      Console.WriteLine(timer.Elapsed.TotalSeconds + " Seconds.");
      Console.WriteLine(FinalResult.PrintPrices());
    }
  }
}
