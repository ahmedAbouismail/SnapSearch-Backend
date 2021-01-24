using MvvmHelpers;
using System;

namespace Snapsearch.Models
{
    public class Item : ObservableObject
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
    }
}