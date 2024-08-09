using System;
using System.Collections.Generic;

namespace _3K1DMidTest.Models;

public partial class Sale
{
    public int Id { get; set; }

    public int? CarId { get; set; }

    public int? CustomerId { get; set; }

    public double Discount { get; set; }

    public virtual Car? Car { get; set; }

    public virtual Customer? Customer { get; set; }


    public string CarMake
    {
        get
        {
            if (Car != null)
            {
                return Car.Make;
            }
            return string.Empty;
        }
    }
    public string CarModel
    {
        get
        {
            if (Car != null)
            {
                return Car.Model;
            }
            return string.Empty;
        }
    }

    public string CustomerName
    {
        get
        {
            if (Customer != null)
            {
                return Customer.Name;
            }
            return string.Empty;
        }
    }
    public DateTime CustomerBirthdate
    {
        get
        {
            if (Customer != null)
            {
                return Customer.BirthDate;
            }
            return DateTime.MinValue;
        }
    }
}
