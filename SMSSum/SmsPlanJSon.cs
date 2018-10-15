using System.Collections.Generic;

namespace SMSSum
{
  /// <summary>
  /// Object to decode JSon file data
  /// </summary>

  class SmsPlanJson
  {
    public List<Dictionary<string, double>> sms_list { get; set; }
    public int max_messages { get; set; }
    public double required_income { get; set; }
  }
}
