﻿namespace Common
{
    public class Person
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string DisplayText => $"{FirstName} {LastName}";
    }
}
