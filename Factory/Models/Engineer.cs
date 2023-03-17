using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Factory.Models;

public class Engineer
{
  public int EngineerId {get;set;}
  [Required(ErrorMessage = "Name Field May Not Be Empty")]
  public string EngineerName {get;set;}
  [Required(ErrorMessage = "Date Hired May Not Be Empty!")]
  public DateTime DateOfHire {get;set;}
  public List<RepairCert> RepairCerts {get;set;}
}
