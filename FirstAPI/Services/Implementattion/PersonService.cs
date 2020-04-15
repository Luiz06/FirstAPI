using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FirstAPI.Data;
using FirstAPI.Model;


namespace FirstAPI.Services.Implementattion
{
    public class PersonService : IPersonService
    {
        private readonly FirstAPIContext _context;

        public PersonService(FirstAPIContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();

            }catch(Exception e)
            {
                throw new Exception("erro");
            }
            return person;
        }

        public void Delete(long Id)
        {

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(Id));
            try
            {
                if (result != null)
                {
                    _context.Persons.Remove(result);
                }
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                throw new Exception("erro");
            }
           
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }


        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Update(Person person)
        {
            bool has =  _context.Persons.Any(x => x.Id == person.Id);

            if (!has)
            {
                throw new Exception("Id not found");
            }
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            try
            {
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                throw new Exception("erro");
            }
            return person;
        }
    }
}
