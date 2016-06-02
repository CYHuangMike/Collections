using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gmaps.Models
{
  public class Resort
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }
    public string LogoUrl { get; set; }
    public string HomePage { get; set; }
  }
}