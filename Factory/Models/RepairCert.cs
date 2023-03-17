using System;
using System.Collections.Generic;
namespace Factory.Models;

public class RepairCert
{
  public int RepairCertId {get;set;}
  public int EngineerId {get;set;}
  public int MachineId {get;set;}
  public Engineer Engineer {get;set;}
  public Machine Machine {get;set;}
}
