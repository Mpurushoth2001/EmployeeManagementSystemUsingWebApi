using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            Console.Write("Enter User ID :");
            var username = Console.ReadLine();
            
            Console.Write("Enter Password :");
            var password = Console.ReadLine();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
            int choice;
            Another:
            Console.WriteLine("Http Request :\n1.Create\n2.Update\n3.Delete\n4.Get All\n5.Get By ID ");
            
            Console.Write("\nEnter the Choice : ");
            choice=Convert.ToInt32(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                    
                    Console.WriteLine("\nCREATE EMPLOYEE :-");
                    Console.Write("\nEnter First Name :");
                    string FirstName=Console.ReadLine();
                    Console.Write("\nEnter Last Name :");
                    string LastName = Console.ReadLine();
                    Console.Write("\nEnter Gender :");
                    char Gender = Convert.ToChar(Console.ReadLine());
                    Console.Write("\nEnter Date Of Birth :");
                    DateTime DOB = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("\nEnter Designation :");
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
                    Console.Write("\nEnter Employee ID:");
                    int EmployeeId= Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nEnter First Name :");
                    FirstName = Console.ReadLine();
                    Console.Write("\nEnter Last Name :");
                    LastName = Console.ReadLine();
                    Console.Write("\nEnter Gender :");
                    Gender = Convert.ToChar(Console.ReadLine());
                    Console.Write("\nEnter Date Of Birth :");
                    DOB = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("\nEnter Designation :");
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
                        string errmessage = $"Failed to Update Data :{responsemessage.StatusCode}";
                        Console.WriteLine(errmessage);
                    }

                    break;

                case 3:
                    Console.WriteLine("\nDelete Employee Details :-");
                    Console.Write("\nEnter Employee ID:");
                    int ID = Convert.ToInt32(Console.ReadLine());
                    HttpResponseMessage httpResponse1 = await client.DeleteAsync("https://localhost:7127/Employee?ID="+ ID);
                    if (httpResponse1.IsSuccessStatusCode)
                    {
                        //string result = await httpResponse1.Content.ReadAsStringAsync();
                        Console.WriteLine($"Data Deleted Successfully ");
                    }
                    else
                    {
                        string errmessage = $"Failed to Delete Data :{httpResponse1.StatusCode}";
                        Console.WriteLine(errmessage);
                    }
                    break;

                case 4:
                    
                    Console.WriteLine("\nFetch Employee Details :-\n");
                    HttpResponseMessage httpResponse = await client.GetAsync("https://localhost:7127/Employee");
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string result = await httpResponse.Content.ReadAsStringAsync();
                        Console.WriteLine(result);
                    }
                    else
                    {
                        string errmessage = $"No Data Found :{httpResponse.StatusCode}";
                        Console.WriteLine(errmessage);
                    }
                    break;

                case 5:
                    Console.WriteLine("\nbFetch Employee Details by Id:-");
                    Console.Write("\nEnter Employee ID:");
                    EmployeeId = Convert.ToInt32(Console.ReadLine());
                    HttpResponseMessage responseMessage1 = await client.GetAsync("https://localhost:7127/Employee/GetByEmployeeID?ID="+EmployeeId);
                    if (responseMessage1.IsSuccessStatusCode)
                    {
                        string result = await responseMessage1.Content.ReadAsStringAsync();
                        Console.WriteLine(result);
                    }
                    else
                    {
                        string errmessage = $"No Data Found :{responseMessage1.StatusCode}";
                        Console.WriteLine(errmessage);
                    }
                    break;
                default: Console.WriteLine("\nInvalid Choice");
                    goto Another;
                    break;
            }
            
            Run:
            Console.WriteLine("\n\nPress 'R' key to Run the Request Again or Press 'E' to Exit");
            char key=Convert.ToChar(Console.ReadKey().Key);
            if (key == (char)ConsoleKey.R)
            {
                Console.WriteLine();
                goto Another;
            }
            else if(key == (char)ConsoleKey.E)
            {
                Environment.Exit(0);
            }
            else if((key != (char)ConsoleKey.R) && (key != (char)ConsoleKey.E))
            {
                Console.WriteLine("\nInvalid Choice");
                goto Run;
            }
        }
    }
}
