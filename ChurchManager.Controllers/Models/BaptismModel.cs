using System;

namespace ChurchManager.Controllers.Models
{
    public class BaptismModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public PersonModel Baptized { get; set; }

        public PersonModel GodParent1 { get; set; }

        public PersonModel GodParent2 { get; set; }
    }
}