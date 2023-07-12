namespace AddressBookADO1
{
    public class Program
    {
        static void Main(string[] args)
        {
            BookOperation Operations = new BookOperation();
            Console.WriteLine("/n 1.Enter a contact to add to to addresbook : ");

            Console.WriteLine("\n Enter option:");
            int option = Convert.ToInt32(Console.ReadLine());

            switch(option)
            {
                case 1:
                    {
                        Console.WriteLine("Enter Firstname: - ");
                        string FirstName = Console.ReadLine();
                        Console.WriteLine("Enter LastName");
                        string LastName = Console.ReadLine();
                        Console.WriteLine("Enter PhoneNumber");
                        string PhoneNumber = Console.ReadLine();
                        Console.WriteLine("Enter Email:- ");
                        string Email = Console.ReadLine();
                        Console.WriteLine("Enter City:- ");
                        string City = Console.ReadLine();
                        Console.WriteLine("Enter Pincode:- ");
                        string Zip = Console.ReadLine();
                        Console.WriteLine("Enter State:- ");
                        string State = Console.ReadLine();

                        Contact contact = new Contact(FirstName, LastName, PhoneNumber, Email, City, State, Zip);
                        Operations.AddContact(contact);
                        break;
                    }
            }
        }
    }
}