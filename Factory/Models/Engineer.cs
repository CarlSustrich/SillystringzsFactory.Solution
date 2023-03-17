using System;
using System.Collections.Generic;
namespace Factory.Models;

public class Engineer
{
  public int EngineerId {get;set;}
  public string EngineerName {get;set;}
  public DateTime DateOfHire {get;set;}
  public List<RepairCert> RepairCerts {get;set;}
}
