using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Plato
    {
        /*
        public int id { get; set; }              // id
        public string nombre { get; set; } = ""; // title
        public string ingredientes { get; set; } = ""; // body
        public decimal precio { get; set; } = 0; // ficticio
        */


        private int _id;
        public int id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                    return;
                _id = value;
                PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(id)));
            }
        }
        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set
            {
                if (_nombre == value)
                    return;
                _nombre = value;
                PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(nombre)));
            }
        }

        private decimal _precio;
        public decimal precio
        {
            get { return _precio; }
            set
            {
                if (_precio == value)
                    return;
                _precio = value;
                PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(precio)));
            }
        }

        private string? _ingredientes;
        public string ingredientes
        {
            get { return _ingredientes; }
            set
            {
                if (_ingredientes == value)
                    return;
                _ingredientes = value;
                PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(ingredientes)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
