using System;
using System.Collections.Generic;
namespace Factory.Models;

public class Machine
{
  public int MachineId {get;set;}
  public string MachineName {get;set;}
  public DateTime DateAcquired {get;set;}
  public List<RepairCert> RepairCerts {get;set;}
}
