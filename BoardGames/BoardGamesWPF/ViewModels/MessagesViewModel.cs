using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoardGamesWPF.ViewModels
{
    //Test i tymczasowen rozwiązanie
    public class MessagesViewModel
    {
	    public void Show(string text)
	    {
		    MessageBox.Show(text);
	    }
    }
}
