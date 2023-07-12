using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookADO1
{
    public class BookOperation
    {
        private string connection = $"Data source= DESKTOP-41GBJMF; Database = AddressBooKStored; Integrated Security = true; TrustServerCertificate = true";

        private SqlConnection sqlConnection;

        public BookOperation()
        {
            sqlConnection = new SqlConnection(connection);
        }
        public bool AddContact(Contact contact)
        {
            try
            {
                sqlConnection.Open();
                string query = "AddContact";
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection, sqlTransaction);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@FirstName", contact.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", contact.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Email", contact.Email);
                sqlCommand.Parameters.AddWithValue("@City", contact.City);
                sqlCommand.Parameters.AddWithValue("@SState", contact.SState);
                sqlCommand.Parameters.AddWithValue("@Zip", contact.Zip);

                try
                {
                    int result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    if (result > 0)
                    {
                        Console.WriteLine($"{result} number of rows affected in Contact Table");
                        Console.WriteLine("Data added .....");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong");
                        sqlConnection.Close();
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                    Console.WriteLine("Rollback changes ");
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DisplayContacts()
        {
            try
            {
                List<Contact> contactslist = new List<Contact>();


                sqlConnection.Open();
                string query = "SELECT * FROM contacts";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Contact contact = new Contact()
                    {
                        ID = (int)reader["ID"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        City = (string)reader["City"],
                        SState = (string)reader["SState"],
                        Zip = (string)reader["Zip"],
                    };

                    contactslist.Add(contact);
                }
                foreach (Contact contact in contactslist)
                {
                    Console.WriteLine($"ID: {contact.ID}");
                    Console.WriteLine($"First Name: {contact.FirstName}");
                    Console.WriteLine($"Last Name: {contact.LastName}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
                    Console.WriteLine($"City: {contact.City}");
                    Console.WriteLine($"State: {contact.SState}");
                    Console.WriteLine($"Zip: {contact.Zip}");
                    Console.WriteLine("----------------------");
                }


                return true;

            }
            catch(Exception ex ) 
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                connection.Clone();
            }
            
        }

    }
}

