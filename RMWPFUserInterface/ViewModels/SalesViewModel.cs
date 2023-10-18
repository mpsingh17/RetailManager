using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWPFUserInterface.ViewModels
{
    public class SalesViewModel : Screen
    {
		private BindingList<string> _products;

		public BindingList<string> Products
		{
			get { return _products; }
			set 
			{ 
				_products = value; 
				NotifyOfPropertyChange(() => Products);
			}
		}

		private BindingList<string> _cart;

		public BindingList<string> Cart
		{
			get { return _cart; }
			set 
			{
				_cart = value; 
				NotifyOfPropertyChange(() => Cart);
			}
		}


		private int _itemQuantity;

		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set 
			{
				_itemQuantity = value;
				NotifyOfPropertyChange(() => ItemQuantity);
			}
		}


		public string SubTotoal
        {
			get 
			{
				// TODO: Replace with calculation.
				return "$0.0";
			}
		}

        public string Tax
        {
            get
            {
                // TODO: Replace with calculation.
                return "$0.0";
            }
        }

        public string Total
        {
            get
            {
                // TODO: Replace with calculation.
                return "$0.0";
            }
        }


        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                // Make sure something is selected.
				// Make sure item quantity is available.

                return output;
            }
        }

		public void AddToCart()
		{

		}

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                // Make sure something is selected.

                return output;
            }
        }

        public void RemoveFromCart()
        {

        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                // Make sure there's something in the cart.

                return output;
            }
        }

        public void CheckOut()
        {

        }
    }
}
