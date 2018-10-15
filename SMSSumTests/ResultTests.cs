using Microsoft.VisualStudio.TestTools.UnitTesting;
using SMSSum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSum.Tests
{
  [TestClass()]
  public class ResultTests
  {
    [TestMethod()]
    public void ResultTest()
    {
      var result = new Result();
      Assert.IsNotNull(result.Prices);
      Assert.IsNotNull(result.Income);
    }

    [TestMethod()]
    public void ResultTestOverlay()
    {
      var result = new Result();
      result.Prices.Add(1);
      result.Income.Add(1);
      var result2=new Result(result);
      Assert.AreNotSame(result,result2);
      Assert.AreEqual(result.Prices[0],result2.Prices[0]);
      Assert.AreEqual(result.Income[0], result2.Income[0]);
    }

    [TestMethod()]
    public void AddPlanTest()
    {
      var result=new Result();
      Dictionary<string,double> smsPlan = new Dictionary<string, double>();
      smsPlan.Add("price",1);
      smsPlan.Add("income",1);
      result.AddPlan(smsPlan);
      Assert.AreEqual(result.Prices[0],smsPlan["price"]);
      Assert.AreEqual(result.Income[0], smsPlan["income"]);
    }

    [TestMethod()]
    public void PrintPricesTest()
    {
      var result=new Result();
      Dictionary<string, double> smsPlan = new Dictionary<string, double>();
      smsPlan.Add("price", 1.5);
      smsPlan.Add("income", 1.5);
      result.AddPlan(smsPlan);
      smsPlan["price"] = 2;
      smsPlan["income"] = 2;
      result.AddPlan(smsPlan);
      Assert.IsFalse(result.PrintPrices()=="1.5 EUR, 2 EUR = 3.5");
    }
  }
}