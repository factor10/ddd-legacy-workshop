using System.Collections.Generic;
using factor10.TimeRegistration.DomainModel;

namespace factor10.TimeRegistration.AntiCorruptionLayer
{
    public class CustomersAgent : ICustomersAgent
    {
        private static readonly IDictionary<string, Customer> customers = new Dictionary<string, Customer>();

        //Fake customers for now, this will probably be an integration later or maybe a persistent cache?
        static CustomersAgent()
        {
            var sonera = new Customer("Sonera");
            customers.Add(sonera.Name, sonera);
            
            var finnair = new Customer("Finnair");
            customers.Add(finnair.Name, finnair);
        }

        public Customer TheOneWithName(string name)
        {
            customers.TryGetValue(name, out var customer);
            return customer;
        }
    }
}
