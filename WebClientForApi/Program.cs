using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebClientForApi
{
    public class Program
    {        
        public static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            int choice;
            Another:
            Console.WriteLine("\nHttp Request :\n1.Create\n2.Update\n3.Delete\n4.Get All\n5.Get By ID ");
            
            Console.Write("\nEnter the Choice : ");
            choice=Convert.ToInt32(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                    
                    Console.WriteLine("\nCREATE EMPLOYEE :-");
                    Console.WriteLine("Enter First Name :");
                    string FirstName=Console.ReadLine();
                    Console.WriteLine("Enter Last Name :");
                    string LastName = Console.ReadLine();
                    Console.WriteLine("Enter Gender :");
                    char Gender = Convert.ToChar(Console.ReadLine());
                    Console.WriteLine("Enter Date Of Birth :");
                    DateTime DOB = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter Designation :");
                    string Designation = Console.ReadLine();

                    var createdata = new
                    {
                        firstname = FirstName, 
                        lastname = LastName,
                        gender = Gender,
                        dob = DOB,
                        designation = Designation
                    };

                    var json=Newtonsoft.Json.JsonConvert.SerializeObject(createdata);
                    var createcontent =new StringContent(json,Encoding.UTF8,"application/json");
                    HttpResponseMessage responseMessage = await client.PostAsync("https://localhost:7127/Employee",createcontent);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string result = await responseMessage.Content.ReadAsStringAsync();
                        Console.WriteLine($"Data Added Successfully : {result}");
                    }
                    else
                    {
                        string errmessage = $"Failed to Add Data :{responseMessage.StatusCode}";
                        Console.WriteLine(errmessage);
                    }
                    break;

                case 2:
                    Console.WriteLine("\nUPDATE EMPLOYEE :-");
                    Console.WriteLine("Enter Employee ID:");
                    int EmployeeId= Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter First Name :");
                    FirstName = Console.ReadLine();
                    Console.WriteLine("Enter Last Name :");
                    LastName = Console.ReadLine();
                    Console.WriteLine("Enter Gender :");
                    Gender = Convert.ToChar(Console.ReadLine());
                    Console.WriteLine("Enter Date Of Birth :");
                    DOB = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter Designation :");
                    Designation = Console.ReadLine();

                    var Updatedata = new
                    {
                        employeeid= EmployeeId,
                        firstname = FirstName,
                        lastname = LastName,
                        gender = Gender,
                        dob = DOB,
                        designation = Designation
                    };

                    var updatejson = Newtonsoft.Json.JsonConvert.SerializeObject(Updatedata);
                    var updatecontent = new StringContent(updatejson, Encoding.UTF8, "application/json");
                    HttpResponseMessage responsemessage = await client.PutAsync("https://localhost:7127/Employee", updatecontent);
                    if (responsemessage.IsSuccessStatusCode)
                    {
                        string result = await responsemessage.Content.ReadAsStringAsync();
                        Console.WriteLine($"Data Updated Successfully : {result}");
                    }
                    else
                    {
                        string errmessage = $"Failed to Add Data :{responsemessage.StatusCode}";
                        Console.WriteLine(errmessage);
                    }

                    break;

                case 3:
                    Console.WriteLine("\nDelete Employee Details :-");
                    Console.WriteLine("Enter Employee ID:");
                    EmployeeId = Convert.ToInt32(Console.ReadLine());
                    HttpResponseMessage httpResponse1 = await client.DeleteAsync("https://localhost:7127/Employee"+EmployeeId);
                    if (httpResponse1.IsSuccessStatusCode)
                    {
                        string result = await httpResponse1.Content.ReadAsStringAsync();
                        Console.WriteLine($"Data Updated Successfully : {result}");
                    }
                    else
                    {
                        string errmessage = $"Failed to Add Data :{httpResponse1.StatusCode}";
                        Console.WriteLine(errmessage);
                    }
                    break;

                case 4:
                    Console.WriteLine("\nEmployee Details :-");
                    HttpResponseMessage httpResponse = await client.GetAsync("https://localhost:7127/Employee");
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string result = await httpResponse.Content.ReadAsStringAsync();
                        Console.WriteLine(result);
                    }
                    else
                    {
                        string errmessage = $"Failed to Retrive Data :{httpResponse.StatusCode}";
                        Console.WriteLine(errmessage);
                    }
                    break;

                case 5:
                    Console.WriteLine("\nGet by Id:-");
                    Console.WriteLine("Enter Employee ID:");
                    EmployeeId = Convert.ToInt32(Console.ReadLine());
                    HttpResponseMessage responseMessage1 = await client.GetAsync("https://localhost:7127/Employee/GetByEmployeeID?ID="+EmployeeId);
                    if (responseMessage1.IsSuccessStatusCode)
                    {
                        string result = await responseMessage1.Content.ReadAsStringAsync();
                        Console.WriteLine(result);
                    }
                    else
                    {
                        string errmessage = $"Failed to Retrive Data :{responseMessage1.StatusCode}";
                        Console.WriteLine(errmessage);
                    }
                    break;
                default: Console.WriteLine("\nInvalid Choice");
                    break;
            }
            
            Console.WriteLine("\n\nPress any key to Run again or Enter to Exit");
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine();
                goto Another;
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
