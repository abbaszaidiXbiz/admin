using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Collections;

public class Employee
{
     [BsonId]
    public string _id { get; set; }  = new BsonObjectId(ObjectId.GenerateNewId()).ToString();
    public string EmployeeID { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string Age { get; set; }
    public string Role { get; set; }
    public string StartDate { get; set; }
    public string Salary { get; set; }
    public string Address { get; set; }
}