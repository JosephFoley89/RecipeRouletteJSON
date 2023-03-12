using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecipeRouletteJSON.Utility {
    internal class Alert {
        public bool DidUserConfirm() {

            return MessageBox.Show(
                "Are you sure you wish to complete this action?",
                "Please Confirm",
                MessageBoxButton.YesNo
            ) == MessageBoxResult.Yes;
        }

        public void DisplayError(string message, string title) {
            MessageBox.Show(
                message,
                title,
                MessageBoxButton.OK
            );
        }
    }
}
