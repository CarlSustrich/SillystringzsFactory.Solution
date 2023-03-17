using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Factory.Models;

public class Machine
{
  public int MachineId {get;set;}
  [Required(ErrorMessage = "Name Field May Not Be Empty")]
  public string MachineName {get;set;}
  [Required(ErrorMessage = "Date Hired May Not Be Empty!")]
  public DateTime DateAcquired {get;set;}
  public List<RepairCert> RepairCerts {get;set;}
}
