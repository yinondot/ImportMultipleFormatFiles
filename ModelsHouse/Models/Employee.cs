using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsHouse.Models
{
   public class Employee
    {
      private string firstName;
      private string lastName;
      private int age;

      public Employee()
      {

      }
      public Employee(string firstName, string lastName, int age)
      {
         this.firstName = firstName;
         this.lastName = lastName;
         this.age = age;
      }

      public string FirstName
      {
         get { return firstName; }
         set { firstName = value; }
      }
 
      public string LastName
      {
         get { return lastName; }
         set { lastName = value; }
      }
  
      public int Age
      {
         get { return age; }
         set { age = value; }
      }

   


   }
}
